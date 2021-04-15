using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//Attached to Player
public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int health;
    public int dMultiplier;

    public GameObject healthBar;

    //Set health to full
    private void Start()
    {
        health = maxHealth;
    }
    void Update()
    {
        //Death Condition
        if(health <= 0)
        {
            GameOver();
        }

        //Updating HealthBar UI
        healthBar.GetComponent<Image>().fillAmount = (float)health / (float)maxHealth;
        if(health > 90)
        {
            healthBar.GetComponent<Image>().color = new Color32(0, 255, 0, 255);
        }
        else if(health > 60)
        {
            healthBar.GetComponent<Image>().color = new Color32(255, 255, 0, 255);
        }
        else
        {
            healthBar.GetComponent<Image>().color = new Color32(255, 0, 0, 255);
        }
    }

    //Referneced in Zombie Script
    public void Damage(int damage)
    {
        health -= (damage * dMultiplier);
    }

    //Loss
    void GameOver()
    {
        SceneManager.LoadScene(3);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
