using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{


    public float health = 200f;
    public float sinkSpeed = 2.5f;  

    private Animator enemyAnim;
    private bool enemyDead;
    private bool enemySinking = false;
    private float timer;

    private EnemyMovement_RG enemyMovement;
    private Rigidbody enemyRigidBody;

    static int deadParameterBool = Animator.StringToHash("Dying");



    // Use this for initialization
    void Start()
    {
        enemyAnim = GetComponent<Animator>();
        enemyMovement = GetComponent<EnemyMovement_RG>();
        enemyRigidBody = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0f)
        {

            if (!enemyDead)
            {
                EnemyDying();
            }
            else
            {
                EnemyDead();
                
                
            }
        }

        if (enemySinking == true)
        {
            StartCoroutine(StartSinking());
            
        }

    }



    ////////////////////////////////////////////////////////////////////////

    void EnemyDying()
    {
        enemyDead = true;
        enemyAnim.SetBool(deadParameterBool, true);
    }


    void EnemyDead()
    {
        enemyAnim.SetBool(deadParameterBool, false);
        enemyMovement.enabled = false;
        enemySinking = true;
    }

    IEnumerator StartSinking()
    {
        yield return new WaitForSeconds(2.5f);
        transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        Destroy(gameObject, 2f);
    }

    public void EnemyTakeDamage(float amount)
    {
        health -= amount;
    }



} // END OF lINE

