using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public InventorySlot[] inventorySlots;
    public Image virtualSlot;
    public RectTransform virtualTransform;
    public Tooltip tooltip;
    public PlayerAttack playerAttack;
    public int fromIndex = -1;
    public int toIndex = -1;
    public ItemData itemData;
    public int getIndex(InventorySlot slot)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i] == slot)
            {
                return i;
            }
        }
        return -1;
    }
    public void swap()
    {
        bool validSwap = true;
        if (inventorySlots[fromIndex].isEquipment || inventorySlots[toIndex].isEquipment)
        {
            if (inventorySlots[toIndex].item && inventorySlots[fromIndex].piece != "")
            {
                if (inventorySlots[fromIndex].piece != inventorySlots[toIndex].item.piece)
                {
                    validSwap = false;
                }
            }
            if (inventorySlots[fromIndex].item && inventorySlots[toIndex].piece != "")
            {
                if (inventorySlots[toIndex].piece != inventorySlots[fromIndex].item.piece)
                {
                    validSwap = false;
                }
            }
        }
        if (validSwap)
        {
            if (inventorySlots[fromIndex].isEquipment)
            {
                inventorySlots[fromIndex].item.HandleModifyStat(playerAttack, false);
                if (inventorySlots[toIndex].item)
                {
                    inventorySlots[toIndex].item.HandleModifyStat(playerAttack, true);
                }
            }
            if (inventorySlots[toIndex].isEquipment)
            {
                inventorySlots[fromIndex].item.HandleModifyStat(playerAttack, true);
                if (inventorySlots[toIndex].item)
                {
                    inventorySlots[toIndex].item.HandleModifyStat(playerAttack, false);
                }
            }
            ItemData temp = inventorySlots[fromIndex].item;
            inventorySlots[fromIndex].item = inventorySlots[toIndex].item;
            inventorySlots[toIndex].item = temp;
        }
        fromIndex = -1;
        toIndex = -1;
    }
    private void Update()
    {
        if (fromIndex == -1)
        {
            virtualSlot.color = new Color(1, 1, 1, 0);
            tooltip.enabled = true;
        }
        else
        {
            virtualSlot.color = new Color(1, 1, 1, 1);
            virtualSlot.sprite = inventorySlots[fromIndex].item.sprite;
            virtualTransform.position = Input.mousePosition;
            tooltip.enabled = false;
        }
    }
}
