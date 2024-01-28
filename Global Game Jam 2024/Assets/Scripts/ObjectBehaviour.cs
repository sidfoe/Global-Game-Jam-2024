using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBehaviour : MonoBehaviour
{
    public int gender = -1; //boy, girl, gender neutral
    public int age = -1; //5yr, 7yr, 10yr, 12yr, 16yr, office worker
    public int type = -1; //wig, shoes, nose, pants, prop

    public GameObject spawnPoint;

    private void Start()
    {
        spawnPoint = gameObject.transform.parent.gameObject;
    }
}
