using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryEntry : MonoBehaviour {

    public Inventory inventory;

    public Item item;

    public List<Subject> lstsubUpdateOnChange;

    public Text txtInventoryName;
    public Text txtInventoryCount;
    public Text txtInventoryBaseValue;

    public void SetInventory(Inventory _inventory) {
        inventory = _inventory;
    }

    public void SetItem(Item _item) {
        item = _item;

        txtInventoryName.text = item.sName;
        txtInventoryBaseValue.text = item.nBaseValue.ToString();
        
    }

    public void Destroy() {
        foreach (Subject sub in lstsubUpdateOnChange) {
            sub.UnSubscribe(cbUpdateInventoryCountString);
        }

        GameObject.Destroy(this);
    }

    public void SetSubUpdateOnChange(params Subject[] _subUpdateOnChange) {
        if (lstsubUpdateOnChange != null) {
            foreach (Subject sub in lstsubUpdateOnChange) {
                sub.UnSubscribe(cbUpdateInventoryCountString);
            }
        }

        lstsubUpdateOnChange = new List<Subject>();

        if (_subUpdateOnChange.Length > 0) {
            foreach (Subject sub in _subUpdateOnChange) {
                sub.Subscribe(cbUpdateInventoryCountString);
                lstsubUpdateOnChange.Add(sub);
            }
        }
    }

    public void cbUpdateInventoryCountString(Object tar, params object[] args) {

        txtInventoryCount.text = inventory.GetItemCount(item.itemtype).ToString();

    }
}
