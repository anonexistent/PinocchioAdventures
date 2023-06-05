using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region v1.0
//public class TESTinf3 : MonoBehaviour
//{
//    Transform camera1;
//    Vector3 camStart;
//    float distance;
//    GameObject[] backs;
//    Material[] mats;
//    float[] backSpeed;
//    float farthestBack;

//    [Range(0.01f, 0.05f)]
//    public float parlxSpeed;

//    // Start is called before the first frame update
//    void Start()
//    {
//        camera1 = Camera.main.transform;
//        camStart = camera1.position;

//        int backCount = transform.childCount;
//        mats = new Material[backCount];
//        backSpeed = new float[backCount];
//        backs = new GameObject[backCount];

//        for(int i = 0; i < backCount; i++)
//        {
//            backs[i] = transform.GetChild(i).gameObject;
//            mats[i] = backs[i].GetComponent<Renderer>().material;
//        }
//        HowBackSpeed(backCount);
//    }

//    void HowBackSpeed(int count)
//    {
//        for(int i = 0;i < count;i++) 
//        {
//            if ((backs[i].transform.position.z - camera1.position.z) < farthestBack)
//            {
//                farthestBack = backs[i].transform.position.z - camera1.transform.position.z;

//            }
//        }

//        for (int i = 0; i < count; i++)
//        {
//            backSpeed[i] = 1 - (backs[i].transform.position.z - camera1.position.z) / farthestBack;
//        }
//    }

//    private void LateUpdate()
//    {
//        distance = camera1.position.x - camStart.x;
//        transform.position = new Vector3(camera1.position.x, camera1.position.y, 0);

//        for (int i = 0; i < backs.Length; i++)
//        {
//            float speed = backSpeed[i] * parlxSpeed;
//            mats[i].SetTextureOffset("_MainTex", new Vector2(distance, 0) * speed);
//        }
//    }
//}
#endregion

#region v2.0
public class TESTinf3 : MonoBehaviour
{
    Transform cam; // Main Camera
    Vector3 camStartPos;
    Vector2 distance; // camera1 start position, current position

    GameObject[] backgrounds;
    Material[] mat;
    float[] backSpeed;

    float farthestBack;

    [Range(0f, 0.1f)]
    public float parallaxSpeed;

    void Start()
    {
        //  workablesses objects's inicialization
        cam = Camera.main.transform;
        camStartPos = cam.position;

        int backCount = transform.childCount;
        mat = new Material[backCount];
        backSpeed = new float[backCount];
        backgrounds = new GameObject[backCount];

        for (int i = 0; i < backCount; i++)
        {
            backgrounds[i] = transform.GetChild(i).gameObject;
            mat[i] = backgrounds[i].GetComponent<Renderer>().material;

        }
        BackSpeedCalculate(backCount);
    }

    void BackSpeedCalculate(int backCount)
    {
        for (int i = 0; i < backCount; i++) // find the farhthest background
        {
            if ((backgrounds[i].transform.position.z - cam.position.z) > farthestBack)
            {
                farthestBack = backgrounds[i].transform.position.z - cam.position.z;
            }

        }

        for (int i = 0; i < backCount; i++) // set the speed of backgrounds
        {
            backSpeed[i] = 1 - (backgrounds[i].transform.position.z - cam.position.z) / farthestBack;
        }
    }



    private void LateUpdate()
    {
        distance = cam.position - camStartPos
            ;
        
        
        //      ÐÀÁÎÒÀ ÍÀÄ ÒÅÌ ×ÒÎÁÛ ::::::: ÊÀÐÒÈÍÊÀ ÑÌÅÙÀÅÒÑß Â ÊÀÌÅÐÅ!!!!!!!!
        
        transform.position = new Vector3(cam.position.x
            -0.85f
            , cam.transform.position.y
            +0.26f
            , 0);


        for (int i = 0; i < backgrounds.Length; i++)
        {
            float speedX = backSpeed[i] * parallaxSpeed;
            float speedY = speedX / 2;  // if you close Y movement , set to 0
            mat[i].SetTextureOffset("_MainTex", new Vector2(distance.x * speedX, distance.y * speedY));
        }
    }
}
#endregion