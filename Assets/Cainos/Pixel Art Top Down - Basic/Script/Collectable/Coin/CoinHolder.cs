using UnityEngine;

public class CoinHolder : MonoBehaviour
{
    [SerializeField] private GameObject[] coins;
    [SerializeField] private PlayerAttack playerAttack;
    private void Awake()
    {
    }
    private void Update()
    {
    }
    public void Spawn(Vector3 position)
    {
        FindCoin().GetComponent<Coin>().Spawn(position, playerAttack.coinDropRate);
    }
    private GameObject FindCoin()
    {
        for (int i = 0; i < coins.Length; i++)
        {
            if (!coins[i].activeInHierarchy)
            {
                return coins[i];
            }
        }
        return coins[0];
    }
}
