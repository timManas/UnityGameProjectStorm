using UnityEngine;
using System.Collections;

public class ArachnyaMovement : MonoBehaviour
{

    public Animator arachnyaAnim;
    private Transform player;

    private int counterAttack;
    private int counter = 0;

    // Use this for initialization
    void Start()
    {
        arachnyaAnim = GetComponentInParent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        if (arachnyaAnim.layerCount == 2)
        {
            arachnyaAnim.SetLayerWeight(1, 1);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            arachnyaAnim.SetTrigger("weakAttack");
            counter++;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        { 
            arachnyaAnim.SetTrigger("medAttack");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            arachnyaAnim.SetTrigger("hardAttack");
        }

    }


}
