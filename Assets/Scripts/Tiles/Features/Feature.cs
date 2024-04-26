using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Feature {
    
    public string sName {
        get { return Features.arsFeatureTypeNames[(int)featuretype]; }
    }
    public FeatureType featuretype;
    public TileTerrain tileterrain;

    //Store any values necessary for supporting special behaviours on the Feature
    private Dictionary<string, object> dictStoredContextValues; 

    public Feature(TileTerrain _tileterrain) {
        tileterrain = _tileterrain;
    }


    //Cast the return value to whatever type we need
    public object GetContextValue(string s) {
        object objValue = null;
        if(dictStoredContextValues == null || dictStoredContextValues.TryGetValue(s, out objValue) == false) {
            Debug.LogErrorFormat("Could not load ContextValue {0}", s);
        }
        return objValue;
    }

    public void SetContextValue(string s, object o) {
        if (dictStoredContextValues == null) dictStoredContextValues = new Dictionary<string, object>();

        if (dictStoredContextValues.ContainsKey(s)) {
            dictStoredContextValues[s] = o;
        } else {
            dictStoredContextValues.Add(s, o);
        }
    }


}
