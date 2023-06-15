using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Web;
using UnityEngine;

public class JavaScript : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void PluginTestWeb(float a);
    [DllImport("__Internal")]
    private static extern void OtherFunc(int a);

    // Start is called before the first frame update
    void Start()
    {
        //RequestJs();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void RequestJs()
    {
        var a = Random.Range(0f, 128f);
        //PluginTestWeb(a);
        OtherFunc(StarCollector.starCount);
    }

    public void ResponseOk()
    {
        Debug.Log("ok");
    }
    public void ResponseError()
    {
        Debug.Log("error");
    }

}
