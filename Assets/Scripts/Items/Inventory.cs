using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory {

    private List<(ItemType, int)> lstItemCounts = new List<(ItemType, int)>();

    public void AddItem(ItemType itemtype, int nAdded) {
        for(int i=0; i<lstItemCounts.Count; i++) {
            if(itemtype == lstItemCounts[i].Item1) {
                lstItemCounts[i] = (itemtype, lstItemCounts[i].Item2 + nAdded);
                return;
            }
        }

        lstItemCounts.Add((itemtype, nAdded));
    }

    public void RemoveItem(ItemType itemtype, int nToRemove) {
        for(int i=0; i<lstItemCounts.Count; i++) {
            if(itemtype == lstItemCounts[i].Item1) {
                if(lstItemCounts[i].Item2 < nToRemove) {
                    Debug.LogErrorFormat("Can't remove {0} {1} since we only have {2}", nToRemove, itemtype, lstItemCounts[i].Item2);
                    return;
                }else if(lstItemCounts[i].Item2 == nToRemove) {
                    lstItemCounts.RemoveAt(i);
                } else {
                    lstItemCounts[i] = (itemtype, lstItemCounts[i].Item2 - nToRemove);
                }
                return;
            }
        }
        Debug.LogErrorFormat("Can't remove {0} since we don't have any", itemtype);
    }

    public int GetItemCount(ItemType itemtype) {
        for(int i=0; i<lstItemCounts.Count; i++) {
            if(itemtype == lstItemCounts[i].Item1) {
                return lstItemCounts[i].Item2;
            }
        }
        return 0;
    }

    public bool CanAfford(ItemType itemtype, int nCost) {
        return GetItemCount(itemtype) >= nCost;
    }
}
