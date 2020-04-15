using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMovement : MonoBehaviour
{
    //private CharacterController characterController;

    public float movementSpeed = 100f;
    public float rotationSpeed = 240f;

    private Vector3 moveDirection = Vector3.zero;

    Vector3 currentPosition, lastPosition;

    // Start is called before the first frame update
    void Start()
    {
        //characterController = GetComponent<CharacterController>();
        ProcessHeroLocation();
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

    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
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

    private void Movement()
    {
        //getting movement input
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        

        Vector3 movement = new Vector3(moveX, 0, moveZ).normalized * movementSpeed * Time.deltaTime;
        transform.Translate(movement, Space.Self);

        //GetComponent<Rigidbody>().velocity = movement * movementSpeed * Time.deltaTime;
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
            GameManager.gameInstance.canGetEncounter = true;
        }
    }
}
