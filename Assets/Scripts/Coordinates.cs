using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Coordinates  {

    public int x, y;

    public Coordinates(int _x, int _y) {

        x = _x;
        y = _y;
    }

    public override string ToString() {
        return string.Format("({0},{1})", x, y);
    }
}
