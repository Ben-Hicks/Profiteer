using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSearchables {

    public TileTerrain tile;
    private LinkedList<Searchable> llstSearchables;

    public TileSearchables(TileTerrain _tile, params Searchable[] arSearchables) {

        tile = _tile;
        llstSearchables = new LinkedList<Searchable>();

        //Always add in a base Searchable that is just a failure to find anything
        AddSearchable(SearchablesFactory.CreateDefaultSearchable(tile));

        foreach(Searchable searchable in arSearchables) {
            AddSearchable(searchable);
        }
    }

    //We'll keep our Searchables sorted in increasing order of difficulty
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

    // We'll scan through increasingly difficult searchables until we find that the next one is too difficult
    public Searchable FindHardestSearchable(Entity ent) {

        LinkedListNode<Searchable> bestFound = llstSearchables.First;

        Debug.LogFormat("Searching for the hardest searchable in {0} that is under {1}", this, ent.entinfo.nInvestigation);

        while(bestFound.Next != null &&  ent.entinfo.nInvestigation.Get() >= bestFound.Next.Value.nSearchDifficulty) {
            bestFound = bestFound.Next; 
        }

        Debug.LogFormat("Found hardest searchable of difficulty {0}", bestFound.Value.nSearchDifficulty);

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

    public override string ToString() {
        string s = tile.ToString() + "'s Searchables: ";


        LinkedListNode<Searchable> curNode = llstSearchables.First;

        while(curNode != null) {
            s += curNode.Value.nSearchDifficulty + " ";

            curNode = curNode.Next;
        }

        return s;
    }

}
