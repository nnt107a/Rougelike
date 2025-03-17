using UnityEngine;

public class RunHandler : MonoBehaviour
{
    [SerializeField] private SpawnerHandler spawnerHandler;
    public int currentWave;
    public int maxWave;

    public float HealthModifier()
    {
        return Mathf.Pow(1.1f, currentWave - 1);
    }
    public float DamageModifier() 
    {
        return Mathf.Pow(1.02f, currentWave - 1);    
    }
    public int EnemiesModifier(int numEnemies)
    {
        return (int)(numEnemies * Mathf.Pow(1.2f, currentWave - 1));
    }
}
