using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float turnSmoothing = 15f;   // A smoothing value for turning the player.
    public float speed;

    private Animator anim;              // Reference to the animator component.
    private Vector3 movement;
    private Rigidbody playerRigidBody;
   

    static int walkParameterBool = Animator.StringToHash("Walking");
    static int runParameterBool = Animator.StringToHash("Running");

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        playerRigidBody = GetComponent<Rigidbody>();
        //anim.SetLayerWeight(1, 1f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        speed = 1f;
        Move(horizontal, vertical);

    }

    void Move(float horizontal, float vertical)
    {

        if (horizontal != 0f || vertical != 0f)
        {
            Rotating(horizontal, vertical);
            Walking(horizontal, vertical);

            if (Input.GetAxis("Run") != 0)
            {
                Running(horizontal, vertical);
            }
            else
            {
                anim.SetBool(runParameterBool, false);
            }


        }
        else
        {
            anim.SetBool(walkParameterBool, false);
            anim.SetBool(runParameterBool, false);
        }



    }

    void Walking(float horizontal, float vertical)
    {
        //anim.SetFloat(hash.speedFloat, 5.5f, speedDampTime, Time.deltaTime);
        anim.SetBool(walkParameterBool, true);
        movement.Set(horizontal, 0f, vertical);
        movement = movement.normalized * speed * Time.deltaTime; // DELTA TIME  is the time between each call ...or each secondsdsssssss
        playerRigidBody.MovePosition(transform.position + movement); // >>>>>>>>>>>>>>>>>>>> Heads up ...this is another way of changing the characters position
    }

    void Running(float horizontal, float vertical)
    {

        speed = 2f;
        anim.SetBool(runParameterBool, true);
        movement.Set(horizontal, 0f, vertical);
        movement = movement.normalized * speed * Time.deltaTime; // DELTA TIME  is the time between each call ...or each secondsdsssssss
        playerRigidBody.MovePosition(transform.position + movement); // >>>>>>>>>>>>>>>>>>>> Heads up ...this is another way of changing the characters position
    }


    void Rotating(float horizontal, float vertical)
    {
        // Create a new vector of the horizontal and vertical inputs.
        Vector3 targetDirection = new Vector3(horizontal, 0f, vertical);

        // Create a rotation based on this new vector assuming that up is the global y axis.
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);

        // Create a rotation that is an increment closer to the target rotation from the player's rotation.
        Quaternion newRotation = Quaternion.Lerp(playerRigidBody.rotation, targetRotation, turnSmoothing * Time.deltaTime);

        // Change the players rotation to this new rotation.
        playerRigidBody.MoveRotation(newRotation);
    }




}
