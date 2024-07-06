using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubValue<T> {

    private T val;

    public Subject subValChanged;

    public void Set(T _val) {
        val = _val;

        subValChanged.NotifyObs();
    }

    public T Get() {
        return val;
    }

    public void Subscribe(Subject.FnCallback fnCallback) {
        subValChanged.Subscribe(fnCallback);
    }

    public void UnSubscribe(Subject.FnCallback fnCallback) {
        subValChanged.UnSubscribe(fnCallback);
    }

    public SubValue(T _val){

        subValChanged = new Subject();

        Set(_val);

    }

}
