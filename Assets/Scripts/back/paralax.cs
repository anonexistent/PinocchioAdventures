using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paralax : MonoBehaviour
{
    Material mat;
    float dist;

    [Range(0f, 0.5f)]
    public float speed = 0.2f;

    private void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        dist += Time.deltaTime * speed;
        mat.SetTextureOffset("_MainTex", Vector2.right * dist);
    }


}
