using System.Collections.Generic;
using UnityEngine;

public class LevelUpSelection : MonoBehaviour
{
    [SerializeField] public Sprite[] sprites;
    [SerializeField] private LevelUpSelect[] levelUpsSelects;
    public string[] attributes;
    public int[] ints = new int[4];
    private void Awake()
    {
    }
    private void Update()
    {
    }
    public void Gen()
    {
        List<int> list = new List<int>();
        for (int i = 0; i < attributes.Length; i++)
        {
            list.Add(i);
        }
        for (int i = 0; i < 3; i++)
        {
            int rand = Random.Range(0, list.Count);
            ints[i] = list[rand];
            list.RemoveAt(rand);
        }
        for (int i = 0; i < 3; i++)
        {
            levelUpsSelects[i].Activate();
        }
    }
}
