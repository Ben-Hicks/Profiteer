using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceOptionPanel : MonoBehaviour {

    public NotificationPanel notificationpanel;
    public Text textLabel;
    public System.Action fnExecuteChoiceOption;

    public void OnSelectOption() {
        if (notificationpanel.bOptionSelected) return;
        notificationpanel.bOptionSelected = true;

        //We can despawn our Notification Panel
        GameObject.Destroy(notificationpanel.gameObject);
        
        //Then perform whatever option was selected
        fnExecuteChoiceOption();

        
    }

    public void InitChoiceOption(NotificationPanel _notificationpanel, string sLabel, System.Action _fnExecuteChoiceOption) {
        notificationpanel = _notificationpanel;
        textLabel.text = sLabel;
        fnExecuteChoiceOption = _fnExecuteChoiceOption;
    }

}
