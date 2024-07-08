using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory {

    public Entity entOwner;

    public List<Item> lstItems = new List<Item>();
    public Subject subInventoryNewItem = new Subject();
    public Subject subInventoryItemFullyRemoved = new Subject();


    public void AddItem(Item item) {
        Debug.LogFormat("Adding {0}", item);
        for (int i = 0; i < lstItems.Count; i++) {
            if (item.itemtype == lstItems[i].itemtype) {
                lstItems[i].nCount.Set(lstItems[i].nCount.Get() + item.nCount.Get());
                return;
            }
        }

        lstItems.Add(item);
        subInventoryNewItem.NotifyObs(null, item);
    }

    public void AddItems(params Item[] arItems) {
        foreach(Item item in arItems) {
            AddItem(item);
        }
    }

    public void RemoveItem(Item itemToRemove) {
        for(int i=0; i< lstItems.Count; i++) {
            if(itemToRemove.itemtype == lstItems[i].itemtype) {
                if(lstItems[i].nCount.Get() < itemToRemove.nCount.Get()) {
                    Debug.LogErrorFormat("Can't remove {0} {1} since we only have {2}", itemToRemove.nCount.Get(), itemToRemove.itemtype, lstItems[i].nCount.Get());
                    return;
                }else if(lstItems[i].nCount.Get() == itemToRemove.nCount.Get()) {
                    lstItems.RemoveAt(i);
                    subInventoryItemFullyRemoved.NotifyObs(null, itemToRemove);
                } else {
                    lstItems[i].nCount.Set(lstItems[i].nCount.Get() - itemToRemove.nCount.Get());
                }
                return;
            }
        }
        Debug.LogErrorFormat("Can't remove {0} since we don't have any", itemToRemove.itemtype);
    }

    public int GetItemCount(ItemType itemtype) {
        for(int i=0; i< lstItems.Count; i++) {
            if(itemtype == lstItems[i].itemtype) {
                return lstItems[i].nCount.Get();
            }
        }
        return 0;
    }

    public bool CanAfford(ItemType itemtype, int nCost) {
        return GetItemCount(itemtype) >= nCost;
    }

    public Inventory(Entity _entOwner) {
        entOwner = _entOwner;
    }
}
