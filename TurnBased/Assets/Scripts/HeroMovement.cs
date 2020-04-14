using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMovement : MonoBehaviour
{

    public float movementSpeed = 100f;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = GameManager.gameInstance.newHeroPosition;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveX , 0, moveZ);
        GetComponent<Rigidbody>().velocity = movement * movementSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
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
    }
}
