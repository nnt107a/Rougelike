using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnerHandler : MonoBehaviour
{
    [SerializeField] private float minSpawnerCD = 1.0f;
    [SerializeField] private float maxSpawnerCD = 1.5f;
    [SerializeField] private DangerIcon[] dangerIcons;
    [SerializeField] private Spawner[] spawnersList;
    [SerializeField] private RunHandler runHandler;
    [SerializeField] private int baseNumEnemies;
    [SerializeField] private GameObject enemySelection;
    private List<Spawner> spawners;
    private int numEnemies;
    private int enemiesCounter;
    private float spawnerCD = 1.5f;
    private float spawnerTimer = 0;
    private int spawnedCounter;
    private int constraintSpawnedCounter;
    private bool waiting;
    private void Awake()
    {
        waiting = true;
        enemySelection.SetActive(true);
        enemySelection.GetComponent<EnemySelection>().Gen();
        Time.timeScale = 0;
    }
    private void NextWave()
    {
        int cnt = 0;
        spawners = new List<Spawner>();
        for (int i = 0; i < spawnersList.Length; i++)
        {
            if (spawnersList[i].enabled)
            {
                spawners.Add(spawnersList[i]);
            }
        }
        foreach (Spawner spawner in spawners)
        {
            if (spawner.enabled)
            {
                cnt += 1;
            }
        }
        foreach (Spawner spawner in spawners)
        {
            if (spawner.enabled)
            {
                spawner.minSpawnCount = numEnemies / 2 / cnt;
                constraintSpawnedCounter += spawner.minSpawnCount;
            }
        }
    }
    private void Update()
    {
        if (waiting)
        {
            return;
        }
        if (spawnedCounter == numEnemies)
        {
            if (enemiesCounter == 0)
            {
                waiting = true;
                StartCoroutine(CounterToSelection());
            }
            return;
        }
        if (spawnerTimer < spawnerCD)
        {
            spawnerTimer += Time.deltaTime;
        }
        else
        {
            spawnerCD = Random.Range(minSpawnerCD * 10, maxSpawnerCD * 10 + 1) / 10.0f;
            spawnerTimer = 0;
            if (spawnedCounter < constraintSpawnedCounter)
            {
                while (true)
                {
                    int rand = Random.Range(0, spawners.Count);
                    if (spawners[rand].minSpawnCount != 0 && spawners[rand].ExistEnemies())
                    {
                        spawners[rand].SpawnEnemy(spawners[rand].FindEnemy());
                        break;
                    }
                }
            }
            else
            {
                int rand = Random.Range(0, spawners.Count);
                if (spawners[rand].ExistEnemies())
                {
                    spawners[rand].SpawnEnemy(spawners[rand].FindEnemy());
                }
            }
        }
    }

    public IEnumerator Danger(GameObject enemy)
    {
        Enemy tmpComp = enemy.GetComponent<Enemy>();
        Vector2 tmpPos = tmpComp.CalcSpawnPos();
        DangerIcon tmpIcon = FindDangerIcon();
        tmpIcon.Activate(tmpPos);
        yield return new WaitForSeconds(tmpIcon.flashedTime);
        tmpIcon.ReverseState();
        yield return new WaitForSeconds(tmpIcon.flashedTime);
        tmpIcon.ReverseState();
        yield return new WaitForSeconds(tmpIcon.flashedTime);
        tmpIcon.Deactivate();
        tmpComp?.Spawn(tmpPos);
        spawnedCounter += 1;
        enemiesCounter += 1;
    }

    public IEnumerator CounterToSelection()
    {
        yield return new WaitForSeconds(1);
        enemySelection.SetActive(true);
        enemySelection.GetComponent<EnemySelection>().Gen();
        Time.timeScale = 0;
    }
    public DangerIcon FindDangerIcon()
    {
        for (int i = 0; i < dangerIcons.Length; i++)
        {
            if (!dangerIcons[i].GetActiveInHierarchy())
            {
                return dangerIcons[i];
            }
        }
        return dangerIcons[0];
    }
    public void SetNumEnemy(int num)
    {
        numEnemies = num;
    }
    public void DecreaseEnemy(GameObject enemy)
    {
        foreach (Spawner spawner in spawners)
        {
            if (spawner.name.Split(" ")[0] == enemy.name.Split(" ")[0])
            {
                spawner.ReturnEnemy(enemy);
                enemiesCounter -= 1;
                return;
            }
        }
    }
    private IEnumerator CounterToNextWave()
    {
        yield return new WaitForSeconds(5);
        runHandler.currentWave += 1;
        constraintSpawnedCounter = 0;
        spawnedCounter = 0;
        numEnemies = runHandler.EnemiesModifier(baseNumEnemies);
        NextWave();
        waiting = false;
    }
    public void ActiveNextWave()
    {
        StartCoroutine(CounterToNextWave());
    }
}
