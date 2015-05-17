using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour
{

    public int damagePerShot = 5;
    public float timeBetweenBullets = 0.15f;
    public float range = 100f;
    public float effectsDisplayTime = 0.2f;

    private float timer;
    private Ray shootRay;
    private RaycastHit shootHit;
    private int shootableMask;
    private Animator playerAnim;
    private Light gunLight;
    private LineRenderer gunLineRenderer;

    static int shootParameterBool = Animator.StringToHash("Shooting");

    // Use this for initialization
    void Awake()
    {

        playerAnim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        gunLight = GetComponent<Light>();
        gunLineRenderer = GetComponent<LineRenderer>();

        shootableMask = LayerMask.GetMask("Shootable");
    }

    
    void Update()
    {

        timer += Time.deltaTime;

        if (Input.GetButton("Fire1"))
        {
            Shoot();
        }
        else
        {
            playerAnim.SetBool(shootParameterBool, false);
            gunLight.enabled = false;
        }

    }
     

    // ===========================================================================================

    void Shoot()
    {
        timer = 0f;
        playerAnim.SetBool(shootParameterBool, true);
        gunLineRenderer.enabled = true;

        gunLineRenderer.SetPosition(0, transform.position);
        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
        {
            EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();

            if (shootHit.collider.tag == "Enemy" && enemyHealth.health != 0f )
            {
                
                gunLight.enabled = true;
                gunLineRenderer.SetPosition(1, shootHit.point);
                enemyHealth.EnemyTakeDamage(damagePerShot);
                
            }
            

        }
        else
        {
            gunLineRenderer.SetPosition(1, shootRay.origin + shootRay.direction * range);
        }

        
    }


} // End of Line
