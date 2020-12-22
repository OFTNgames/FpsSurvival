using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{

    public int healthDrop = 20;
    
    [SerializeField]
    private float timeAlive = 15f;

    private void OnEnable()
    {
        //AudioManager.instance.Play("");              
        
        Invoke("Destroy", timeAlive);

    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other == PlayerManager.instance.player.GetComponentInChildren<CapsuleCollider>())
        {
            GameObject player = PlayerManager.instance.player;
            PlayerHealth playerHealth = player.GetComponentInChildren<PlayerHealth>();
            
            if(playerHealth.health < playerHealth.maxHealth)
            {
                AudioManager.instance.Play("AmmoHealth");
                PlayerHealth playerStats = PlayerManager.instance.player.GetComponent<PlayerHealth>();
                playerStats.PlayerGiveHealth(healthDrop);
                Destroy();

            }
            
            
            
        }

        
    }
    public void Destroy()
    {
        //gameObject.SetActive(false);
        Destroy(transform.parent.gameObject);
    }

    //private void OnDisable()
    //{
    //    CancelInvoke();
    //}

}
