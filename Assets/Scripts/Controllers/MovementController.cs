using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : Singleton<MovementController>{
    public float fMovingTimePerTile;

    public class Path {
        public List<TileTerrain> lstTilePath;
        public float fCurProgress;
        public float fTotalProgress;

        public Path(List<TileTerrain> _lstTilePath) {
            lstTilePath = _lstTilePath;

            fTotalProgress = MovementController.Get().fMovingTimePerTile * (lstTilePath.Count - 1);
        }
    }

    public Dictionary<Entity, Path> dictEntitiesMoving;


    public override void Init() {
        dictEntitiesMoving = new Dictionary<Entity, Path>();
    }

    //returns true if movement is complete
    public bool ProgressMovingEntity(Entity ent, Path path) {

        path.fCurProgress += Time.deltaTime;

        //Which index of our path we're currently on
        int iProgress = Mathf.FloorToInt(path.fCurProgress / MovementController.Get().fMovingTimePerTile);
        float fSegmentProgress = (path.fCurProgress % MovementController.Get().fMovingTimePerTile) / MovementController.Get().fMovingTimePerTile;

        if(iProgress >= path.lstTilePath.Count - 1) {
            //Then we're done moving - just set our position to the very end of our list and take us off the moving list
            ent.transform.position = Map.Get().tilemapTerrain.GetCellCenterWorld(path.lstTilePath[path.lstTilePath.Count - 1].v3Coords) + Entity.fEntityOffsetZ;
            return true;
        } else {

            Debug.LogFormat("iProgress is {0} and we have count {1}", iProgress, path.lstTilePath.Count);
            ent.transform.position = Vector3.Lerp(
            path.lstTilePath[iProgress].v3WorldPosition,
            path.lstTilePath[iProgress + 1].v3WorldPosition,
            fSegmentProgress) + Entity.fEntityOffsetZ;
            return false;
        }
        
    }

    public void ProgressMovingAllEntities() {
        List<Entity> lstCompleted = new List<Entity>();

        foreach(KeyValuePair<Entity, Path> kvp in dictEntitiesMoving) {
            bool bDone = ProgressMovingEntity(kvp.Key, kvp.Value);
            if (bDone) lstCompleted.Add(kvp.Key);
        }

        foreach(Entity ent in lstCompleted) {
            Debug.LogFormat("Removing {0}", ent);
            dictEntitiesMoving.Remove(ent);
        }

        
    }

    public void MoveEntToTile(Entity ent, TileTerrain tileDestination) {

        if (tileDestination == null) {
            Debug.LogError("Cannot move to a null tile");
            return;
        }

        Debug.LogFormat("Moving entity {0} from {1} to {2}", ent, ent.tile, tileDestination);

        if (tileDestination.ent != null) {
            //If an entity exists where we're trying to move, then cancel the movement
            Debug.LogErrorFormat("Can't move {0} to {1} since {2} is already on that tile", ent, tileDestination, tileDestination.ent);
            return;
        }

        (Pathing.PathResultType, List<TileTerrain>) path = Pathing.FindPath(ent.tile, tileDestination, 100f);

        if (path.Item1 == Pathing.PathResultType.Unreachable) return;

        Path pathNew = new Path(path.Item2);

        //We'll instantly move them in the backend, but display them moving over time
        ent.SetTile(tileDestination);

        //TODO: Add some consideration for if our key is already in the moving dictionary
        if (dictEntitiesMoving.ContainsKey(ent)) {
            Debug.LogErrorFormat("TODO: add some contingency to extend an existing path");
        }
        dictEntitiesMoving.Add(ent, pathNew);
    }

    public void Update() {
        ProgressMovingAllEntities();
    }
}
