
using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{

    public float health = 100f;
    public float resetAfterDeathTime = 5f;              // How much time from the player dying to the level reseting.
    

    private Animator playerAnim;
    private bool playerDead;
    private float timer;

    private PlayerMovement_CC playerMovement;

    static int deadParameterBool = Animator.StringToHash("Dying");


    void Awake()
    {
        playerAnim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement_CC>();


    }

    // Update is called once per frame
    void Update()
    {

        if (health <= 0f)
        {

            if (!playerDead)
            {
                PlayerDying();
            }
            else
            {
                PlayerDead();
                LevelReset();
            }
        }
    }

    void PlayerDying()
    {
        playerDead = true;
        playerAnim.SetBool(deadParameterBool, true);
    }

    void PlayerDead()
    {
        playerAnim.SetBool(deadParameterBool, false);
        playerMovement.enabled = false;   //All functions should stop 
    }

    void LevelReset()
    {
        /*
        // Increment the timer.
        timer += Time.deltaTime;

        //If the timer is greater than or equal to the time before the level resets...
        if (timer >= resetAfterDeathTime)
            // ... reset the level.
            sceneFadeInOut.EndScene();
         */
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
    }

    // ============================================================================================



} // End of File
