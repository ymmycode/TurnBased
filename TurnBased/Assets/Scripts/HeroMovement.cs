using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMovement : MonoBehaviour
{

    public float movementSpeed = 100f;

    Vector3 currentPosition, lastPosition;

    // Start is called before the first frame update
    void Start()
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
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveX, 0, moveZ);
        GetComponent<Rigidbody>().velocity = movement * movementSpeed * Time.deltaTime;
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

        /*
        if (other.tag == "Enter Town")
        {
            CollisionHandler collisionHandler = other.GetComponent<CollisionHandler>();
            GameManager.gameInstance.newHeroPosition =
                collisionHandler.spawnPoint.transform.position;
            GameManager.gameInstance.sceneToLoad = collisionHandler.sceneToLoad;
            GameManager.gameInstance.LoadNextScene();
        }

        if (other.tag == "Leave Town")
        {
            CollisionHandler collisionHandler = other.GetComponent<CollisionHandler>();
            GameManager.gameInstance.newHeroPosition =
                collisionHandler.spawnPoint.transform.position;
            GameManager.gameInstance.sceneToLoad = collisionHandler.sceneToLoad;
            GameManager.gameInstance.LoadNextScene();
        }
        */

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
