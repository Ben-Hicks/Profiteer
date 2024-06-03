using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DictTags {


    private Dictionary<string, object> dict;

    public DictTags(params (string, object) [] arInitTags) {
        dict = new Dictionary<string, object>();

        foreach((string, object) pairTag in arInitTags) {
            SetFeatureValue(pairTag.Item1, pairTag.Item2);
        }
    }

    public void SetFeatureValue(string sFeatureValue, object val) {
        if (dict.ContainsKey(sFeatureValue)) {
            dict[sFeatureValue] = val;
        } else {
            dict.Add(sFeatureValue, val);
        }
    }

    public bool FetchFeatureValue<T>(string sFeatureValue, out T valFetched) {

        object oVal;

        bool bSuccess = dict.TryGetValue(sFeatureValue, out oVal);

        if (bSuccess == false) {
            valFetched = default(T);
        } else {
            valFetched = (T)oVal;
        }

        return bSuccess;
    }

}
