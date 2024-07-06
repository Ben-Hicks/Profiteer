using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Consumable : Item {

    public Consumable(int _nCount) : base(_nCount) {

    }

    public abstract void OnConsume();

}
