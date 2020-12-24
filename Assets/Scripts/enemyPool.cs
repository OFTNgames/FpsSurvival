using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyPool : MonoBehaviour
{
    public static enemyPool enemyPoolInstance;
    public int xPos;
    public int zPos;
    public int enemySpawnCap = 4;
    public float spawnRate = 5f;

    [SerializeField] private GameObject enemyPrefab;
    private bool notEnoughEnemyInPool = true;
    public int enemyCount;
    private List<GameObject> enemies;

    private void Awake() => enemyPoolInstance = this;

    void Start()
    {
        enemies = new List<GameObject>();
        StartCoroutine(SpawnCap());
    }
    
    IEnumerator SpawnCap()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRate);
            enemySpawnCap += 1;
        }
    }

    void Update()
    {
        enemyCount = enemies.Count;
        if (enemyCount < enemySpawnCap)
        {
            StartCoroutine(EnemyDrop());
        }
    }

    IEnumerator EnemyDrop()
    {
        xPos = Random.Range(80, 105);
        zPos = Random.Range(-35, -47);

        GameObject newEnemy = GetEnemy();
        newEnemy.transform.position = new Vector3(xPos, 3, zPos);
        newEnemy.SetActive(true);

        yield return new WaitForSeconds(0.5f);
        enemyCount += 1;
    }

    public GameObject GetEnemy()
    {
        if (enemies.Count > 0)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (!enemies[i].activeInHierarchy)
                {
                    return enemies[i];
                }
            }
        }

        if (notEnoughEnemyInPool)
        {
            GameObject newEnemy = Instantiate(enemyPrefab);
            newEnemy.SetActive(false);
            enemies.Add(newEnemy);
            return newEnemy;
        }
        return null;
    }
}
