using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasRenderer))]
public class HideablePanel : MonoBehaviour{

    public CanvasRenderer rend;

    public void Hide() {
        Debug.Log("Hiding");
        gameObject.SetActive(false);
    }

    public void Show() {
        Debug.Log("Showing");
        gameObject.SetActive(true);
    }

}
