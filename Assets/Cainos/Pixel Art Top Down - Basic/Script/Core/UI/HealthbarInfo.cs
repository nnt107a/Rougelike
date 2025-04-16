using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HealthbarInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Text attributeText;
    [SerializeField] private PlayerAttack playerAttack;
    private void Awake()
    {
    }
    private void Update()
    {
        
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        attributeText.enabled = true;
        attributeText.text = Mathf.RoundToInt(playerAttack.currentHealth).ToString() + 
                             (playerAttack.shieldValue > 0 ? "+" + Mathf.RoundToInt(playerAttack.shieldValue).ToString() : "") 
                             + "/" + Mathf.RoundToInt(playerAttack.startingHealth).ToString();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        attributeText.enabled = false;
    }
}
