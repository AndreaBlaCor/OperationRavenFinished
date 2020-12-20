using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainLogic : MonoBehaviour
{
    public GameObject player;

    //Roofs variables
    private GameObject[] roofs;
    private string actualRoof;
    private string oldRoof;

    public Material closedRoof;
    public Material openRoof;

    //energy variables
    public int energy;
    public UnityEngine.UI.Slider energyBar;

    //time variables
    public float startTime;
    public float stepTime;

    //player alive var
    public bool ifDead = false;

    //player winner
    public bool ifWinner = false;
    public int numGems = 0; 

    //array for Flasks
    private bool[] vitamineFlask;
    private bool[] wrongFlask;

    //prefabs for Flasks
    public GameObject vitaminePrefab;
    public GameObject wrongPrefab;

    //number of flaks to generate
    public int numVitamine;
    public int numWrong;

    //array for Objectives
    private bool[] objective1;
    private bool[] objective2;
    private bool[] objective3;
    private bool[] objective4;

    //prefabs for Objectives
    public GameObject obj1Prefab;
    public GameObject obj2Prefab;
    public GameObject obj3Prefab;
    public GameObject obj4Prefab;

    //number of objectives to generate
    public int numObj1;
    public int numObj2;
    public int numObj3;
    public int numObj4;

    //generate sprites for images
    public Sprite gem1;
    public Sprite gem2;
    public Sprite gem3;
    public Sprite gem4;
    
    //generare images on UI
    public UnityEngine.UI.Image cGem1;
    public UnityEngine.UI.Image cGem2;
    public UnityEngine.UI.Image cGem3;
    public UnityEngine.UI.Image cGem4;

    // Game Texts
    public UnityEngine.UI.Text finalText;

    
    
    // Start is called before the first frame update
    void Start()
    {
                
        /*set comparison tags to nothing/blank*/
        actualRoof = "";
        oldRoof = "NONE";
        energy = 100;

        /*initialice array to objects with "roof" tag*/
        roofs = GameObject.FindGameObjectsWithTag("Roof");
        startTime = Time.realtimeSinceStartup;
                
        /*create array to hold flasks*/
        vitamineFlask = new bool[25];
        wrongFlask = new bool[25];

        /*process to assign randomly a number of flask into the arrays*/
        int tmpVitamine = 0;
        int tmpWrong = 0;

        while ((tmpVitamine < numVitamine) && (tmpWrong < numWrong))
        {
            int tmp1 = Random.Range(0, 24);
            int tmp2 = Random.Range(0, 24);

            if (vitamineFlask[tmp1] == false)
            {
                vitamineFlask[tmp1] = true;
                tmpVitamine++;
            }

            if (wrongFlask[tmp2] == false)
            {
                wrongFlask[tmp2] = true;
                tmpWrong++;
            }
        }

        /*assign the array of flasks to the roofs*/
        for (int i = 0; i < 24; i++)
        {
            if (vitamineFlask[i] == true)
            {
                float px = Random.Range(-4, 4) + roofs[i].transform.position.x;
                float pz = Random.Range(-4, 4) + roofs[i].transform.position.z;
                Vector3 npos = new Vector3(px, 0, pz);
                Instantiate(vitaminePrefab, npos, Quaternion.identity);          
            }

            if (wrongFlask[i] == true)
            {
                float px = Random.Range(-4, 4) + roofs[i].transform.position.x;
                float pz = Random.Range(-4, 4) + roofs[i].transform.position.z;
                Vector3 npos = new Vector3(px, 0, pz);
                Instantiate(wrongPrefab, npos, Quaternion.identity);
            }
        }

        /*create array to hold objectives*/
        objective1 = new bool[25];
        objective2 = new bool[25];
        objective3 = new bool[25];
        objective4 = new bool[25];

        /*process to assign randomly a number of flask into the arrays*/
        int tmpObj1 = 0;
        int tmpObj2 = 0;
        int tmpObj3 = 0;
        int tmpObj4 = 0;

        while ((tmpObj1 < numObj1 )  && (tmpObj2 < numObj2) && (tmpObj3 < numObj3) && (tmpObj4 < numObj4))
        {
            int tmp1 = Random.Range(0, 24);
            int tmp2 = Random.Range(0, 24);
            int tmp3 = Random.Range(0, 24);
            int tmp4 = Random.Range(0, 24);

            if (objective1[tmp1] == false)
            {
                objective1[tmp1] = true;
                tmpObj1++;
            }

            if (objective2[tmp2] == false)
            {
                objective2[tmp2] = true;
                tmpObj2++;
            }

            if (objective3[tmp3] == false)
            {
                objective3[tmp3] = true;
                tmpObj3++;
            }

            if (objective4[tmp4] == false)
            {
                objective4[tmp4] = true;
                tmpObj4++;
            }
        }

        /*Assign the array of objectives to the roofs*/
        for (int j = 0; j < 24; j++)
        {
            if (objective1[j] == true)
            {
                float px = Random.Range(-4, 4) + roofs[j].transform.position.x;
                float pz = Random.Range(-4, 4) + roofs[j].transform.position.z;
                Vector3 npos = new Vector3(px, 0, pz);
                Instantiate(obj1Prefab, npos, Quaternion.identity);
            }

            if (objective2[j] == true)
            {
                float px = Random.Range(-4, 4) + roofs[j].transform.position.x;
                float pz = Random.Range(-4, 4) + roofs[j].transform.position.z;
                Vector3 npos = new Vector3(px, 0, pz);
                Instantiate(obj2Prefab, npos, Quaternion.identity);
            }

            if (objective3[j] == true)
            {
                float px = Random.Range(-4, 4) + roofs[j].transform.position.x;
                float pz = Random.Range(-4, 4) + roofs[j].transform.position.z;
                Vector3 npos = new Vector3(px, 0, pz);
                Instantiate(obj3Prefab, npos, Quaternion.identity);
            }

            if (objective4[j] == true)
            {
                float px = Random.Range(-4, 4) + roofs[j].transform.position.x;
                float pz = Random.Range(-4, 4) + roofs[j].transform.position.z;
                Vector3 npos = new Vector3(px, 0, pz);
                Instantiate(obj4Prefab, npos, Quaternion.identity);
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        /*get actual roof*/
        actualRoof = getClosestRoof();

        if (actualRoof != oldRoof)
        {
            roomChange(actualRoof);
            oldRoof = actualRoof;
        }

        /*reduce engergy as time passes*/
        stepTime = Time.realtimeSinceStartup - startTime;
        if (stepTime > 1)
        {
            energy--;
            refreshEnergy();
            startTime = Time.realtimeSinceStartup;
        }
        /*Die when out of energy*/
        if (energy <= 3)
            {
                ifDead = true;
                gameEnd(false);
            }

        if (ifDead || ifWinner)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        
        }

    }

    public void gameEnd(bool winner)
    { 
        if (winner)
        {
            finalText.text = "YOU WON!";            
            finalText.color = Color.green;
            finalText.gameObject.SetActive(true);
        }
        else
        {
            finalText.text = "YOU HAVE BEEN DEFEATED!";
            finalText.color = Color.red;
            finalText.gameObject.SetActive(true);
        }     
    }

    public void addGem()
    {
        numGems++;
        if (numGems == 4)
        {
            ifWinner = true;
            gameEnd(true);
        }
    
    }

    public void setSign(int value)
    {
        switch (value)
        {
            case 1:
                cGem1.sprite = gem1;
                break;
            case 2:
                cGem2.sprite = gem2;
                break;
            case 3:
                cGem3.sprite = gem3;
                break;
            case 4:
                cGem4.sprite = gem4;
                break;
        }
    }

    //refill energy
    public void refillEnergy(int value)
    {
        energy = energy + value;

        if (energy > 100)
        {
            energy = 100;
        }
        refreshEnergy();    
    }

    //make energy bar display the same as energy
    public void refreshEnergy(){
        energyBar.value = energy;
    }




    //find closest roof
    public string getClosestRoof() {
        string roofMin = "";
        float minDistance = Mathf.Infinity;

        //for each object in roofs
        foreach (GameObject gameObj in roofs) {
            Transform roof = gameObj.transform;

            float dist = Vector3.Distance(roof.position, player.transform.position);

            if (dist < minDistance) {
                roofMin = roof.gameObject.name;
                minDistance = dist;
            }
        }
        return roofMin;
    }

    //room change
    public void roomChange(string actual) {

        foreach (GameObject gameObj in roofs) {
            gameObj.GetComponent<MeshRenderer>().material = closedRoof;
        }
        GameObject.Find(actual).GetComponent<MeshRenderer>().material = openRoof;
    }
}
