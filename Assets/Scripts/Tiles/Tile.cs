using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tile : MonoBehaviour {

    public TextMeshProUGUI txtCoordsLabel;
    public TextMeshProUGUI txtTileTypeLabel;
    public TextMeshProUGUI txtDebugLabel;
    

    public Coordinates coords;

    public Entity ent;

    
    public void InitCoords(int y, int x) {
        coords = new Coordinates(y, x);

        gameObject.transform.position = new Vector3(x * Map.Get().fTileWidth, y * Map.Get().fTileHeight, 0f);

        txtCoordsLabel.text = coords.ToString();
    }


    public override string ToString() {
        return coords.ToString();
    }
}
