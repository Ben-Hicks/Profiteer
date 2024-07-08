using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationController : Singleton<NotificationController> {

    public GameObject pfPanelNotification;

    public void SpawnNotification(string sDescription, params (string, System.Action)[] arpairChoiceOptions) {

        GameObject goNewNotification = GameObject.Instantiate(pfPanelNotification, this.transform);
        NotificationPanel newNotificationPanel = goNewNotification.GetComponent<NotificationPanel>();

        newNotificationPanel.InitNotification(sDescription, arpairChoiceOptions);
    }

    public override void Init() {
        
    }
}
