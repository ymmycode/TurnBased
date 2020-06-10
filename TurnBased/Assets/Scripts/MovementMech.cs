using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementMech : MonoBehaviour
{

    //hero movement
    public Rigidbody rigidBody;
    public bool cubeIsOnTheGround = true;
    public float movementSpeed = 100f;
    public float rotationSpeed = 240f;
    private Vector3 moveDirection = Vector3.zero;



    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        //getting movement input
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveX, 0, moveZ).normalized * movementSpeed * Time.deltaTime;
        transform.Translate(movement, Space.Self);

        if (Input.GetButtonDown("Jump") && cubeIsOnTheGround)
        {
            rigidBody.AddForce(new Vector3(0, 5, 0), ForceMode.Impulse);
            cubeIsOnTheGround = false;
        }

        //GetComponent<Rigidbody>().velocity = movement * movementSpeed * Time.deltaTime;
    }
    
        private void OnCollisionEnter(Collision ground)
    {
        if(ground.gameObject.tag == "Ground")
        {
            cubeIsOnTheGround = true;
        }
    }
}
