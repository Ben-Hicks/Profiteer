using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public float fMinDist;
    public float fScrollSpeed;
    public float fZoomSpeed;

    public GameObject goFocus;

    public Vector3 v3Target;
    public float fTargetZoom;
    public Vector3 v3DragStart;

    // Start is called before the first frame update
    void Start() {
        v3Target = this.transform.position;
    }

    public void SetFocus(GameObject _goFocus) {
        goFocus = _goFocus;
    }

    public void MoveTowardTarget() {

        if (Vector3.SqrMagnitude(v3Target - this.transform.position) > fMinDist) {
            this.transform.position = Vector3.Lerp(this.transform.position, v3Target, 0.1f);
        }
        if (Mathf.Abs(fTargetZoom - Camera.main.orthographicSize) > fMinDist) {
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, fTargetZoom, 0.9f);
        }
    }

    public void HandleDrag() {

        if (Input.GetMouseButtonDown(2)) {
            v3DragStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        goFocus = null;

        Vector3 v3CurMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 v3DragDelta = v3CurMousePos - v3DragStart;

        float x = this.transform.position.x;
        float y = this.transform.position.y;
        float z = this.transform.position.z;

        x -= v3DragDelta.x;
        y -= v3DragDelta.y;

        this.transform.position = new Vector3(x, y, z);
        v3Target = this.transform.position;
    }

    public void HandleZoom() {

        fTargetZoom += fZoomSpeed * (-Input.mouseScrollDelta.y) * Time.fixedDeltaTime;

        if (Mathf.Abs(fTargetZoom - Camera.main.orthographicSize) > fMinDist) {
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, fTargetZoom, 0.9f);
        }
    }

    public void UpdateTarget() {

        if (goFocus != null) {
            v3Target = goFocus.transform.position;
        }

        float x = v3Target.x;
        float y = v3Target.y;
        float z = this.transform.position.z;

        if (Input.GetKey(KeyCode.UpArrow)) {
            y += fScrollSpeed * Time.fixedDeltaTime;
            goFocus = null;
        }
        if (Input.GetKey(KeyCode.RightArrow)) {
            x += fScrollSpeed * Time.fixedDeltaTime;
            goFocus = null;
        }
        if (Input.GetKey(KeyCode.DownArrow)) {
            y -= fScrollSpeed * Time.fixedDeltaTime;
            goFocus = null;
        }
        if (Input.GetKey(KeyCode.LeftArrow)) {
            x -= fScrollSpeed * Time.fixedDeltaTime;
            goFocus = null;
        }

        v3Target = new Vector3(x, y, z);
        
    }

    // Update is called once per frame
    void Update() {

        if (Input.GetMouseButton(2)) {
            HandleDrag();
        } else {
            UpdateTarget();

            MoveTowardTarget();
        }

        HandleZoom();
        
        
    }
}
