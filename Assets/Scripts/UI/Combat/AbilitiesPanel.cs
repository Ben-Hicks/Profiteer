using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilitiesPanel : MonoBehaviour {

    public Combat combat;
    public Combatter combatterShowing;
    public HideablePanel hideablePanel;

    public Text txtAbilLeft2;
    public Text txtAbilLeft1;
    public Text txtAbilArmour;
    public Text txtAbilRight1;
    public Text txtAbilRight2;


    public void SubmitAbilitySelection(Ability abilChosen) {

        if (combat.combatterCurActing == null) {
            Debug.LogError("No entity currently acting, so can't submit any ability selections");
            return;
        }

        if(combat.combatterCurActing != combatterShowing) {
            Debug.LogFormat("Can't submit an ability selection for {0} since {1} is currently acting", combatterShowing, combat.combatterCurActing);
            return;
        }

        if (combat.combatterCurActing.team !=  Combatter.Team.Player) {
            Debug.LogErrorFormat("Can't submit an ability selection for {0} since they aren't a human", combat.combatterCurActing.ent);
            return;
        }

        combat.ChooseAbilityToExecute(abilChosen, combatterShowing);
        
    }

    public void OnClickAbilityLeft2() {
        SubmitAbilitySelection(combatterShowing.ent.equip.equippableLeft.abil2);
    }

    public void OnClickAbilityLeft1() {
        SubmitAbilitySelection(combatterShowing.ent.equip.equippableLeft.abil1);
    }

    public void OnClickAbilityArmour() {
        SubmitAbilitySelection(combatterShowing.ent.equip.equippableArmour.abil);
    }

    public void OnClickAbilityRight1() {
        SubmitAbilitySelection(combatterShowing.ent.equip.equippableRight.abil1);
    }

    public void OnClickAbilityRight2() {
        SubmitAbilitySelection(combatterShowing.ent.equip.equippableRight.abil2);
    }


    public void SetCombatterShowing(Combatter _combatterShowing) {

        combatterShowing = _combatterShowing;

        if(combatterShowing == null) {
            //Hide if we shouldn't show anything
            hideablePanel.Hide();

        } else {
            //Unhide and display this nonnull combatter
            hideablePanel.Show();

            UpdateButtonLabels();
        }

    }
    
    public void DisplayAbilityButton(Text txtAbility, Ability abil) {
        txtAbility.gameObject.SetActive(true);
        txtAbility.text = abil.sName;
    }

    public void HideAbilityButton(Text txtAbility) {
        txtAbility.gameObject.SetActive(false);
    }

    public void UpdateButtonLabels() {

        if(combatterShowing.ent.equip.equippableLeft != null) {
            DisplayAbilityButton(txtAbilLeft1, combatterShowing.ent.equip.equippableLeft.abil1);
            DisplayAbilityButton(txtAbilLeft2, combatterShowing.ent.equip.equippableLeft.abil2);
        } else {
            HideAbilityButton(txtAbilLeft1);
            HideAbilityButton(txtAbilLeft2);
        }

        if (combatterShowing.ent.equip.equippableArmour != null) {
            DisplayAbilityButton(txtAbilArmour, combatterShowing.ent.equip.equippableArmour.abil);
        } else {
            HideAbilityButton(txtAbilArmour);
        }

        if (combatterShowing.ent.equip.equippableRight != null) {
            DisplayAbilityButton(txtAbilRight1, combatterShowing.ent.equip.equippableRight.abil1);
            DisplayAbilityButton(txtAbilRight2, combatterShowing.ent.equip.equippableRight.abil2);
        } else {
            HideAbilityButton(txtAbilRight1);
            HideAbilityButton(txtAbilRight2);
        }
    }
}
