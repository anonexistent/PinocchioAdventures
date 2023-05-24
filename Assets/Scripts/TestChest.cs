using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class TestChest : MonoBehaviour
{
    private void OnGUI()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            GUI.Window(0, new Rect(Screen.width/2-150, Screen.height/2-75, 300, 150),
                ChestPopup, "Open?");
        }
    

        void ChestPopup(int windowID)
        {
            if (GUI.Button(new Rect(100, 100, 100, 30), "Open"))
            {
                Debug.Log("Opening chest!"); 
                Destroy(GameObject.Find("ChestPopup")); 
            }
            if (GUI.Button(new Rect(100, 140, 100, 30), "Cancel"))
            {
                Debug.Log("Cancelled!"); 
                Destroy(GameObject.Find("ChestPopup"));             }
        }
    }
}
