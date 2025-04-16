using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Healthbar")]
    [SerializeField] private Image healthBarDisplay;

    [Header("Manabar")]
    [SerializeField] private Image manaBarDisplay;

    [Header("Expbar")]
    [SerializeField] private Image expBarDisplay;

    [Header("Progressbar")]
    [SerializeField] private Image progressBarDisplay;
    [SerializeField] private RunHandler runHandler;

    [Header("Shieldbar")]
    [SerializeField] private Image shieldBarDisplay;

    [Header("Coin")]
    [SerializeField] private Text coinDisplay;

    [SerializeField] private PlayerAttack playerAttack;
    private void Awake()
    {
    }
    private void Update()
    {
        shieldBarDisplay.fillAmount = Mathf.Min((playerAttack.currentHealth + playerAttack.shieldValue) / playerAttack.startingHealth, 1);
        healthBarDisplay.fillAmount = playerAttack.currentHealth / Mathf.Max(playerAttack.currentHealth + playerAttack.shieldValue, playerAttack.startingHealth);
        manaBarDisplay.fillAmount = 1 - playerAttack.mana.currentMana / playerAttack.mana.startingMana;
        expBarDisplay.fillAmount = 1 - playerAttack.exp.currentExp / playerAttack.exp.expRequired;
        progressBarDisplay.fillAmount = RunHandler.currentWave * 1.0f / runHandler.maxWave;
        coinDisplay.text = ((int)CoinHandler.instance.coinCounter).ToString();
    }
}
