using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Entity : MonoBehaviour {

    public TextMeshProUGUI txtEntityType;
    public TextMeshProUGUI txtDebug;

    public Tile tile;
    public int id;

    //temp
    public float fTimeSinceMove;

    public static readonly Vector3 v3PosOffset = new Vector3(0f, 0f, -0.5f);

    public void SetId(int _id) {
        id = _id;

        txtDebug.text = id.ToString();
    }

    public void SetTile(Tile _tile) {
        if (tile != null && tile.ent != this) Debug.LogError("Mismatch on tile.ent!");

        Debug.LogFormat("Setting Entity {0} from {1} to {2}", id, tile, _tile);

        tile = _tile;
        tile.ent = this;


        gameObject.transform.position = tile.transform.position + v3PosOffset;
    }

    public void MoveToTile(Tile _tile) {
        if (_tile == null) {
            Debug.LogError("Cannot move to a null tile");
            return;
        }

        Debug.LogFormat("Moving entity {0} from {1} to {2}", id, tile, _tile);

        if (_tile.ent != null) {
            //If an entity exists where we're trying to move, then cancel the movement

            /* could swap if we wanted
            Debug.LogFormat("_tile {0} has entity {1} that we need to swap with", _tile, _tile.ent.id);
            _tile.ent.SetTile(tile);
            */
            return;
        } else {
            //If nothing's moving onto our tile, then clear out it's entity
            tile.ent = null;
        }
        SetTile(_tile);

        fTimeSinceMove = 0f;
    }


    public void InitOnTile(Tile _tile) {
        SetTile(_tile);
    }

    public override string ToString() {
        return string.Format("Entity {0}", id);
    }

    public void OnMouseEnter() {
        Debug.LogFormat("Entered Entity {0}", id);
        InfoPanel.Get().SetInfo(ToString());
    }

    public void OnMouseExit() {
        Debug.LogFormat("Left Entity {0}", id);
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
