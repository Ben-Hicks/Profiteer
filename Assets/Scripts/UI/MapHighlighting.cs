using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapHighlighting : Singleton<MapHighlighting> {

    public HashSet<TileTerrain> setTilesHighlighted;

    public void SetSingleHighlight(TileTerrain tile) {
        ClearAllHighlighting();

        tile.Highlight();
        setTilesHighlighted.Add(tile);
    }

    public void SetAllHighlighting(ICollection<TileTerrain> setToHighlight) {
        ClearAllHighlighting();

        foreach(TileTerrain tile in setToHighlight) {
            Debug.LogFormat("Want to highlight {0}", tile);
            tile.Highlight();
            setTilesHighlighted.Add(tile);
        }
    }

    public void ClearAllHighlighting() {
        foreach(TileTerrain tile in setTilesHighlighted) {
            tile.Unhighlight();
        }
        setTilesHighlighted.Clear();
    }

    public override void Init() {
        setTilesHighlighted = new HashSet<TileTerrain>();
    }
}
