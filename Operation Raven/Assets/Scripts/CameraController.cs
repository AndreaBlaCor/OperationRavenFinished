using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject followPlayer;
    //public float moveSpeed = 100;
    public float height;
    public float distance;
    public float radio;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   float radioRad = (radio + followPlayer.transform.eulerAngles.y) * Mathf.Deg2Rad;
        float px = followPlayer.transform.position.x + (distance * Mathf.Sin(radioRad));
        float pz = followPlayer.transform.position.z + (distance * Mathf.Cos(radioRad));
        float py = followPlayer.transform.position.y + height;

        transform.position = new Vector3(px, py, pz);
        transform.LookAt(followPlayer.transform.position);               
    }
}


