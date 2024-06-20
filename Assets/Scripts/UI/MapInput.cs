using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapInput : Singleton<MapInput> {

    public Map map;

    public Vector3 v3OldMousePos;
    public TileTerrain tileHovering;
    public Entity entHovering;

    public TileTerrain tileFocused;

    public Subject subRightClick = new Subject();
    public Subject subTileClick = new Subject();
    public Subject subTileHoverChange = new Subject();

    public Subject subEntHoverChange = new Subject();


    public override void Init() {

        map = Map.Get();
    }

    public void StartEntityHover(Entity _ent) {
        if(_ent == null) {
            return;
        }

        SetEntityHover(_ent);
    }

    public void StopEntityHover(Entity _ent) {
        if(_ent == null) {
            return;
        }

        if (_ent != entHovering) {
            return;
        }

        SetEntityHover(null);
    }

    private void SetEntityHover(Entity _entHovering) {

        if (entHovering == _entHovering) return;

        entHovering = _entHovering;
        
        subEntHoverChange.NotifyObs(null, entHovering);
        
    }

    public void SetTileHover(TileTerrain _tileHovering) {

        if (tileHovering == _tileHovering) return;

        tileHovering = _tileHovering;
        
        subTileHoverChange.NotifyObs(null, tileHovering);
        
    }

    public void OnClickTile(TileTerrain _tileClicked) {

        if(tileFocused == _tileClicked) {
            //We can clear our focus
            tileFocused.Unhighlight();
            tileFocused = null;

        } else {
            //Then we have a new focus
            if(tileFocused != null) {
                tileFocused.Unhighlight();
            }

            //Update our focus
            tileFocused = _tileClicked;

            tileFocused.Highlight();
        }

        subTileClick.NotifyObs(null, tileHovering);
    }

    public void OnRightClickTile(TileTerrain _tileClicked) {
        if(tileFocused != null) {
            tileFocused.Unhighlight();
        }

        tileFocused = null;

        subRightClick.NotifyObs();
    }

    public void Update() {


        if (v3OldMousePos != Input.mousePosition) {
            Vector3 v3MouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector3Int v3NewTilePosHovering = map.tilemapTerrain.WorldToCell(v3MouseWorldPos);

            SetTileHover(map.GetTile(v3NewTilePosHovering.y, v3NewTilePosHovering.x));

            v3OldMousePos = Input.mousePosition;
        }

        if (EventSystem.current.IsPointerOverGameObject() == false) {

            if (Input.GetMouseButtonUp(0)) {
                if (tileHovering != null) {
                    OnClickTile(tileHovering);
                }
            }

            if (Input.GetMouseButtonUp(1)) {
                OnRightClickTile(tileHovering);
            }

        }
    }

}
