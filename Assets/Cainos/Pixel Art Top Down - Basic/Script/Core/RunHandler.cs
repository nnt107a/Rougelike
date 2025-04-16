using UnityEngine;

public class RunHandler : MonoBehaviour
{
    [SerializeField] private SpawnerHandler spawnerHandler;
    public static int currentWave = 0;
    public int maxWave;

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
}
