using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Entity : MonoBehaviour {

    public static readonly Vector3 fEntityOffsetZ = new Vector3(0, 0, -0.1f);

    public TextMeshProUGUI txtEntityType;
    public TextMeshProUGUI txtDebug;

    public TileTerrain tile;
    public int id;

    //temp
    public float fTimeSinceMove;

    public static readonly Vector3 v3PosOffset = new Vector3(0f, 0f, -0.5f);

    public void SetId(int _id) {
        id = _id;

        txtDebug.text = id.ToString();
    }

    public void SetTile(TileTerrain _tile) {

        Debug.LogFormat("Setting Entity {0} from {1} to {2}", id, tile, _tile);

        if(tile != null) {
            tile.ent = null;
        }

        tile = _tile;
        tile.ent = this;
        
    }

    public void MoveToTile(TileTerrain _tile) {
        if (_tile == null) {
            Debug.LogError("Cannot move to a null tile");
            return;
        }

        MovementController.Get().MoveEntToTile(this, _tile);

        fTimeSinceMove = 0f;
    }


    public void InitOnTile(TileTerrain _tile) {
        SetTile(_tile);
        this.transform.position = _tile.v3WorldPosition + Entity.fEntityOffsetZ;
    }

    public override string ToString() {
        return string.Format("Entity-{0}", id);
    }

    public void OnMouseEnter() {
        Debug.LogFormat("Entered Entity {0}", ToString());
        InfoPanel.Get().SetInfo(ToString());
    }

    public void OnMouseExit() {
        Debug.LogFormat("Left Entity {0}", ToString());
        InfoPanel.Get().ClearInfo();
    }

    //Temp
    public void Update() {
        fTimeSinceMove += Time.deltaTime;

        if (fTimeSinceMove > 0.3f) {
            if (Input.GetKeyUp(KeyCode.Q)) {
                MoveToTile(Map.Get().GetTileInDir(tile, Dir.UL));
            } else if (Input.GetKeyUp(KeyCode.W)) {
                MoveToTile(Map.Get().GetTileInDir(tile, Dir.U));
            } else if (Input.GetKeyUp(KeyCode.E)) {
                MoveToTile(Map.Get().GetTileInDir(tile, Dir.UR));
            } else if (Input.GetKeyUp(KeyCode.D)) {
                MoveToTile(Map.Get().GetTileInDir(tile, Dir.DR));
            } else if (Input.GetKeyUp(KeyCode.S)) {
                MoveToTile(Map.Get().GetTileInDir(tile, Dir.D));
            } else if (Input.GetKeyUp(KeyCode.A)) {
                MoveToTile(Map.Get().GetTileInDir(tile, Dir.DL));
            }
        }

    }
}
