using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionBar : MonoBehaviour {

    public TileTerrain tileSelected;
    public ManualInput curManualInput;

    public Button btnEndTurn;
    public Button btnMove;
    public Button btnSearch;
    public Button btnAttack;
    public Button btnSpecial;
    public Dropdown dropdownSpecialOptions;

    public GameObject panelEnergy;
    public Text txtCurEnergy;
    public Text txtMaxEnergy;
    

    public void SelectTile(TileTerrain _tileSelected) {
        //No need to do anything if we've already selected this tile
        if (_tileSelected == tileSelected) return;

        //Deselect whatever tile we currently have
        DeselectTile();

        tileSelected = _tileSelected;
    }

    public void DeselectTile() {
        //If we don't have anything selected, then we don't need to do anything
        if (tileSelected == null) return;

        tileSelected = null;

    }

    public bool CanSubmitAction() {
        if(curManualInput != null) {
            Debug.LogFormat("Current Entity acting is {0}", curManualInput.ent);
            return true;
        } else {
            Debug.LogFormat("No manual input is currently acting");
            return false;
        }
    }


    public void OnMoveInput() {
        if (CanSubmitAction() == false) return;
        if (tileSelected == null) return;

        TurnController.Get().SubmitChosenAction(new ActionEntityMove(curManualInput.ent, tileSelected));
    }

    public void OnSearchInput() {
        if (CanSubmitAction() == false) return;
        if (tileSelected == null) return;

        TurnController.Get().SubmitChosenAction(new ActionEntitySearch(curManualInput.ent, tileSelected));
    }

    public void OnAttackInput() {
        if (CanSubmitAction() == false) return;
        if (tileSelected == null) return;

        TurnController.Get().SubmitChosenAction(new ActionEntityMove(curManualInput.ent, tileSelected));
    }

    public void OnSpecialInput() {
        if (CanSubmitAction() == false) return;
        if (tileSelected == null) return;

        TurnController.Get().SubmitChosenAction(new ActionEntityMove(curManualInput.ent, tileSelected));
    }

    public void OnEndTurnInput() {
        if (CanSubmitAction() == false) return;

        TurnController.Get().SubmitFinishTurn(curManualInput.ent);
    }

    public void cbUpdateEnergyDisplay(Object obj, params object[] args) {
        if(curManualInput == null) {
            panelEnergy.gameObject.SetActive(false);
        } else {
            panelEnergy.gameObject.SetActive(true);
            txtCurEnergy.text = curManualInput.entinfo.nCurEnergy.Get().ToString();
            txtMaxEnergy.text = curManualInput.entinfo.nMaxEnergy.Get().ToString();
        }
    }

    public void cbOpenManualInput(Object obj, object[] args) {
        curManualInput = (ManualInput)obj;

        curManualInput.entinfo.nCurEnergy.Subscribe(cbUpdateEnergyDisplay);
        curManualInput.entinfo.nMaxEnergy.Subscribe(cbUpdateEnergyDisplay);
        cbUpdateEnergyDisplay(curManualInput);
    }

    public void cbCloseManualInput(Object obj, object[] args) {
        if(curManualInput != (ManualInput)obj) {
            Debug.LogErrorFormat("Tried to end manual input for {0}, but we're currently open for {1}", ((ManualInput)obj).ent, curManualInput.ent);
        }

        curManualInput.entinfo.nCurEnergy.UnSubscribe(cbUpdateEnergyDisplay);
        curManualInput.entinfo.nMaxEnergy.UnSubscribe(cbUpdateEnergyDisplay);
        curManualInput = null;

        cbUpdateEnergyDisplay(null);
    }

    public void cbClickTile(Object obj, object[] args) {
        TileTerrain tileClicked = (TileTerrain)args[0];
        SelectTile((TileTerrain)args[0]);
    }

    public void Start() {
        TurnController.Get().subOpenManualInput.Subscribe(cbOpenManualInput);
        TurnController.Get().subCloseManualInput.Subscribe(cbCloseManualInput);
        MapInput.Get().subTileClick.Subscribe(cbClickTile);
    }

    public void Update() {

        if (Input.GetKeyUp(KeyCode.Alpha0)) {
            OnEndTurnInput();
        }else if (Input.GetKeyUp(KeyCode.Alpha1)) {
            OnMoveInput();
        }else if(Input.GetKeyUp(KeyCode.Alpha2)) {
            OnSearchInput();
        }else if (Input.GetKeyUp(KeyCode.Alpha3)) {
            OnAttackInput();
        } else if (Input.GetKeyUp(KeyCode.Alpha4)) {
            OnSpecialInput();
        }


    }
}
