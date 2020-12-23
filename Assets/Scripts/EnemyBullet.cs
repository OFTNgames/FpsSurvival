using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour, ICanTakeDamage
{
    private Vector3 moveDirection;
    private AudioSource fire;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float timeAlive = 3f;
    private Transform target;
    public int damage = 20;

    private void OnEnable()
    {
        fire = GetComponent<AudioSource>();
        fire.Play();
        Invoke("Deactivate", timeAlive);
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
            Deactivate();
        }
    }

    public void SetMoveDirection(Vector3 dir) 
    {
        moveDirection = dir;
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);  
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    public void TakeDamage(float amount)
    {
        Deactivate();
    }
}
