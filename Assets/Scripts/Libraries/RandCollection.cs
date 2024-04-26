using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandCollection<T> {

    private Dictionary<int, T> dict;
    private int nTotalWeight;

    public RandCollection(params (T, int)[] lstToAdd) {
        nTotalWeight = 0;

        dict = new Dictionary<int, T>();
        Add(lstToAdd);
    }

    public void Add(params (T, int)[] lstToAdd) {
        foreach((T, int) pair in lstToAdd) {
            Add(pair.Item1, pair.Item2);
        }
    }

    public void Add(T t, int nWeight) {
        if(nWeight <= 0) {
            Debug.LogErrorFormat("Cannot support a weight of {0}", nWeight);
            return;
        }

        nTotalWeight += nWeight;
        dict.Add(nTotalWeight - 1, t);
    }

    public T GetRandom() {
        int nRand = Random.Range(0, nTotalWeight);

        while(nRand < nTotalWeight) {

            if (dict.ContainsKey(nRand)) return dict[nRand];

            nRand++;
        }

        Debug.LogErrorFormat("Could not get an entry for nRand = {0} since we only have total weight of {1}", nRand, nTotalWeight);
        return default(T);
    }
    
}
