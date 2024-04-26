using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heap<T> {

    public List<(float, T)> lst;

    public Heap() {
        lst = new List<(float, T)>();
    }

    private int Left(int i) {
        return i * 2 + 1;
    }

    private int Right(int i) {
        return i * 2 + 2;
    }

    private int Parent(int i) {
        if (i % 2 == 0) {
            return (i - 2) / 2;
        } else {
            return (i - 1) / 2;
        }
    }

    private void Swap(int i, int j) {
        float ftemp = lst[i].Item1;
        T ttemp = lst[i].Item2;
        lst[i] = lst[j];
        lst[j] = (ftemp, ttemp);
    }

    private int FixUp(int i) {
        while(i > 0) {
            int iParent = Parent(i);
            if (lst[i].Item1 >= lst[iParent].Item1) break;
            Swap(i, iParent);
            i = iParent;
        }
        return i;
    }

    private int FixDown(int i) {
        while(i < lst.Count) {
            int iLeft = Left(i);
            if (iLeft >= lst.Count) break;
            int iRight = Right(i);
            if (iRight >= lst.Count || lst[iLeft].Item1 <= lst[iRight].Item1) {
                if (lst[i].Item1 < lst[iLeft].Item1) break;
                Swap(i, iLeft);
                i = iLeft;
            } else {
                if (lst[i].Item1 < lst[iRight].Item1) break;
                Swap(i, iRight);
                i = iRight;
            }
        }
        return i;
    }

    public bool IsEmpty() {
        return lst.Count == 0;
    }

    public (float, T) Peek() {
        return lst[0];
    }

    public (float, T) PopMin() {
        (float, T) ret = lst[0];

        Swap(0, lst.Count - 1);
        lst.RemoveAt(lst.Count - 1);

        FixDown(0);


        return ret;
    }

    public void Add((float, T) toAdd) {
        lst.Add(toAdd);

        FixDown(FixUp(lst.Count - 1));
    }

    public void Print() {
        string sSorted = "";
        for (int i = 0; i < lst.Count; i++) {
            sSorted += string.Format(" {0} ", lst[i].Item1);
        }
        Debug.Log(sSorted);
    }

    public static void Test(int n) {
        List<float> lst = new List<float>();
        Heap<float> heapTest = new Heap<float>();

        for (int i = 0; i < n; i++) {
            lst.Add(Random.Range(0f, 100f));
        }

        for (int i = 0; i < n; i++) {
            heapTest.Add((lst[i], lst[i]));
            //heapTest.Print();
        }

        lst.Clear();

        string sSorted = "";
        for (int i = 0; i < n; i++) {
            lst.Add(heapTest.PopMin().Item1);
            //heapTest.Print();
            sSorted += string.Format(" {0} ", lst[i]);
            if (i > 0 && lst[i] < lst[i - 1]) {
                Debug.LogErrorFormat("Found a mis-sorted item at {0} vs {1}", lst[i], lst[i - 1]);
            }
        }

        Debug.LogFormat(sSorted);
    }
}

