using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMovement : MonoBehaviour
{
    //hero gameobject
    public GameObject heroItSelf;
    public HUD hud;
    public GameObject cameraMovementStop;
    public GameObject flowchart;

    public GameObject computer;

    public bool isNearComputer = false;


    //private CharacterController characterController;

    //hero current and last pos
    Vector3 currentPosition, lastPosition; 

    // Start is called before the first frame update
    void Start()
    {
        heroItSelf.GetComponent<MovementMech>().enabled = false;
        Invoke("EnableMovement",2f);
        //characterController = GetComponent<CharacterController>();
        ProcessHeroLocation();
    }

    void EnableMovement()
    {
        heroItSelf.GetComponent<MovementMech>().enabled = true;
    }

    private void ProcessHeroLocation()
    {
        if (GameManager.gameInstance.nextSpawnPoint != "")
        {
            GameObject spawnPoint = GameObject.Find(GameManager.gameInstance.nextSpawnPoint);
            transform.position = spawnPoint.transform.position;
            GameManager.gameInstance.nextSpawnPoint = "";

        }
        else if (GameManager.gameInstance.previousHeroPosition != Vector3.zero)
        {
            transform.position = GameManager.gameInstance.previousHeroPosition;
            GameManager.gameInstance.previousHeroPosition = Vector3.zero;
        }
    }

    private void Update()
    {
        if(isNearComputer == true)
        {
            LaunchFlowchartPuzzle();
        }
    }

    private void LaunchFlowchartPuzzle()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            heroItSelf.GetComponent<MovementMech>().enabled = false;
            cameraMovementStop.GetComponent<PlayerFollow>().enabled = false;

            UnityEngine.Cursor.visible = true;
            UnityEngine.Cursor.lockState = CursorLockMode.None;

            hud.CloseMessagePanel("");
            flowchart.GetComponent<DragnDrop>().LaunchPanel();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MovementMeasurment();
    }

    private void MovementMeasurment()
    {
        currentPosition = transform.position;
        if (currentPosition == lastPosition)
        {
            //is not walking
            GameManager.gameInstance.isWalking = false;
        }
        else
        {
            //is walking
            GameManager.gameInstance.isWalking = true;
        }
        lastPosition = currentPosition;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Transition")
        {
            CollisionHandler col = other.gameObject.GetComponent<CollisionHandler>();
            GameManager.gameInstance.nextSpawnPoint = col.spawnPointName;
            GameManager.gameInstance.sceneToLoad = col.sceneToLoad;
            GameManager.gameInstance.LoadNextScene();
        }

        if (other.tag == "Encounter Zone")
        {
            RegionData region = other.gameObject.GetComponent<RegionData>();
            GameManager.gameInstance.currentRegion = region;
        }

        if (other.tag == "Computer")
        {
            hud.OpenMessagePanel("");
            isNearComputer = true;        
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Encounter Zone")
        {
            GameManager.gameInstance.canGetEncounter = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Encounter Zone")
        {
            GameManager.gameInstance.canGetEncounter = false;
        }

        if (other.tag == "Computer")
        {
            hud.CloseMessagePanel("");
            isNearComputer = false;
        }
    }
}
