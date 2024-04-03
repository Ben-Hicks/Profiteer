using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Dir { U, UR, DR, D, DL, UL };

public static class Direction {

    public static readonly Dir[] arAllDirs = { Dir.U, Dir.UR, Dir.DR, Dir.D, Dir.DL, Dir.UL };
    public static readonly int[] arDirY = { 2, 1, -1, -2, -1, 1 };
    public static readonly int[] arDirX = { 0, 1, 1, 0, -1, -1 };

    public static Dir Prev(Dir d) {
        return (Dir)(((int)d - 1) % 6);
    }

    public static Dir Opposite(Dir d) {
        return (Dir)(((int)d + 3) % 6);
    }

    public static Dir Next(Dir d) {
        return (Dir)(((int)d + 1) % 6);
    }

}
