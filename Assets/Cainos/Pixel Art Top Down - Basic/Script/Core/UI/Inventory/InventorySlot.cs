using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Inventory inventory;
    public ItemData item;
    public bool isEquipment = false;
    public string piece = "";

    [SerializeField] private Image img;
    [SerializeField] private Sprite placeholder;
    
    public void OnPointerEnter(PointerEventData pointer)
    {
        inventory.itemData = item;
        inventory.tooltip.isShowing = true;
        inventory.tooltip.SetData(item);
    }
    public void OnPointerExit(PointerEventData pointer)
    {
        inventory.itemData = null;
        inventory.tooltip.isShowing = false;
        inventory.tooltip.SetData(null);
    }
    public void OnPointerClick(PointerEventData pointer)
    {
        if (inventory.fromIndex == -1)
        {
            if (!item)
            {
                return;
            }
            inventory.fromIndex = inventory.getIndex(this);
            return;
        }
        inventory.toIndex = inventory.getIndex(this);
        inventory.swap();
    }
    private void Update()
    {
        if (item && inventory.getIndex(this) != inventory.fromIndex)
        {
            img.color = new Color(1, 1, 1, 1);
            img.sprite = item.sprite;
        }
        else
        {
            if (!isEquipment)
            {
                img.color = new Color(1, 1, 1, 0);
            }
            else
            {
                img.sprite = placeholder;
            }
        }
    }
}
