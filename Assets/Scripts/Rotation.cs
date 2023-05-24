using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public int door = 0;
    void Start()
    {

    }

    void WelcomeMessage()
    {
        transform.Rotate(0.5f, 0.1f, 0.09f);

        //непрерывное вращение
        //transform.rotation = Quaternion.Euler(0, 45, 0);  //резкий поворот.
        //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 45, 0), Time.deltaTime); //плавный поворот

        //if (door == 0)
        //{ transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0), Time.deltaTime); }
        //if (door == 1)
        //{ transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 90, 0), Time.deltaTime); }
    }

    void Update()
    {
        WelcomeMessage();


    }
}
