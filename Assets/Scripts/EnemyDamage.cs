using UnityEngine;
using System.Collections;
using System;
 



public class EnemyDamage : MonoBehaviour
{
    public float enemyHealth = 30f;
    private float enemyCurrentHealth;    
    public int randomNumber;

    [SerializeField]
    private GameObject HealthPickup;
    [SerializeField]
    private GameObject AmmoPickup;

    public float healthChance = 0.15f;
    public float ammoChance = 0.30f;
    
    [SerializeField]
    private float flashTime = 0.5f;
    public Material[] pubmaterial;
    MeshRenderer mr;
   

    public event Action<float> OnHealthPctChange = delegate { };

    

    private void OnEnable()
    {
        enemyCurrentHealth = enemyHealth;

        mr = GetComponentInChildren<MeshRenderer>();
        mr.sharedMaterial = pubmaterial[0];
                  
    }



    public void EnemyTakeDamage(float amount) 
    {
        
        enemyCurrentHealth -= amount;
        float currentHealthPct = (float)enemyCurrentHealth / (float)enemyHealth;
        OnHealthPctChange(currentHealthPct);

        AudioManager.instance.Play("EnemyOnHit");

        mr.sharedMaterial = pubmaterial[1];


        if(enemyCurrentHealth <= 0f) 
        {
            AudioManager.instance.Play("EnemyOnDeath");

            randomNumber = UnityEngine.Random.Range(0, 100);
            
            if(randomNumber <= 50)
            {
                if (UnityEngine.Random.value <= healthChance)
                {
                    Instantiate(HealthPickup, transform.position, transform.rotation);
                }

            }
            else 
            {
                if (UnityEngine.Random.value <= ammoChance)
                {
                    Instantiate(AmmoPickup, transform.position, transform.rotation);
                }
            }
            
            

            Die();
        }
        else 
        {
            Invoke("ResetMaterial", flashTime);
        }
        

    }

    void ResetMaterial() 
    {
        //mr.material = matDefault;
        mr.sharedMaterial = pubmaterial[0];
    }

    void Die() 
    {
        
        
        gameObject.SetActive(false);
        GameObject theEnemyPool = GameObject.Find("EnemyPool");
        enemyPool ePool = theEnemyPool.GetComponent<enemyPool>();

        KillcounterController.instance.IncreaseKillCount();

        //ePool.enemyCount -= 1;
        //Debug.Log(ePool);
        enemyPool.enemyPoolInstance.enemyCount = enemyPool.enemyPoolInstance.enemyCount - 1; 

    }
}
