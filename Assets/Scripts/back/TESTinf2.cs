using System.Collections;
using UnityEngine;

    /// <summary>
    /// script need be in ever background modeules in "Part".
    /// so may be paralax effect worked with help order in layer
    /// </summary>
public class TESTinf2 : MonoBehaviour
{
    float lenght, startPos, temp, distance;

    int tiktok;

    GameObject camera1;
    public float paralax;

    void Start()
    {
        camera1 = GameObject.FindWithTag("ForeignCamera");
        startPos = transform.position.x;
        lenght = GetComponent<SpriteRenderer>().bounds.size.x;

        StartCoroutine(Cor());
    }

    private void Foo()
    {
        temp = camera1.transform.position.x * (1 - paralax);
        distance = camera1.transform.position.x * paralax;
        transform.position = new Vector3(startPos + distance + (lenght / 2), camera1.transform.position.y, transform.position.z);
        
        if (temp > startPos + lenght)
        {
            startPos += lenght;
        }
        else if (temp < startPos + lenght)
        {
            startPos -= lenght;
        }
        
    }

    private IEnumerator Cor()
    {
        while (true)
        {
            Foo();
            yield return new WaitForSeconds(0.01f);
        }
    }
}
