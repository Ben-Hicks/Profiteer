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

    
    public void InitCoords(int y, int x) {
        coords = new Coordinates(y, x);

        gameObject.transform.localPosition = new Vector3(x * Map.Get().fTileWidth, y * Map.Get().fTileHeight, 0f);

        UpdateTileVisuals();
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
        txtTileTypeLabel.text = tileinfo.arnPropertyValues[(int)TileInfoProperties.Elevation].ToString();
        txtDebugLabel.text = tileinfo.arnPropertyValues[(int)TileInfoProperties.Temperature].ToString();
        txtCoordsLabel.text = coords.ToString();

        DisplayBiome();
    }

    public void DisplayBiome() {
        sprren.color = MapGenerator.Get().GetBiomeColour(tileinfo.biometype);
    }

    public void DisplayProperty(TileInfoProperties property) {
        txtDebugLabel.text = tileinfo.arnPropertyValues[(int)property].ToString();

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
