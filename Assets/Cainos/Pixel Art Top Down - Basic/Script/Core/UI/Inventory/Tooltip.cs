using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    [SerializeField] private Text _name;
    [SerializeField] private Text description;
    [SerializeField] private Text rarity;
    [SerializeField] private Text stats;
    private RectTransform rect;
    public bool isShowing = false;
    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        gameObject.SetActive(false);
    }
    public void SetData(ItemData item)
    {
        if (!item)
        {
            gameObject.SetActive(false);
            return;
        }
        gameObject.SetActive(true);
        _name.text = item.name;
        description.text = item.description;
        rarity.text = item.rarity;
        rarity.color = RunHandler.rarityColor[rarity.text];
        stats.text = "";
        string[] temp = item.GetData();
        for (int i = 0; i < 5; i++)
        {
            if (temp[i] != null && temp[i] != "")
            {
                stats.text = stats.text + temp[i] + "\n";
            }
        }
    }
    public void setPosition()
    {
        rect.position = new Vector3(Input.mousePosition.x + 60,
                                    Input.mousePosition.y - 70,
                                    0);
    }
    private void Update()
    {
        if (isShowing)
        {
            setPosition();
        }
    }
}
