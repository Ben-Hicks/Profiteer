using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterOptionsPanel : MonoBehaviour {
    
    public StatsPanel statspanel;
    public InventoryPanel inventorypanel;


    public void HideAll() {
        statspanel.panelContent.Hide();
        inventorypanel.panelContent.Hide();
    }

    public void ShowPanel(HideablePanel hideableToShow) {
        HideAll();
        if(hideableToShow != null) hideableToShow.Show();
    }

}
