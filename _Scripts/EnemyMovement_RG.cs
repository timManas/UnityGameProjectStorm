using UnityEngine;
using System.Collections;

public class EnemyMovement_RG : MonoBehaviour
{

    public bool useCurves;						// a setting for teaching purposes to show use of curves
    public float animSpeed = 1.5f;				// a public setting for overall animator animation speed
    public float lookSmoother = 3f;				// a smoothing setting for camera motion
    public float lightsmooth = 3f;              //light lerp smoothness
    public float turnSmoothing = 15f;
    public float speed = .25f;
    public float deadZone = 5f;
    public float angleResponseTime = 0.6f;          // Response time for turning an angle into angularSpeed.
    public float timeToStop = 1f;

    public Transform dustStart;
    public Transform mechChest;
    public Transform mechSpine;

    public GameObject mainEngine;
    public GameObject mainEngineInner;
    public GameObject mainEngineSmallBits;
    public GameObject mainEngineLight;
    public GameObject backheadLightL;
    public GameObject backheadLightR;
    public GameObject frontheadLight;
    public GameObject mouthLightL;
    public GameObject mouthLightR;
    public GameObject backEngineL;
    public GameObject backEngineR;
    public GameObject backEngineLightL;
    public GameObject backEngineLightR;
    public GameObject backEngineInnerL;
    public GameObject backEngineInnerR;
    public GameObject backEngineSmokeDownL;
    public GameObject backEngineSmokeOutL;
    public GameObject backEngineSmokeDownR;
    public GameObject backEngineSmokeOutR;


    static Animator enemyAnim;							// a reference to the animator on the character
    private Rigidbody enemyRigidBody;
    private Transform enemyBulletSpawn;            // ALERT ! RED FLAG  

    private Transform player;
    private Vector3 movement;
    private float angularSpeed;
    //private EnemyShooting enemyShoot;

    private Color EngineLow = new Color(0, 0, 0, 0);
    private Color EngineHigh = new Color(0, 0, 0, 0);
    private Color EngineHover = new Color(0, 0, 0, 0);

    private float dustTime = 0.0f;
    private bool startLights = false;				//reference for starting up lights
    private bool inAir = false;

    private Light gunLight;


    private AnimatorStateInfo currentBaseState;			// a reference to the current state of the animator, used for base layer
    private AnimatorStateInfo layer2CurrentState;	// a reference to the current state of the animator, used for layer 2

    static int StartSequenceParameterBool = Animator.StringToHash("StartSequence");
    //static int StartSequenceParameterBool = Animator.StringToHash("StartUp");
    static int walkingParameterBool = Animator.StringToHash("Walking");
    static int shootParameterBool = Animator.StringToHash("Shoot");

    static int startUpState = Animator.StringToHash("Base Layer.StartUp");
    static int idleState = Animator.StringToHash("Base Layer.Idle");
    static int walkState = Animator.StringToHash("Base Layer.Walk");
    static int offState = Animator.StringToHash("Base Layer.Off");

    static int shootState = Animator.StringToHash("Layer 2.Shoot");	

    // Use this for initialization
    void Start()
    {
        enemyAnim = GetComponent<Animator>();
        if (enemyAnim.layerCount == 2)
        {
            enemyAnim.SetLayerWeight(1, 1);
        }
            

        enemyRigidBody = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        //enemyShoot = GetComponent<EnemyShooting>();
        currentBaseState = enemyAnim.GetCurrentAnimatorStateInfo(0);
        if (enemyAnim.layerCount == 2)
        {
            layer2CurrentState = enemyAnim.GetCurrentAnimatorStateInfo(1);	// set our layer2CurrentState variable to the current state of the second Layer (1) of animation
        }
            
        gunLight = GetComponent<Light>();

        Initiliaze();
    }


