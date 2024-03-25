using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tile : MonoBehaviour {

    public TextMeshProUGUI txtCoordsLabel;
    public TextMeshProUGUI txtTileTypeLabel;
    public TextMeshProUGUI txtDebugLabel;
    public SpriteRenderer sprren;

    public Coordinates coords;

    public TileInfo tileinfo;
    public Entity ent;

    public Tile[] arAdj;
    public Tile U {
        get { return arAdj[(int)Dir.U]; }
        set { arAdj[(int)Dir.U] = value; }
    }
    public Tile UR {
        get { return arAdj[(int)Dir.UR]; }
        set { arAdj[(int)Dir.UR] = value; }
    }
    public Tile DR {
        get { return arAdj[(int)Dir.DR]; }
        set { arAdj[(int)Dir.DR] = value; }
    }
    public Tile D {
        get { return arAdj[(int)Dir.D]; }
        set { arAdj[(int)Dir.D] = value; }
    }
    public Tile DL {
        get { return arAdj[(int)Dir.DL]; }
        set { arAdj[(int)Dir.DL] = value; }
    }
    public Tile UL {
        get { return arAdj[(int)Dir.UL]; }
        set { arAdj[(int)Dir.UL] = value; }
    }

    public void InitCoords(int x, int y) {
        coords = new Coordinates(x, y);

        gameObject.transform.localPosition = new Vector3(x * Map.Get().fTileWidth, 
             y * Map.Get().fTileHeight + (x % 2 == 0 ? 0 : 0.5f), 0f);
        
    }

    public void OnMouseEnter() {
        InfoPanel.Get().SetInfo(this);
    }

    public void OnMouseExit() {
        InfoPanel.Get().ClearInfo();
    }


    public override string ToString() {
        return coords.ToString();
    }

    public void UpdateTileVisuals() {
        //txtTileTypeLabel.text = tileinfo.arnPropertyValues[(int)TileInfoProperties.Elevation].ToString();
        //txtDebugLabel.text = tileinfo.arnPropertyValues[(int)TileInfoProperties.Temperature].ToString();
        txtCoordsLabel.text = coords.ToString();

        DisplayBiome();
        txtDebugLabel.text = string.Format("{0} ({1})", tileinfo.nColumnAccordingToThread, tileinfo.iThreadMadeBy);
    }

    public void DisplayBiome() {
        sprren.color = MapGenerator.Get().GetBiomeColour(tileinfo.biometype);
    }

    public void DisplayProperty(TileInfoProperties property) {
        if (tileinfo.arnPropertyValues == null) return;

        sprren.color = MapGenerator.Get().GetPropertyColour(property, tileinfo.arnPropertyValues[(int)property]);
    }

    public void Update() {
        if (Input.GetKeyUp(KeyCode.Space)) {
            DisplayBiome();
        }else if (Input.GetKeyUp(KeyCode.Alpha1)) {
            DisplayProperty(TileInfoProperties.Elevation);
        } else if (Input.GetKeyUp(KeyCode.Alpha2)) {
            DisplayProperty(TileInfoProperties.Wetness);
        } else if(Input.GetKeyUp(KeyCode.Alpha3)) {
            DisplayProperty(TileInfoProperties.Temperature);
        } else if (Input.GetKeyUp(KeyCode.Alpha4)) {
            DisplayProperty(TileInfoProperties.Hospitableness);
        }
    }
}
