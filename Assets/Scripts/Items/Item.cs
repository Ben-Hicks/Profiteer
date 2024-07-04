using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item {

    public string sName;
    public ItemType itemtype;
    public SubValue<int> nCount;
    public int nBaseValue;

    public virtual int GetValue() {
        return nBaseValue;
    }
    
}