    void FixedUpdate()
    {

        currentBaseState = enemyAnim.GetCurrentAnimatorStateInfo(0);	// set our currentState variable to the current state of the Base Layer (0) of animation

        if (enemyAnim.layerCount == 2)
            layer2CurrentState = enemyAnim.GetCurrentAnimatorStateInfo(1);	// set our layer2CurrentState variable to the current state of the second Layer (1) of animation

        
        
            
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            enemyAnim.SetBool(StartSequenceParameterBool, true);
            enemyAnim.SetBool(walkingParameterBool, false);
            startLights = true;
            Instantiate(dustStart, transform.position, transform.rotation);
            enemyRigidBody.transform.LookAt(player.position);

            
            
            
        }
    }

    void OnTriggerStay(Collider other)
    {
        
        if (other.tag == "Player")
        {
            Vector3 newPlayerPosition = player.position;
            newPlayerPosition.y = 1;
            enemyRigidBody.transform.LookAt(newPlayerPosition);


            Debug.Log("Start Shooting");
            enemyAnim.SetTrigger("ShootTrigger");
            Debug.Log("Stop Shooting");

           
        }
         
       
    }


    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Walking(other.transform.position.x, other.transform.position.z);
            StartCoroutine(coroutinePlayerExitWalking());

            
            
        }

    }


    ////////////////////////////////////////////////////////////////////////////////////////////////


    void Shoot()
    {
        bool shooting = false;

        
        
        gunLight.enabled = true;
    }

    void Walking(float horizontal, float vertical)
    {
        
        enemyAnim.SetBool(walkingParameterBool, true);
        movement.Set(horizontal, 0f, vertical);
        movement = movement.normalized * speed * Time.deltaTime; // DELTA TIME  is the time between each call ...or each secondsdsssssss
   
    }

    void Initiliaze()
    {
        //initialising engine colours
        EngineLow.r = 0;
        EngineLow.g = 0.015f;
        EngineLow.b = 0.055f;
        EngineLow.a = 0.3f;

        EngineHigh.r = 0f;
        EngineHigh.g = 0.05f;
        EngineHigh.b = 0.7f;
        EngineHigh.a = 0.6f;

        EngineHover.r = 0.5f;
        EngineHover.g = 0.5f;
        EngineHover.b = 0f;
        EngineHover.a = 1f;

        //Particle Systems OFF!

        mainEngine.GetComponent<ParticleSystem>().enableEmission = false;
        mainEngineInner.GetComponent<ParticleSystem>().enableEmission = false;
        mainEngineSmallBits.GetComponent<ParticleSystem>().enableEmission = false;
        mainEngineLight.GetComponent<Light>().intensity = 0;

        backheadLightL.GetComponent<Light>().intensity = 0;
        backheadLightR.GetComponent<Light>().intensity = 0;
        frontheadLight.GetComponent<Light>().intensity = 0;
        mouthLightL.GetComponent<Light>().intensity = 0;
        mouthLightR.GetComponent<Light>().intensity = 0;

        backEngineL.GetComponent<ParticleSystem>().enableEmission = false;
        backEngineR.GetComponent<ParticleSystem>().enableEmission = false;
        backEngineLightL.GetComponent<Light>().intensity = 0;
        backEngineLightR.GetComponent<Light>().intensity = 0;
        backEngineInnerL.GetComponent<ParticleSystem>().enableEmission = false;
        backEngineInnerR.GetComponent<ParticleSystem>().enableEmission = false;
        backEngineSmokeDownL.GetComponent<ParticleSystem>().enableEmission = false;
        backEngineSmokeOutL.GetComponent<ParticleSystem>().enableEmission = false;
        backEngineSmokeDownR.GetComponent<ParticleSystem>().enableEmission = false;
        backEngineSmokeOutR.GetComponent<ParticleSystem>().enableEmission = false;
    }

    IEnumerator coroutinePlayerExitWalking()
    {
        yield return new WaitForSeconds(2.5f);
        enemyAnim.SetBool(walkingParameterBool, false);
    }

}
