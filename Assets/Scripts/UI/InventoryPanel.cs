using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(HideablePanel))]
public class InventoryPanel : Singleton<InventoryPanel> {
    
    public GameObject goInventoryContainer;

    public List<InventoryEntry> lstInvEntry = new List<InventoryEntry>();

    public GameObject pfInventoryEntry;
    public GameObject pfSeperator;

    public Inventory inv;
    
    public HideablePanel panelContent;

    public void SetInventory(Inventory _inv) {
        inv = _inv;

        ForceDestroyAllInventoryEntry();

        ForceAddAllInventoryEntry();

        inv.subInventoryNewItem.Subscribe(cbAddNewInventoryEntry);
        inv.subInventoryItemFullyRemoved.Subscribe(cbRemoveInventoryEntry);
    }

    public void AddNewInventoryEntry(Item item, params Subject[] subToUpdateOnChange) {
        Debug.LogFormat("Adding {0}", item.ToString());

        GameObject goNewInventoryEntry = Instantiate(pfInventoryEntry, goInventoryContainer.transform);

        InventoryEntry inventry = goNewInventoryEntry.GetComponent<InventoryEntry>();
        if (inventry == null) {
            Debug.LogError("ERROR! - No InventoryEntry on the inventoryentry prefab");
            return;
        }

        inventry.SetInventory(inv);
        Debug.LogFormat("Inventory was set to {0}", inv.entOwner.ToString());
        inventry.SetItem(item);
        inventry.SetSubUpdateOnChange(subToUpdateOnChange);

        //Simulate an update to get an initial display;
        inventry.cbUpdateInventoryCountString(null);

        lstInvEntry.Add(inventry);
    }

    public void cbAddNewInventoryEntry(Object obj, params object[] args) {
        Item itemAdded = (Item)args[0];
        AddNewInventoryEntry(itemAdded, itemAdded.nCount.subValChanged);
    }

    public void RemoveInventoryEntry(ItemType itemtype) {

        Debug.LogFormat("Removing {0}", itemtype);
        for(int i=0; i<lstInvEntry.Count; i++) {
            if(lstInvEntry[i].item.itemtype == itemtype) {

                Destroy(lstInvEntry[i].gameObject);
                lstInvEntry.RemoveAt(i);
                
                return;
            }
        }
    }

    public void cbRemoveInventoryEntry(Object obj, params object[] args) {
        ItemType itemtype = (ItemType)args[0];
        RemoveInventoryEntry(itemtype);
    }

    public void AddSeperator(string sLabel) {
        GameObject goNewSeperator = Instantiate(pfSeperator, goInventoryContainer.transform);

        goNewSeperator.GetComponentInChildren<Text>().text = sLabel;
    }

    

    public void ForceAddAllInventoryEntry() {

        Debug.LogFormat("Force adding {0} inventory items", inv.lstItems.Count);

        foreach (Item item in inv.lstItems) {
            AddNewInventoryEntry(item, item.nCount.subValChanged);
        }

    }

    public void ForceDestroyAllInventoryEntry() {
        for(int i=0; i<lstInvEntry.Count; i++) {
            lstInvEntry[i].Destroy();
        }

        lstInvEntry = new List<InventoryEntry>();
    }

    public override void Init() {
        panelContent.Hide();
    }

}
