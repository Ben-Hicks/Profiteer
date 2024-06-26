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
    public IEnumerator ProgressMovingEntity(Entity ent, Path path) {

        while (true) {
            path.fCurProgress += Time.deltaTime;

            //Which index of our path we're currently on
            int iProgress = Mathf.FloorToInt(path.fCurProgress / MovementController.Get().fMovingTimePerTile);
            float fSegmentProgress = (path.fCurProgress % MovementController.Get().fMovingTimePerTile) / MovementController.Get().fMovingTimePerTile;

            if (iProgress >= path.lstTilePath.Count - 1) {
                //Then we're done moving - just set our position to the very end of our list and take us off the moving list
                ent.transform.position = Map.Get().tilemapTerrain.GetCellCenterWorld(path.lstTilePath[path.lstTilePath.Count - 1].v3Coords) + Entity.fEntityOffsetZ;

                yield break;
            } else {

                //Debug.LogFormat("iProgress is {0} and we have count {1}", iProgress, path.lstTilePath.Count);
                ent.transform.position = Vector3.Lerp(
                path.lstTilePath[iProgress].v3WorldPosition,
                path.lstTilePath[iProgress + 1].v3WorldPosition,
                fSegmentProgress) + Entity.fEntityOffsetZ;

                yield return null;
            }
        }
        
    }


    /*
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
    */

    //
    public IEnumerator MoveEntToTile(Entity ent, TileTerrain tileDestination, int nDestinationRange = 0) {
        
        if (tileDestination == null) {
            Debug.LogError("Cannot move to a null tile");
            yield break;
        }

        Debug.LogFormat("Moving entity {0} from {1} toward {2}", ent, ent.tile, tileDestination);


        (Pathing.PathResultType, List<TileTerrain>, int) path = Pathing.FindPath(ent.tile, tileDestination, ent.entinfo.nCurEnergy.Get(), nDestinationRange);


        //Check if we could possibly get within the range we wanted
        if (path.Item1 == Pathing.PathResultType.Unreachable) {
            Debug.LogFormat("No path from {0} to {1} exists", ent.tile, tileDestination);
            yield break;
        }
        
        Debug.LogFormat("The path we found has length {0} and cost {1}", path.Item2.Count, path.Item3);

        if (ent.entinfo.CanPayEnergy(path.Item3) == false) {
            Debug.LogErrorFormat("Cannot pay the cost ({0}) for the generated path", path.Item3);
            yield break;
        }

        ent.entinfo.PayEnergy(path.Item3);
        Debug.LogFormat("{0} paid {1} energy to move, so now they're at {2}", ent, path.Item3, ent.entinfo.nCurEnergy);

        Path pathNew = new Path(path.Item2);

        //We'll instantly move them in the backend, but display them moving over time
        ent.SetTile(pathNew.lstTilePath[pathNew.lstTilePath.Count - 1]);

        //TODO: Add some consideration for if our key is already in the moving dictionary
        /*if (dictEntitiesMoving.ContainsKey(ent)) {
            Debug.LogErrorFormat("TODO: add some contingency to extend an existing path");
        }
        dictEntitiesMoving.Add(ent, pathNew);*/

        yield return ProgressMovingEntity(ent, pathNew);
    }

    public void Update() {
        //ProgressMovingAllEntities();
    }
}
