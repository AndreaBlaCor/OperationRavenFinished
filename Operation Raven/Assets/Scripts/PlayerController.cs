using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Speeds - Movement of the player
    public float tspeed = 10;
    float speed;
       
    public float rotateSpeed = 60.0f;
    private GameObject mainLogic;

    //Limits
    private float leftLimit = 22.0f;
    private float topLimit = 85.0f;
    private float rightLimit = -68.0f;
    private float bottomLimit = -6.0f;

    //Animations
    Animator playerAnim;

    //Audios    
    public AudioSource codePlayer;


    public AudioClip robotDead;
    public AudioClip robotOn;

    // Start is called before the first frame update
    void Start()
    {
        mainLogic = GameObject.Find("GameLogic");
        speed = tspeed;
        playerAnim = gameObject.GetComponent<Animator>();
        playerAnim.SetBool("Open_Anim", true);
        codePlayer = GetComponent<AudioSource>();
        codePlayer.PlayOneShot(robotOn,1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        /*Walk and animate player while alive*/
        if ((mainLogic.GetComponent<MainLogic>().ifDead == false) && (mainLogic.GetComponent<MainLogic>().ifWinner == false))
        {

            float verticalInput = Input.GetAxis("Vertical");
            float horizontalInput = Input.GetAxis("Horizontal");

            /*if vertical inputs aren't clicked, no walk animation*/
            if (verticalInput == 0)
            {
                playerAnim.SetBool("Walk_Anim", false);
            }

            /*if intput for movement, walk*/
            else
            {
                playerAnim.SetBool("Walk_Anim", true);
            }

            /*movement*/
            transform.Rotate(Vector3.up * rotateSpeed * horizontalInput * Time.deltaTime);
            transform.Translate(Vector3.forward * speed * verticalInput * Time.deltaTime);

            /*limits so the player stays within the laberynth*/
            if (transform.position.x > topLimit)
            {
                transform.position = new Vector3(topLimit, transform.position.y, transform.position.z);
            }

            if (transform.position.x < bottomLimit)
            {
                transform.position = new Vector3(bottomLimit, transform.position.y, transform.position.z);
            }

            if (transform.position.z > leftLimit)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, leftLimit);
            }

            if (transform.position.z < rightLimit)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, rightLimit);
            }
        }

        /*Stop walking animation if player's dead*/
        if (mainLogic.GetComponent<MainLogic>().ifDead == true)
        {
            playerAnim.SetBool("Walk_Anim", false);
            codePlayer.PlayOneShot(robotDead,1.0f);
        }

        if (mainLogic.GetComponent<MainLogic>().ifWinner == true) {
            playerAnim.SetBool("Walk_Anim", false);
        }
    }

    //I was having errors with the walls callider's, this was the solution I came up with to fix it
    void OnCollisionEnter(Collision collision)
    {
       // Debug.Log(collision.transform.name);
        if (collision.transform.tag == "Wall")
        {
            speed = 0;
        }
        
        if (collision.gameObject.CompareTag("Obj1"))
        {
            mainLogic.GetComponent<MainLogic>().setSign(1);
            mainLogic.GetComponent<MainLogic>().addGem();            
            Destroy(collision.gameObject);
            
        }

        if (collision.gameObject.CompareTag("Obj2"))
        {
            mainLogic.GetComponent<MainLogic>().setSign(2);
            mainLogic.GetComponent<MainLogic>().addGem();
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Obj3"))
        {
            mainLogic.GetComponent<MainLogic>().setSign(3);
            mainLogic.GetComponent<MainLogic>().addGem();
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Obj4"))
        {
            mainLogic.GetComponent<MainLogic>().setSign(4);
            mainLogic.GetComponent<MainLogic>().addGem();
            Destroy(collision.gameObject);
        }
    }

    void OnCollisionStay (Collision collision)
    {
        //Debug.Log(collision.transform.name);
        if (collision.transform.tag == "Wall")
        {
            speed = -1*tspeed;
        }
    }
    void OnCollisionExit(Collision collision)
    {
        //Debug.Log(collision.transform.name);
        if (collision.transform.tag == "Wall")
        {
            speed = tspeed;
        }
    }
 }