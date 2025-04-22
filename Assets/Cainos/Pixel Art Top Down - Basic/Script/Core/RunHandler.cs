using UnityEngine;
using System.Collections.Generic;

public class RunHandler : MonoBehaviour
{
    [SerializeField] private SpawnerHandler spawnerHandler;
    public static int currentWave = 0;
    public static Dictionary<string, Color> rarityColor = new();
    public int maxWave;
    [SerializeField] private GameObject inventory;
    private void Awake()
    {
        rarityColor["Common"] = new Color(0.1960784f, 0.1960784f, 0.1960784f, 1);
        rarityColor["Uncommon"] = new Color(0.3170333f, 0.6698113f, 0.2369616f, 1);
        rarityColor["Epic"] = new Color(0.4560575f, 0.2241456f, 0.6509434f, 1);
        rarityColor["Legendary"] = new Color(0.9339623f, 0.9081489f, 0.383277f, 1);
    }
    public float HealthModifier()
    {
        return Mathf.Pow(1.1f, currentWave - 1);
    }
    public float DamageModifier() 
    {
        return Mathf.Pow(1.05f, currentWave - 1);    
    }
    public float DefModifier()
    {
        return Mathf.Pow(1.05f, currentWave - 1);
    }
    public int EnemiesModifier(int numEnemies)
    {
        return (int)(numEnemies * Mathf.Pow(1.1f, currentWave - 1));
    }
    public static float CollectableModifier()
    {
        return Mathf.Pow(1.1f, currentWave - 1);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventory.SetActive(!inventory.activeInHierarchy);
            Time.timeScale = inventory.activeInHierarchy ? 0 : 1;
            inventory.GetComponent<Inventory>().fromIndex = -1;
            inventory.GetComponent<Inventory>().toIndex = -1;
        }
    }
}
