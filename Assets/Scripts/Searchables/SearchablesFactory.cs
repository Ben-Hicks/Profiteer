using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Can add constructors for shared searchable results that are used in a few different scenarios
public static class SearchablesFactory {

    public static Searchable CreateDefaultSearchable(TileTerrain tileterrain) {
        return new Searchable(tileterrain, "Nothing Found", -10000, "You found nothing", 
            (Entity ent) => { Debug.LogFormat("{0} found nothing", ent); });
    }
}
