﻿using System.Collections;
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
                    new ItemCopperOre(3),
                    new ItemWheat(20),
                    new ItemBearSkin(6)
                    );

                ent.inv.AddAndEquipStartingEquipment(new WeaponShortsword(1), null, new ArmourPlatemail(1));

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

                ent.inv.AddAndEquipStartingEquipment(new WeaponWolfClaws(1), null, new ArmourWolfPelt(1));

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

        EntityController.Get().RegisterEnt(ent);

    }
}
