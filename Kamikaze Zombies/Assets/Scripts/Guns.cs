using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Attached to indivisual guns
public class Guns : MonoBehaviour
{
    public int damage = 1;
    public float range = 100f;
    public float force = 30f;
    public float rateOfFire = 15f;

    public int maxAmmo = 10;
    public float reloadTime = 3f;
    int ammo;
    bool isReloading = false;

    public Camera cam;
    public ParticleSystem flash;
    public GameObject mFlash;
    public GameObject impactEffect;

    float reloading = 0f;

    public Animator animator;

    public Text ammoDisplay;

    public GameObject killCounter;

    //Max out ammo
    private void Start()
    {
        mFlash.SetActive(false);
        ammo = maxAmmo;
    }

    //Weapon switch glith fix
    private void OnEnable()
    {
        isReloading = false;
        animator.SetBool("Reloading", false);
    }
    
    void Update()
    {
        //Ends if weapon's being reloaded
        if(isReloading)
        {
            return;
        }

        //Force Reload
        if(ammo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }
        //Manual Reload
        else if(Input.GetKeyDown(KeyCode.R) && ammo != maxAmmo)
        {
            StartCoroutine(Reload());
            return;
        }

        //Ammo UI text
        ammoDisplay.text = ammo.ToString();

        //Shoot
        if (Input.GetButton("Fire1"))
        {
            mFlash.SetActive(true);
            flash.Play();
            if (Time.time >= reloading)
            {
                Fire();
                reloading = Time.time + 1f / rateOfFire;
            }
        }   
        //Stop Shooting
        else if(!Input.GetButton("Fire1"))
        {
            flash.Pause();
            mFlash.SetActive(false);
        }
    }

    //Shooting setting
    void Fire()
    {
        ammo--;


        //Cast Ray from gun
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, range))
        {
            //Damage Zombie
            if (hit.transform.CompareTag("Zombie"))
            {
                hit.transform.GetComponent<Zombie>().health = hit.transform.GetComponent<Zombie>().health - damage;
            }

            //Apply force
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * force);
            }

            //Create bullet impact particle effect
            GameObject impactE = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactE, 0.6f);
        }
    }

    //Reload
    IEnumerator Reload()
    {
        isReloading = true;
        animator.SetBool("Reloading", true);
        mFlash.SetActive(false);
        ammoDisplay.text = "Reloading...";

        yield return new WaitForSeconds(reloadTime - 0.25f);
        animator.SetBool("Reloading", false);
        yield return new WaitForSeconds(0.25f);

        ammo = maxAmmo;
        isReloading = false;
    }
}
