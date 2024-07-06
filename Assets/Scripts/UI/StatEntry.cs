using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   

public class StatEntry : MonoBehaviour {

    public Entity ent;
    public string sLabel;
    public delegate string GetValueString(Entity _ent);
    public GetValueString getvaluestring;
    public List<Subject> lstsubUpdateOnChange;

    public Text txtStatLabel;
    public Text txtStatValue;

    public void SetEntity(Entity _ent) {
        ent = _ent;
    }
    
    public void SetLabel(string _slabel) {
        sLabel = _slabel;

        txtStatLabel.text = sLabel;
    }

    public void SetGetValueString(GetValueString _getvaluestring) {
        getvaluestring = _getvaluestring;
    }

    public void Destroy() {
        foreach (Subject sub in lstsubUpdateOnChange) {
            sub.UnSubscribe(cbUpdateValueString);
        }

        GameObject.Destroy(this);
    }

    public void SetSubUpdateOnChange(params Subject[] _subUpdateOnChange) {
        if (lstsubUpdateOnChange != null) {
            foreach(Subject sub in lstsubUpdateOnChange) {
                sub.UnSubscribe(cbUpdateValueString);
            }
        }

        lstsubUpdateOnChange = new List<Subject>();

        if (_subUpdateOnChange.Length > 0) {
            foreach (Subject sub in _subUpdateOnChange) {
                sub.Subscribe(cbUpdateValueString);
                lstsubUpdateOnChange.Add(sub);
            }
        }
    }

    public void cbUpdateValueString(Object tar, params object[] args) {
        txtStatValue.text = getvaluestring(ent);
    }
}
