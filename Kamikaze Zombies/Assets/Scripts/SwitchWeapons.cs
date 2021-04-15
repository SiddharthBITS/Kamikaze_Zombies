using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Attached to guns parent object
public class SwitchWeapons : MonoBehaviour
{
    public int weapon = 0;
    public GameObject imageR;
    public GameObject imageH;
    public GameObject textR;
    public GameObject textH;

    //Load weapon
    void Start()
    {
        SelectWeapon();
    }

    void Update()
    {
        int previousWeapon = weapon;

        //Switch on scroll
        if(Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (weapon >= transform.childCount - 1)
            {
                weapon = 0;
            }
            else
            {
                weapon++;
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (weapon <= 0)
            {
                weapon = transform.childCount - 1;
            }
            else
            {
                weapon--;
            }
        }
        if(previousWeapon != weapon)
        {
            SelectWeapon();
        }
    }

    //Enable/Disable gun gameobjects
    void SelectWeapon()
    {
        int i = 0;
        foreach(Transform currentWeapon in transform)
        {
            if(i == weapon)
            {
                currentWeapon.gameObject.SetActive(true);
            }
            else
            {
                currentWeapon.gameObject.SetActive(false);
            }
            i++;
        }
    }

    //Weapon Image and Ammo text UI
    private void FixedUpdate()
    {
        if(weapon == 0)
        {
            imageR.SetActive(true);
            imageH.SetActive(false);
            textR.SetActive(true);
            textH.SetActive(false);
        }
        else if(weapon == 1)
        {
            imageR.SetActive(false);
            imageH.SetActive(true);
            textR.SetActive(false);
            textH.SetActive(true);
        }
    }
}
