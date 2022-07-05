using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
    [SerializeField] GameObject[] floorPrefabs ;
    [SerializeField] public GameObject player;
    void Start()
    {
        
    }
    public void SpawnFloor()
    {
        int r = Random.Range(0, floorPrefabs.Length);
       
        GameObject floor = Instantiate(floorPrefabs[r],transform);
        floor.transform.position = new Vector3(Random.Range(-3.03f, 3.03f), -5.5f, 0f);
    }
}
