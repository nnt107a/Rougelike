using System.Collections.Generic;
using UnityEngine;

public class EnemySelection : MonoBehaviour
{
    [SerializeField] public Sprite[] sprites;
    [SerializeField] public GameObject[] spawners;
    [SerializeField] private EnemySelect[] enemySelects;
    [SerializeField] public SpawnerHandler spawnerHandler;
    public int[] ints1 = new int[4];
    public int[] ints2 = new int[4];
    private void Awake()
    {
    }
    private void Update()
    {
    }
    public void Gen()
    {
        List<int> list = new List<int>();
        for (int i = 0; i < spawners.Length - 1; i++)
        {
            for (int j = i + 1; j < spawners.Length; j++)
            {
                list.Add(i * spawners.Length + j);
            }
        }
        for (int i = 0; i < 3; i++)
        {
            int rand = Random.Range(0, list.Count);
            ints1[i] = list[rand] / spawners.Length;
            ints2[i] = list[rand] % spawners.Length;
            list.RemoveAt(rand);
        }
        for (int i = 0; i < 3; i++)
        {
            enemySelects[i].Activate();
        }
    }
}
