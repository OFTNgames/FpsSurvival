using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth;
    public int health;

    public void PlayerTakeDamage(int damage)
    {
        if (GameManager.instance.gamePlaying)
        {
            AudioManager.instance.Play("TakeDamage");

            health -= damage;
            Debug.Log("Health = " + health.ToString());

            if (health <= 0)
            {
                AudioManager.instance.Play("PlayerDeath");
                FindObjectOfType<GameManager>().GameOver();
            }

        }

    }
    public void PlayerGiveHealth(int healthUp)
    {
        if (GameManager.instance.gamePlaying && health < maxHealth)
        {
            health += healthUp;
            Debug.Log("Health = " + health.ToString());
        }

    }


}
