using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHandler : MonoBehaviour
{
    public List<GameObject> items = new List<GameObject>();
    public List<GameObject> spawns = new List<GameObject>();

    void Start()
    {
        int randItem;
        int randSpawn;
        while(items.Count > 0)
        {
            randItem = Random.Range(0, items.Count);
            randSpawn = Random.Range(0, spawns.Count);

            Instantiate(items[randItem], spawns[randSpawn].transform);

            items.RemoveAt(randItem);
            spawns.RemoveAt(randSpawn);
        }
    }
}
