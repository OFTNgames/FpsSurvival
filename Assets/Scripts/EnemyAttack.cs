using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float lookRadius = 8f;
    Transform target;
    Vector3 bulletSpawnPoint; 
    public float fireRate = 4f;
    private float nextTimeToFire = 0f;
    public float spawnPointM = 1f;
    //Vector3 bulletMoveDirection;
    

    
    void Start()
    {
        target = PlayerManager.instance.player.transform;
       

    }

    
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);
       
        if (distance <= lookRadius && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;

            AttackTarget();

        }

    }

    void AttackTarget()
    {
        Vector3 bulletMoveDirection = (target.position - transform.position).normalized;
        //Debug.DrawRay(transform.position, bulletMoveDirection, Color.green);
        bulletSpawnPoint = Vector3.MoveTowards(transform.position, target.position, spawnPointM);

        GameObject bul = BulletPool.bulletPoolInstance.GetBullet();
        bul.transform.position = (bulletSpawnPoint);
        bul.SetActive(true);
        bul.GetComponent<EnemyBullet>().SetMoveDirection(bulletMoveDirection);
        




    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, lookRadius);



    }



}
