using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coordinates  {

    public int y, x;

    public Coordinates(int _y, int _x) {
        if((_y + _x)%2 == 1) {
            Debug.LogErrorFormat("ERROR!: cannot make coordinate ({0}, {1})", _y, _x);
            return;
        }
        y = _y;
        x = _x;
    }

    public override string ToString() {
        return string.Format("({0},{1})", y, x);
    }
}
