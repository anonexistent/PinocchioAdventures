using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WhoDie : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(waitAnim(collision));
        
    }

    IEnumerator waitAnim(Collision2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            //transform.GetComponent<CircleCollider2D>().enabled = false;
            transform.LeanScale(Vector2.zero,0.1f);
            collision.gameObject.GetComponent<Animator>().SetBool("NoLife", true);
            //transform.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.25f);
            Destroy(collision.gameObject);
            Destroy(gameObject);
            //Destroy(gameObject);
        }
    }
}
