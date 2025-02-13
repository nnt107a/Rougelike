using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Image healthBarDisplay;
    [SerializeField] private GameObject player;
    private PlayerAttack playerAttack;
    private void Awake()
    {
        playerAttack = player.GetComponent<PlayerAttack>();
    }
    private void Update()
    {
        healthBarDisplay.fillAmount = 1 - playerAttack.currentHealth / playerAttack.startingHealth;
    }
}
