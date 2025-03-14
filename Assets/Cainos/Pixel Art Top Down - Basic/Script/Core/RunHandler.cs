using UnityEngine;

public class RunHandler : MonoBehaviour
{
    public int currentWave = 0;

    public float healthModifier()
    {
        return Mathf.Pow(1.1f, currentWave);
    }
    public float damageModifier() 
    {
        return Mathf.Pow(1.02f, currentWave);    
    }
}
