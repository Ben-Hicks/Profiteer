using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityController : Singleton<EntityController> {

    public Entity entPlayer;

    public List<Entity> lstAllEntities;
    public List<Entity> lstEnemyEntities;

    public void RegisterEnt(Entity ent) {

        if(lstAllEntities == null) {
            lstAllEntities = new List<Entity>();
            lstEnemyEntities = new List<Entity>();
        }

        lstAllEntities.Add(ent);
        ent.SetId(lstAllEntities.Count);

        if (ent.entType == EntityFactory.EntType.Player) {
            RegisterEntPlayer(ent);
        } else {
            RegisterEntEnemy(ent);
        }
    }

    public void RegisterEntPlayer(Entity _entPlayer) {
        if(entPlayer != null) {
            Debug.LogError("Trying to set EntPlayer when we already have one!");
            return;
        }
        entPlayer = _entPlayer;

        //Do some special setup since this is our player entity
        StatsPanel.Get().SetEntity(entPlayer);
        InventoryPanel.Get().SetInventory(entPlayer.inv);
    }

    public void RegisterEntEnemy(Entity _entEnemy) {
        lstEnemyEntities.Add(_entEnemy);
    }

    public override void Init() {
        lstAllEntities = new List<Entity>();
        lstEnemyEntities = new List<Entity>();
    }
}
