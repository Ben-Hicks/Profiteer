using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EntityFactory {

    public enum EntType { Player, Wolf, Rabbit, LENGTH }
   

    public static void SetInfoAndInput(Entity ent, EntityInfo entInfo, EntityInput entInput) {

        ent.entinfo = entInfo;
        ent.entinput = entInput;

        entInfo.ent = ent;
        entInput.ent = ent;
        entInput.entinfo = entInfo;

        ent.entinfo.InitEntityInfo();
        ent.entinput.InitEntityInput();
    }

    public static void InitEntity(Entity ent, EntType entType) {

        EntityInfo entInfo = ent.gameObject.AddComponent<EntityInfo>();
        EntityInput entInput = null;

        switch (entType) {

            case EntType.Player:
                entInput = ent.gameObject.AddComponent<ManualInput>();

                entInfo.sName = "Player";

                entInfo.nMaxEnergy = 100;
                entInfo.nMaxTurnsBeforeResting = 100;
                entInfo.nSightRange = 5;
                entInfo.nInvestigation = 10;

                entInfo.dictTags = new DictTags(("Humanoid", true));

                break;

            case EntType.Wolf:
                entInput = ent.gameObject.AddComponent<AIEntityPredator>();

                entInfo.sName = "Wolf";

                entInfo.nMaxEnergy = 80;
                entInfo.nMaxTurnsBeforeResting = 3;
                entInfo.nSightRange = 15;
                entInfo.nInvestigation = 9;

                entInfo.dictTags = new DictTags(("Predator", true));

                break;

            case EntType.Rabbit:
                entInput = ent.gameObject.AddComponent<AIEntityHerbivore>();

                entInfo.sName = "Rabbit";

                entInfo.nMaxEnergy = 100;
                entInfo.nMaxTurnsBeforeResting = 2;
                entInfo.nSightRange = 3;
                entInfo.nInvestigation = 5;

                entInfo.dictTags = new DictTags(("Herbavore", true));

                break;

            default:
                Debug.LogErrorFormat("Cannot create an entity of unsupported type {0}", entType);

                break;
        }

        SetInfoAndInput(ent, entInfo, entInput);


    }
}
