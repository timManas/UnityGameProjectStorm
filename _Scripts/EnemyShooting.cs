using UnityEngine;
using System.Collections;

public class EnemyShooting : MonoBehaviour
{

    public int damagePerShot = 5;

    private Light gunLight;

    private Animator enemyAnim;							// a reference to the animator on the character

    static int shootParameterBool = Animator.StringToHash("Shoot");
    private bool shooting = false;


    // Use this for initialization
    void Awake()
    {
        enemyAnim = GetComponent<Animator>();
    

        gunLight = GetComponent<Light>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Shoot()
    {


        enemyAnim.SetBool(shootParameterBool, true);
        shooting = true;
        Debug.Log("Shoot Start");
        /*
        if (layer2CurrentState.fullPathHash == shootState)
        {
            anim.SetBool("Shooting", false);
        }
        */

        if (shooting == true)
        {
            enemyAnim.SetBool(shootParameterBool, false);
            Debug.Log("Shoot Done");
        }
        

        gunLight.enabled = true;
    }

}
