using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Targetter {

    public delegate bool FnTarget(Combatter source, Combatter target);
    
    public static bool IsAlly(Combatter source, Combatter target) {
        return source.team == target.team;
    }

    public static bool IsEnemy(Combatter source, Combatter target) {
        return source.team != target.team;
    }
    public static bool IsSelf(Combatter source, Combatter target) {
        return source == target;
    }
    public static bool IsOther(Combatter source, Combatter target) {
        return source != target;
    }


    public static FnTarget AND(FnTarget fn1, FnTarget fn2) {
        return (Combatter source, Combatter target) => {
            return fn1(source, target) && fn2(source, target);
        };
    }

    public static FnTarget OR(FnTarget fn1, FnTarget fn2) {
        return (Combatter source, Combatter target) => {
            return fn1(source, target) || fn2(source, target);
        };
    }
}
