using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
    [SerializeField] GameObject[] floorPrefabs ;
    void Start()
    {
        Debug.Log(floorPrefabs.Length);
    }
    public void SpawnFloor()
    {
        int r = Random.Range(0, floorPrefabs.Length);
        GameObject floor = Instantiate(floorPrefabs[r],transform);
        floor.transform.position = new Vector3(Random.Range(-3.03f, 4.04f), -5.5f, 0f);
    }
}
