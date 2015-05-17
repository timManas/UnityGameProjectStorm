using UnityEngine;
using System.Collections;

public class PlayerMovement_CC : MonoBehaviour
{

    public float turnSmoothing = 15f;   // A smoothing value for turning the player.
    public float speed = 1f;
    public float gravity = 20.0F;

    private Vector3 walkMovement;
    private Vector3 runMovement;

    private Vector3 moveDirection;
    private Vector3 forward;
    private Vector3 right;
    private Vector3 targetDirection;

    private Animator anim;              // Reference to the animator component.
    private CharacterController playerCharContr;
    private Transform playerTransform;


    static int walkParameterBool = Animator.StringToHash("Walking");
    static int runParameterBool = Animator.StringToHash("Running");
    static int shootParameterBool = Animator.StringToHash("Shooting");
    // Use this for initialization

    void Start()
    {
        anim = GetComponent<Animator>();
        playerCharContr = GetComponent<CharacterController>();
        playerTransform = GetComponent<Transform>();
        anim.SetLayerWeight(1, 1f);
    }




    // Update is called once per frame
    void FixedUpdate()
    {

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        MoveManagement(horizontal, vertical);
    }

    // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

    void MoveManagement(float horizontal, float vertical)
    {

        if (horizontal != 0f || vertical != 0f)
        {
            Walking(horizontal, vertical);
            if (Input.GetButton("Run"))
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
        speed = 1f;
        
        forward = playerTransform.forward;
        right = new Vector3(forward.z, 0f, -forward.x);

        targetDirection = horizontal * right + vertical * forward;
        moveDirection = Vector3.RotateTowards(moveDirection, targetDirection, 200 * Mathf.Deg2Rad * Time.deltaTime, 1000);
        walkMovement = moveDirection * Time.deltaTime * speed;
        
        anim.SetBool(walkParameterBool, true);
        playerCharContr.Move(walkMovement);
        Rotating(targetDirection, transform);
    }

    void Running(float horizontal, float vertical)
    {

        speed = 2f;
        
        forward = playerTransform.forward;
        right = new Vector3(forward.z, 0f, -forward.x);

        targetDirection = horizontal * right + vertical * forward;
        moveDirection = Vector3.RotateTowards(moveDirection, targetDirection, 200 * Mathf.Deg2Rad * Time.deltaTime, 1000);
        runMovement = moveDirection * Time.deltaTime * speed;

        anim.SetBool(runParameterBool, true);
        playerCharContr.Move(runMovement);
        Rotating(targetDirection, transform);
    }

    void Rotating(Vector3 direction, Transform transform)
    {
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveDirection);
        }
    }





} // End of Line
