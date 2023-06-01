using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeMove2 : MonoBehaviour
{
    [SerializeField]
    float max;

    void Update()
    {
        float x = Mathf.PingPong(Time.time, max);
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
        Debug.Log(Math.Round(x, 1) +" — "+ Math.Round(max, 1));
        if ((Math.Abs( Math.Round(x, 2) - Math.Round(max, 2)) == 1) || (Math.Round(x, 2) - Math.Round(max, 2)) == 0) transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
    }
}
