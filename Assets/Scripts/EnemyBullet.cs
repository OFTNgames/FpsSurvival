using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private Vector3 moveDirection;
    private AudioSource fire;

    [SerializeField]
    private float moveSpeed = 5f;
    [SerializeField]
    private float timeAlive = 3f;
    Transform target;
    public int damage = 20;


    private void OnEnable()
    {
        //AudioManager.instance.Play("EnemyFire");
        fire = GetComponent<AudioSource>();
        fire.Play();
        Invoke("Destroy", timeAlive);
        target = PlayerManager.instance.player.transform;
        
    }

     
    void Update()
    {              
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
            
    }
    

    private void OnTriggerEnter(Collider other) 
    {
        if (other == PlayerManager.instance.player.GetComponentInChildren<CapsuleCollider>())
        {
            PlayerHealth playerStats = PlayerManager.instance.player.GetComponent<PlayerHealth>();
            playerStats.PlayerTakeDamage(damage);
        }

        else if (other.gameObject.tag == "Ground")
        {
            Debug.Log("Hit Ground");
            Destroy();
        }

    }

   

  // void OnCollisionEnter(Collision col)
   // {
        
      //  if (col.gameObject.tag == "Ground")
      //  {
     //       Debug.Log("Hit Ground");
          //  Destroy();
      //  }
  // }


    public void SetMoveDirection(Vector3 dir) 
    {
        moveDirection = dir;
        
    }

    private void Destroy()
    {
        gameObject.SetActive(false);  
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

}
