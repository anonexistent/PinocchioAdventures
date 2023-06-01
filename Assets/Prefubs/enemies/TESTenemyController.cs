using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTenemyController : MonoBehaviour
{
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    //  0.07f - jump distance
    private void Update()
    {
        //if(Random.Range(0f,1f)<0.0005)
        //{
        //    animator.SetTrigger("jumpYes");
        //    transform.position = new Vector2(transform.position.x+0.07f, transform.position.y);            
        //    animator.SetTrigger("jumpNo");
        //}
    }

    public void Go()
    {
        //if (Random.Range(0f, 1f) < 0.5)
        //{

        //} 
        //animator.SetTrigger("jumpYes");
        //transform.position = new Vector2(transform.position.x - 0.07f, transform.position.y);
        //animator.SetTrigger("jumpNo");

        animator.SetTrigger("playerNear");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        transform.localScale = new Vector3( collision.transform.position.x - transform.position.x > 0 ? -1 : 1, 1,1);
        if (collision.transform.tag == "Player")
        {
            animator.SetTrigger("playerNear");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            animator.ResetTrigger("playerNear");
        }
    }

    public void Back()
    {
        animator.ResetTrigger("playerNear");
    }
}
