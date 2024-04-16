using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City {

    public int iCity;
    public int nFaction;
    public TileTerrain tileCenter;
    public int nPopulation;
    public CityType citytype;
    public List<TileTerrain> lstTiles;


    public City(int _iCity, TileTerrain _tileCenter, int _nPopulation) {
        iCity = _iCity;
        tileCenter = _tileCenter;
        nPopulation = _nPopulation;

        lstTiles = new List<TileTerrain>();
    }

    public void SetFaction(int _nFaction) {
        nFaction = _nFaction;
    }

    public void AddToCity(TileTerrain tileNew) {
        lstTiles.Add(tileNew);
    }




}
