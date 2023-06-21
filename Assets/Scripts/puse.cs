using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class puse : MonoBehaviour
{
    public static bool isQuestActive = false;
    public float timer; 
    public bool ispuse; 
    public bool guipuse; 

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isQuestActive)
        {
            isQuestActive = false;
        }
        if (!isQuestActive)
        {
            Time.timeScale = timer; 
        
            if (Input.GetKeyDown(KeyCode.Escape) && ispuse == false) 
            { 
                ispuse = true; 
            } 
            else if (Input.GetKeyDown(KeyCode.Escape) && ispuse == true) 
            { 
                ispuse = false; 
            }
            if (ispuse == true) 
            { 
                timer = 0; 
                guipuse = true; 
            }
            else if (ispuse == false)
            {
                timer = 1f; guipuse = false;
            }
        }

    }
    public void OnGUI()
    {
        if (guipuse == true)
        {
            Cursor.visible = true;

            //if (GUI.Button(new Rect((float)(Screen.width / 2), (float)(Screen.height / 2) - 150f, 150f, 45f), "continue")) 
            //{ 
            //    ispuse = false; 
            //    timer = 0; 
            //    Cursor.visible = false; 
            //} 
            if (GUI.Button(new Rect((float)(Screen.width / 2), (float)(Screen.height / 2) - 100f, 150f, 45f), "restart")) 
            {
                ispuse = false;
                timer = 0;
                //Application.LoadLevel("level3TEST");
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            } 
            if (GUI.Button(new Rect((float)(Screen.width / 2), (float)(Screen.height / 2) - 50f, 150f, 45f), "save as")) 
            {

            } 
            if (GUI.Button(new Rect((float)(Screen.width / 2), (float)(Screen.height / 2), 150f, 45f), "main menu")) 
            {
                StarCollector.starCount = 0;
                ispuse = false; 
                timer = 0; 
                Application.LoadLevel("Menu");
            } 
        } 
    } 
}
        
