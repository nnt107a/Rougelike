using UnityEngine;
using System.Collections;

public class SpawnerHandler : MonoBehaviour
{
    public GameObject[] enemies;
    [SerializeField] private float minSpawnCD = 4.0f;
    [SerializeField] private float maxSpawnCD = 5.0f;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private DangerIcon[] dangerIcons;
    private float spawnCD;
    private float spawnTimer = 0;
    private int enemiesCounter;
    ArrayList spawns = new ArrayList();
    private void Awake()
    {
        spawnCD = Random.Range(minSpawnCD * 100, maxSpawnCD * 100 + 1) * 1.0f / 100;
        foreach (GameObject enemy in enemies)
        {
            spawns.Add(enemy);
        }
    }
    private void Update()
    {
        if (spawnTimer < spawnCD)
        {
            spawnTimer += Time.deltaTime;
        }
        else if (enemiesCounter < enemies.Length)
        {
            spawnCD = Random.Range(minSpawnCD * 100, maxSpawnCD * 100 + 1) * 1.0f / 100;
            SpawnEnemy(FindEnemy());
            spawnTimer = 0;
            enemiesCounter += 1;
        }
    }
    private GameObject FindEnemy()
    {
        int rand = Random.Range(0, spawns.Count);
        return (GameObject)spawns[rand];
    }
    private void SpawnEnemy(GameObject enemy)
    {
        StartCoroutine(Danger(enemy));
        spawns.Remove(enemy);
    }
    private IEnumerator Danger(GameObject enemy)
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
    }
    private DangerIcon FindDangerIcon()
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
    public void DecreaseEnemy(GameObject enemy)
    {
        enemiesCounter -= 1;
        spawns.Add(enemy);
    }
}
