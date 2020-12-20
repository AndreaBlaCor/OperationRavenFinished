using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrongFlaskBehaviour : MonoBehaviour
{
    private GameObject mainLogic;

    
    public AudioClip codeRed;

    // Start is called before the first frame update
    void Start()
    {
        mainLogic = GameObject.Find("GameLogic");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player") {
            mainLogic.GetComponent<MainLogic>().refillEnergy(-20);
            collision.gameObject.GetComponent<AudioSource>().PlayOneShot(codeRed);
            Destroy(gameObject);

        }
    }
}
