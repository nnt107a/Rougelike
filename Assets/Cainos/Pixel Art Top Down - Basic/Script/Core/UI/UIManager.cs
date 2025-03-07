using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Healthbar")]
    [SerializeField] private Image healthBarDisplay;

    [Header("Manabar")]
    [SerializeField] private Image manaBarDisplay;

    [SerializeField] private PlayerAttack playerAttack;
    private void Awake()
    {
    }
    private void Update()
    {
        healthBarDisplay.fillAmount = 1 - playerAttack.currentHealth / playerAttack.startingHealth;
        manaBarDisplay.fillAmount = 1 - playerAttack.mana.currentMana / playerAttack.mana.startingMana;
    }
}
