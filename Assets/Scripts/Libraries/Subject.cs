﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Subject {

    public enum SubType { ALL }; //A flag to pass to the constructor when initiallizing 
                                 // static subjects

    public string sName;

    //Keep a static list of all static subjects (so that we can reset them as needed)
    public static List<Subject> lstAllStaticSubjects;

    public delegate void FnCallback(Object target, params object[] args);

    public List<FnCallback> lstCallbacks = new List<FnCallback>();

    public Subject() {
        lstCallbacks = new List<FnCallback>();
        sName = "none";
    }

    public Subject(Subject subToCopy) {

        Debug.LogError("Warning - copying Subjects can lead to calling Callback methods for shallow object instances");
        lstCallbacks = new List<FnCallback>(subToCopy.lstCallbacks);
        sName = subToCopy.sName + " - copy";
    }

    //To be called only when creating static instances of Subjects
    public Subject(SubType subType, string _sName = "") {

        if (lstAllStaticSubjects == null) {
            lstAllStaticSubjects = new List<Subject>();
        }

        //Note - in principle, since these are static, they should never be destroyed
        //       and thus should never need to be removed from this list
        lstAllStaticSubjects.Add(this);

        sName = _sName;
        //if(sName != "") Debug.Log("Creating " + sName);

    }

    public void ResetSubject() {

        //Clear the list of callback functions
        lstCallbacks = new List<FnCallback>();

    }

    public static void ResetAllStaticSubjects() {

        //Reinitalize all of the static subjects so that they can be reinitialized properly
        // when restarting the game
        for (int i = 0; i < lstAllStaticSubjects.Count; i++) {
            lstAllStaticSubjects[i].ResetSubject();
        }

    }

    public void Subscribe(FnCallback fnCallback) {

        lstCallbacks.Add(fnCallback);
    }

    public void UnSubscribe(FnCallback fnCallback) {

        lstCallbacks.Remove(fnCallback);
    }

    // Used for unspecific updates for views
    public virtual void NotifyObs() {
        NotifyObs(null);
    }

    public virtual void NotifyObs(Object target, params object[] args) {

        List<FnCallback> lstCopied = new List<FnCallback>(lstCallbacks);
        foreach (FnCallback callback in lstCopied) {
            //if (callback == null)
            //	continue;//in case this object has been removed by the results of previous update iterations
            callback(target, args);
        }
    }
}