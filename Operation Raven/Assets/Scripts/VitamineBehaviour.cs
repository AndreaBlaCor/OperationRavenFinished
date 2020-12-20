using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VitamineBehaviour : MonoBehaviour
{
    private GameObject mainLogic;

    public AudioClip codeGreen;
    


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
        if (collision.gameObject.tag == "Player"){
            mainLogic.GetComponent<MainLogic>().refillEnergy(20);
            collision.gameObject.GetComponent<AudioSource>().PlayOneShot(codeGreen);
            Destroy(gameObject);
            
        }
    }
 }
