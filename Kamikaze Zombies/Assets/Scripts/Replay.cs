using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Attached to end screens
public class Replay : MonoBehaviour
{
    public void RePlay()
    {
        SceneManager.LoadScene(1);
    }
}
