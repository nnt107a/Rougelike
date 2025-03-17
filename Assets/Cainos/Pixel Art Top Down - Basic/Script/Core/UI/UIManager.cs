using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Healthbar")]
    [SerializeField] private Image healthBarDisplay;

    [Header("Manabar")]
    [SerializeField] private Image manaBarDisplay;

    [Header("Progressbar")]
    [SerializeField] private Image progressBarDisplay;
    [SerializeField] private RunHandler runHandler;

    [Header("Coin")]
    [SerializeField] private Text coinDisplay;

    [SerializeField] private PlayerAttack playerAttack;
    private void Awake()
    {
    }
    private void Update()
    {
        healthBarDisplay.fillAmount = 1 - playerAttack.currentHealth / playerAttack.startingHealth;
        manaBarDisplay.fillAmount = 1 - playerAttack.mana.currentMana / playerAttack.mana.startingMana;
        progressBarDisplay.fillAmount = runHandler.currentWave * 1.0f / runHandler.maxWave;
        coinDisplay.text = ((int)CoinHandler.instance.coinCounter).ToString();
    }
}
