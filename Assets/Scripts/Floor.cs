using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2f;

    private float score;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        score = (transform.parent.GetComponent<FloorManager>().player.GetComponent<Player>().score)/10 + moveSpeed;
        transform.Translate(0,score*Time.deltaTime,0);
        if (transform.position.y > 6f)
        {
            Destroy(gameObject);
            transform.parent.GetComponent<FloorManager>().SpawnFloor();
        }
    }
}
