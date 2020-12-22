using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoDrop : MonoBehaviour
{
    public int ammoUp = 1;

    [SerializeField]
    private float timeAlive = 15f;

    private void OnEnable()
    {
        //AudioManager.instance.Play("");              

        Invoke("Destroy", timeAlive);

        

    }

    private void OnTriggerEnter(Collider other)
    {
        //AudioManager.instance.Play("");
        if (other == PlayerManager.instance.player.GetComponentInChildren<CapsuleCollider>())
        {
            GameObject player = PlayerManager.instance.player;
            GunsAmmo gunsAmmo = player.GetComponentInChildren<GunsAmmo>();

            if (gunsAmmo.ammo < gunsAmmo.ammoMax )
            {
                gunsAmmo.ammo += ammoUp;
                AudioManager.instance.Play("AmmoHealth");

                Destroy();
            }   
           
        }


    }
    public void Destroy()
    {
        //gameObject.SetActive(false);
        Destroy(transform.parent.gameObject);
    }
}
