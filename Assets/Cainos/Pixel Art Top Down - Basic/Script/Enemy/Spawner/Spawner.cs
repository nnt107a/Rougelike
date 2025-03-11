using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] enemies;
    [SerializeField] protected Transform playerTransform;
    [SerializeField] protected SpawnerHandler spawnerHandler;
    public int minSpawnCount;
    protected ArrayList spawns = new ArrayList();
    public int activeEnemies {get; protected set;}

    protected void Awake()
    {
        foreach (GameObject enemy in enemies)
        {
            spawns.Add(enemy);
        }
    }
    public GameObject FindEnemy()
    {
        foreach (GameObject enemy in spawns)
        {
            if (!enemy.activeInHierarchy)
            {
                return enemy;
            }
        }
        return (GameObject)spawns[0];
    }
    public void SpawnEnemy(GameObject enemy)
    {
        StartCoroutine(spawnerHandler.Danger(enemy));
        spawns.Remove(enemy);
        activeEnemies += 1;
    }
    public void ReturnEnemy(GameObject enemy)
    {
        spawns.Add(enemy);
        activeEnemies -= 1;
    }
    public bool ExistEnemies()
    {
        return spawns.Count > 0;
    }
}
