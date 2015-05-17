using UnityEngine;
using System.Collections;

public class LeviathanMovement : MonoBehaviour 
{

    public Animator leviathanAnim;
    private Transform player;

	// Use this for initialization
	void Start () 
    {
        leviathanAnim = GetComponentInParent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        if (leviathanAnim.layerCount == 2)
        {
            leviathanAnim.SetLayerWeight(1, 1);
        }

	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //leviathanAnim.SetBool("weakAttack", true);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            //leviathanAnim.SetBool("weakAttack", false); // This doesent ??? LOL 
             // This works
            int counter = 0;
            int randomAttack = Random.Range(1, 5);
          
            Debug.Log(randomAttack);
            switch (randomAttack)
            {
                case 1:
                    leviathanAnim.SetTrigger("weakAttackTrg");
                    counter++;
                    break;

                case 2:
                    leviathanAnim.SetTrigger("medAttackTrg");
                    break;

                case 3:
                    leviathanAnim.SetTrigger("hardAttackTrg");
                    break;

                case 4:
                    leviathanAnim.SetTrigger("killerAttackTrg");
                    break;
            }


        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            
        }

    }

}// End of program
