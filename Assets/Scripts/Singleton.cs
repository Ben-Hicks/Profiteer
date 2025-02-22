﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour {

    private static T inst;
    private bool bStarted = false;

    public static T Get() {

        if (inst == null) {
            Debug.LogWarning("Error! Static instance not set!");

            inst = FindObjectOfType<T>();
            if(inst == null) {
                Debug.LogError("Still could not find any objects of this type");
            }
        }

        return inst;
    }

    protected virtual void Awake() {

        if (inst != null) {
            //If an static instance exists,
            // then panic!  Destroy ourselves
            //Debug.Log("Warning!  This singleton already exists (" + gameObject.name + "), so we shouldn't instantiate a new one");
            Destroy(gameObject);

        } else {
            inst = gameObject.GetComponent<T>();
        }


    }

    ~Singleton() {
        //On destruction, we're clearing out the static inst reference
        if(inst == this) inst = null;
    }

    //Singletons will Initialize themselves once when they're created (but will only get to exist if another instance doesn't already exist
    // - switching scenes will delete the previous instance and let a new one Init itself and become the new singleton instance
    public abstract void Init();

    public virtual void Start() {

        if (bStarted == true) return;
        bStarted = true;

        Init();

    }


}