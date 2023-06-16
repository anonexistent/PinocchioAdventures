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
    [DllImport("__Internal")]
    private static extern void TestMySql(int x);

    void Start()
    {
        //RequestJs();

    }

    void Update()
    {

    }

    public static void RequestJs()
    {
        try
        {
            var a = Random.Range(0f, 128f);
            //PluginTestWeb(a);
            OtherFunc(StarCollector.starCount);
            TestMySql(StarCollector.starCount);
        }
        catch (System.Exception)
        {

        }

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
