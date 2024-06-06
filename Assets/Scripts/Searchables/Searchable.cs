using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Searchable {

    public TileTerrain tile;

    public string sName;

    public int nSearchDifficulty;
    public bool bRemovedWhenSearched;

    public string sTextEffect;
    public System.Action<Entity> actEffect;

    public Searchable(TileTerrain _tile, string _sName, int _nSearchDifficulty,
        string _sTextEffect, System.Action<Entity> _actEffect) {
        tile = _tile;
        sName = _sName;
        nSearchDifficulty = _nSearchDifficulty;
        sTextEffect = _sTextEffect;
        actEffect = _actEffect;
    }

}
