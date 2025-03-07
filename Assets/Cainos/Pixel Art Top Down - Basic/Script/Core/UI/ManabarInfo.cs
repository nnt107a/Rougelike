using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ManabarInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Text attributeText;
    [SerializeField] private Mana playerMana;
    private void Awake()
    {
    }
    private void Update()
    {

    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        attributeText.enabled = true;
        attributeText.text = Mathf.RoundToInt(playerMana.currentMana).ToString() + "/" +
                             Mathf.RoundToInt(playerMana.startingMana).ToString();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        attributeText.enabled = false;
    }
}
