using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    System.Random uuu = new();

    public float xInput;
    public float xForce = 2.0f;
    public float yForce = 4.5f;

    public LayerMask groundLayer;
    public Transform feets;
    public bool isGround;

    bool rightOrientation = true;

    Rigidbody2D rb;
    Animator animator;

    public GameObject rifle;

    SpawnObjs sObjs;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sObjs = GameObject.Find("ObjectSpawner").GetComponent<SpawnObjs>();
    }


    void Update()
    {
        xInput = Input.GetAxis("Horizontal"); // key down a d -> <-
        rb.velocity = new Vector2(xInput * xForce, rb.velocity.y);

        isGround = Physics2D.OverlapCircle(feets.position, 0.1f, groundLayer);

        // jumping
        if (isGround && Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("Jump");
            //rb.velocity = Vector2.up * yForce;
            rb.velocity = new Vector2(rb.velocity.x, yForce);
        }
        else if(Input.GetKeyDown(KeyCode.R))
        {
            Application.LoadLevel(0);
        }
        else if(Input.GetKeyDown(KeyCode.F))
        {
            PlayerWinBestEnemies();
        }
        
        // rotation leftr-right
        if (rightOrientation && xInput <0) Flip();
        else if (!rightOrientation && xInput > 0) Flip();

        // run anim
        if (xInput != 0) animator.SetBool("Runnig", true);
        else animator.SetBool("Runnig", false); 

        // idle anim
        if (isGround) animator.SetBool("Grounded", true); //2
        else animator.SetBool("Grounded", false);//3

    }

    private void PlayerWinBestEnemies()
    {
        var a = Instantiate(rifle);
        a.transform.position = transform.position;
        a.GetComponent<Rigidbody2D>().AddForce(new Vector2(rightOrientation?100f:-100f,10f));
        sObjs.curObjs.Add(a);
    }

    void Flip()
    {
        rightOrientation = !rightOrientation;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.tag == "star")
    //    {
    //        StarCollector.starCount++;
    //        Destroy(collision.gameObject);
    //    }
    //}
}
