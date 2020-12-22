using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyPool : MonoBehaviour
{
    public static enemyPool enemyPoolInstance;
    public int xPos;
    public int zPos;
    public int enemyCount;
    public int enemySpawnCap = 4;
    public float spawnRate = 5f;

    [SerializeField]
    private GameObject pooledEnemy;
    private bool notEnoughEnemyInPool = true;
    private Queue<GameObject> availableObjects = new Queue<GameObject>();
    public List<GameObject> enemy;

    private void Awake()
    {
        enemyPoolInstance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        enemy = new List<GameObject>();
        
        StartCoroutine(SpawnCap());
          
    }

    
    IEnumerator SpawnCap()
    {
        while (true)
        {
            //StartCoroutine(EnemyDrop());
            yield return new WaitForSeconds(spawnRate);
            enemySpawnCap += 1;
            //Debug.Log(enemySpawnCap);
        }
    }



    void Update()
    {


        enemyCount = enemy.Count;

        if (enemyCount < enemySpawnCap)
            {
            StartCoroutine(EnemyDrop());
            }
        }
    IEnumerator EnemyDrop()
    {
        xPos = Random.Range(80, 105);
        zPos = Random.Range(-35, -47);

        GameObject newEnemy = enemyPool.enemyPoolInstance.GetEnemy();
        newEnemy.transform.position = new Vector3(xPos, 3, zPos);
        newEnemy.SetActive(true);

        yield return new WaitForSeconds(0.5f);
        enemyCount += 1;
    }




    public GameObject GetEnemy()
    {
        if (enemy.Count > 0)
        {
            for (int i = 0; i < enemy.Count; i++)
            {
                if (!enemy[i].activeInHierarchy)
                {
                   // pooledEnemy = availableObjects.Dequeue();
                    return enemy[i];

                }
            }
        }

        if (notEnoughEnemyInPool)
        {
            GameObject bul = Instantiate(pooledEnemy);
            bul.SetActive(false);
           // availableObjects.Enqueue(pooledEnemy);
            enemy.Add(bul);
            return bul;
        }

        return null;

    }


   
}
