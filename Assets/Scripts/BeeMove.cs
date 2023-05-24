using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BeeMove : MonoBehaviour
{
    [SerializeField]
    Transform point1, point2;
    Vector3 nextPoint;
    public Transform startPoint;
    public float speed = 1f;


    private void Start()
    {
        nextPoint = startPoint.position;
    }
    private void Update()
    {
        //transform.rotation =  Vector3.RotateTowards(transform.position, transform.position, 0.1f, 1.0f);
        transform.position = Vector3.MoveTowards(transform.position, nextPoint, speed * Time.deltaTime);

        if(transform.position == point1.position)
        {
            nextPoint = point2.position;
            transform.localScale = new Vector3(-1, 1,0);
        }

        else if (transform.position == point2.position)
        {
            nextPoint = point1.position;
            transform.localScale = new Vector3(1, 1,0);
        }
    }
    
}
