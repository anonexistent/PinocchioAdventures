using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class menu1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = Vector3.zero;
    }
    public void Open()
    {
        transform.LeanScale(Vector2.one, 0.3f);
    }

    public void Close()
    {
        transform.LeanScale(Vector2.zero, 0.5f).setEaseInBack();
    }
}
