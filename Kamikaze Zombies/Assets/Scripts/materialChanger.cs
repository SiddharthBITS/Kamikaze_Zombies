using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class materialChanger : MonoBehaviour
{
    public GameObject[] gunElements;
    public Material blue;
    public GameObject target;
    public float dist;

    private void OnCollisionEnter(Collision collision)
    {
        foreach (GameObject element in gunElements)
        {
            if (collision.collider.CompareTag("Player"))
            {
                element.GetComponent<MeshRenderer>().material = blue;
            }
        }
    }
}
