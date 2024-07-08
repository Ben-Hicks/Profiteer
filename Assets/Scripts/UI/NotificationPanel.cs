using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationPanel : MonoBehaviour {

    public GameObject pfChoiceOptionPanel;

    public GameObject goChoiceOptionContainer;

    public Text textDescription;
    public bool bOptionSelected;

    public void InitNotification(string sDescription, params (string, System.Action)[] arpairChoiceOptions) {
        bOptionSelected = false;

        textDescription.text = sDescription;

        if (arpairChoiceOptions.Length > 0) {

            foreach((string, System.Action) pair in arpairChoiceOptions) {
                GameObject goNewChoiceOption = GameObject.Instantiate(pfChoiceOptionPanel, goChoiceOptionContainer.transform);
                ChoiceOptionPanel newChoiceOptionPanel = goNewChoiceOption.GetComponent<ChoiceOptionPanel>();
                newChoiceOptionPanel.InitChoiceOption(this, pair.Item1, pair.Item2);
            }

        }


    }


}
