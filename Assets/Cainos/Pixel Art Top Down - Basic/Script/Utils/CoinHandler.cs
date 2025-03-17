using UnityEngine;

public class CoinHandler : MonoBehaviour
{
    public static CoinHandler instance;
    public float coinCounter { get; private set; }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void IncCoin(float amount)
    {
        coinCounter += amount;
    }
    public void DecCoin(float amount)
    {
        coinCounter -= amount;
    }
}
