using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public enum TaskType { Test, TileInfoPopulation, LENGTH };

public class Threader : Singleton<Threader> {

    public Map map;
    public MapGenerator mapgenerator;

    [System.Serializable]
    public struct ThreadTask {
        public TaskType tasktype;

        public int nMaxThreads;
        public List<bool> lstFinishedThreads;

        public System.Action funcFinishedTask;
    }

    public ThreadTask[] arThreadTasks;

    public void DistributeTask(TaskType tasktype, List<Map.Col> lst, System.Action<List<TileTerrain>> func, System.Action _funcFinishedTask) {
        Debug.LogFormat("Called DistributeTask for type {0}", tasktype);

        //Create a clean list of finished threads
        arThreadTasks[(int)tasktype].lstFinishedThreads = new List<bool>();
        for (int j = 0; j < arThreadTasks[(int)tasktype].nMaxThreads; j++) {
            arThreadTasks[(int)tasktype].lstFinishedThreads.Add(false);
        }
        arThreadTasks[(int)tasktype].funcFinishedTask = _funcFinishedTask;


        int nItemsPerThread = lst.Count / arThreadTasks[(int)tasktype].nMaxThreads;
        int nRemainingItems = lst.Count % arThreadTasks[(int)tasktype].nMaxThreads;

        List<Thread> lstThreads = new List<Thread>();

        for (int iThread = 0; iThread < arThreadTasks[(int)tasktype].nMaxThreads; iThread++) {
            Debug.LogFormat("iThread {0} ", iThread);
            lstThreads.Add(
                new Thread(new ThreadStart(
                CreateThreadFunc(iThread, (int _iThread) => {
                    return () => {
                        int iStart = _iThread * nItemsPerThread;
                        if (_iThread > 0) iStart += nRemainingItems;
                        int iEnd = iStart + nItemsPerThread;
                        if (_iThread == 0) iEnd += nRemainingItems;

                        
                        Debug.LogFormat("Thread {0} working from {1} to {2}", _iThread, iStart, iEnd);
                        for (int i = iStart; i < iEnd; i++) {
                            Debug.LogFormat("Thread {0} working on column {1}", _iThread, i);
                            //func(lst[i]);
                            foreach(TileTerrain t in lst[i].lstTiles) {
                                t.tileinfo.iThreadMadeBy = _iThread;
                                t.tileinfo.nColumnAccordingToThread = i;
                                mapgenerator.PopulateTileInfo(t.tileinfo);
                            }
                        }

                        FinishThread(tasktype, _iThread);
                        return;
                    };
                }))));

        }

        for (int i = 0; i < lstThreads.Count; i++) {
            lstThreads[i].Start();
        }
    }

    /*
    public void DistributeTask<T>(TaskType tasktype, List<T> lst, System.Action<T> func, System.Action _funcFinishedTask) {

        //Create a clean list of finished threads
        arThreadTasks[(int)tasktype].lstFinishedThreads = new List<bool>();
        for (int j = 0; j < arThreadTasks[(int)tasktype].nMaxThreads; j++) {
            arThreadTasks[(int)tasktype].lstFinishedThreads.Add(false);
        }
        arThreadTasks[(int)tasktype].funcFinishedTask = _funcFinishedTask;


        int nItemsPerThread = lst.Count / arThreadTasks[(int)tasktype].nMaxThreads;
        int nRemainingItems = lst.Count % arThreadTasks[(int)tasktype].nMaxThreads;

        List<Thread> lstThreads = new List<Thread>();

        for (int iThread = 0; iThread < arThreadTasks[(int)tasktype].nMaxThreads; iThread++) {
            Debug.LogFormat("iThread {0} ", iThread);
            lstThreads.Add(
                new Thread(new ThreadStart(
                CreateThreadFunc(iThread, (int _iThread) => {
                    return () => {
                        int iStart = _iThread * nItemsPerThread + nRemainingItems;
                        int iEnd = iStart + nItemsPerThread;
                        Debug.LogFormat("Thread {0} working from {1} to {2}", _iThread, iStart, iEnd);
                        for (int i = iStart; i < iEnd; i++) {
                            Debug.LogFormat("Thread {0} working on column {1}", _iThread, i);
                            func(lst[i]);
                        }

                        FinishThread(tasktype, _iThread);
                        return;
                    };
                }))));

        }

        for (int i = 0; i < lstThreads.Count; i++) {
            lstThreads[i].Start();
        }
    }
    */

    public delegate System.Action ThreadSpawner(int iThread);

    public System.Action CreateThreadFunc(int iThread, ThreadSpawner threadSpawner) {
        return threadSpawner(iThread);
    }

    public void StartThread(System.Action task) {

        Thread t = new Thread(new ThreadStart(task));
        t.Start();
    }

    public void FinishThread(TaskType tasktype, int iThread) {
        Debug.LogFormat("Calling FinishThread with tasktype={0}, iThread = {1}, and #threads = {2}", tasktype, iThread, arThreadTasks[(int)tasktype].lstFinishedThreads.Count);
        arThreadTasks[(int)tasktype].lstFinishedThreads[iThread] = true;

    }

    public void Update() {

        for (int i = 0; i < (int)TaskType.LENGTH; i++) {

            if (arThreadTasks[i].lstFinishedThreads.Count > 0) {
                int nActiveThreads = 0;

                for (int j = 0; j < arThreadTasks[i].nMaxThreads; j++) {
                    if (arThreadTasks[i].lstFinishedThreads[j]) {
                        nActiveThreads++;
                    }
                }

                Debug.LogFormat("Task {2} has finished {0}/{1} threads", nActiveThreads, arThreadTasks[i].nMaxThreads, (TaskType)i);
                if (nActiveThreads == arThreadTasks[i].nMaxThreads) {
                    Debug.LogFormat("Finished task {0}", (TaskType)i);
                    arThreadTasks[i].funcFinishedTask();
                    arThreadTasks[i].lstFinishedThreads = new List<bool>();
                }
            }
        }
    }

    public override void Init() {

    }

}
