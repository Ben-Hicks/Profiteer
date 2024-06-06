using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSearchables {

    private LinkedList<Searchable> llstSearchables;



    public void AddSearchable(Searchable searchable) {

        LinkedListNode<Searchable> nodeCur = llstSearchables.First;

        while (nodeCur != null) {


            if (searchable.nSearchDifficulty <= nodeCur.Value.nSearchDifficulty) {
                //Then this is the spot we can insert our new node
                llstSearchables.AddBefore(nodeCur, searchable);
                return;
            }
            nodeCur = nodeCur.Next; 
        }

            llstSearchables.AddLast(searchable);
    }

    public void RemoveSearchable(Searchable searchable) {

        llstSearchables.Remove(searchable);

    }

    public Searchable FindHardestSearchable(Entity ent) {

        LinkedListNode<Searchable> bestFound = llstSearchables.First;

        while(bestFound.Next != null ||  ent.entinfo.nInvestigation >= bestFound.Value.nSearchDifficulty) {
            bestFound = bestFound.Next; 
        }

        return bestFound.Value;
    }

    public void PerformSearch(Entity ent) {

        Searchable searchableFound = FindHardestSearchable(ent);

        if (searchableFound.bRemovedWhenSearched) {
            RemoveSearchable(searchableFound);
        }

        Debug.LogFormat("Executing Searchable {0}", searchableFound.sName);

        searchableFound.actEffect(ent);

    }

}
