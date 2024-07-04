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

                entInfo.SetLevelStats(1, 10);
                entInfo.SetCombatStats(15);
                entInfo.SetEnergyStats(100);
                entInfo.SetCoreStats(5, 5, 5, 5, 5, 5);
                entInfo.SetPerceptionStats(3, 10);

                entInfo.dictTags = new DictTags(("Humanoid", true));

                ent.inv.AddItems(
                    );

                break;

            case EntType.Wolf:
                entInput = ent.gameObject.AddComponent<AIEntityPredator>();

                entInfo.sName = "Wolf";

                entInfo.SetLevelStats(1, 10);
                entInfo.SetCombatStats(8);
                entInfo.SetEnergyStats(60);
                entInfo.SetCoreStats(5, 5, 5, 5, 5, 5);
                entInfo.SetPerceptionStats(4, 5);

                entInfo.dictTags = new DictTags(("Predator", true));

                break;

            case EntType.Rabbit:
                entInput = ent.gameObject.AddComponent<AIEntityHerbivore>();

                entInfo.sName = "Rabbit";

                entInfo.SetLevelStats(1, 10);
                entInfo.SetCombatStats(4);
                entInfo.SetEnergyStats(80);
                entInfo.SetCoreStats(5, 5, 5, 5, 5, 5);
                entInfo.SetPerceptionStats(5, 3);

                entInfo.dictTags = new DictTags(("Herbavore", true));

                break;

            default:
                Debug.LogErrorFormat("Cannot create an entity of unsupported type {0}", entType);

                break;
        }

        SetInfoAndInput(ent, entInfo, entInput);

        //As a last check, if we spawned our human, then we can let our stats know to reflect them
        if (entType == EntType.Player) {
            StatsPanel.Get().SetEntity(ent);
        }

    }
}
