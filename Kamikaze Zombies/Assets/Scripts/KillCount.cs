using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//Attached to enemies parent object
public class KillCount : MonoBehaviour
{
    public int killNumber = 0;
    public Text killCounter;

    private void Update()
    {
        //Kills UI text
        killCounter.text = "KILLS: " + killNumber.ToString();

        //Win Condition
        if(killNumber == 10)
        {
            SceneManager.LoadScene(2);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
