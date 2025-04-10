using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LevelUpSelect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private int pos;
    [SerializeField] private Image image;
    [SerializeField] private GameObject levelUpSelection;
    [SerializeField] private PlayerAttack playerAttack;
    private string attribute;
    private Text txt;
    private RectTransform rectTransform;
    private string name;
    private float floatValue;
    private bool percentage;
    private void Awake()
    {
        txt = GetComponentInChildren<Text>();
        rectTransform = GetComponent<RectTransform>();
    }
    private void Update()
    {
    }
    public void Activate()
    {
        attribute = "Increase your ";
        ArrayList tmp = new ArrayList();
        LevelUpSelection levelUp = levelUpSelection.GetComponent<LevelUpSelection>();
        int p = levelUp.ints[pos];
        image.sprite = levelUp.sprites[p];
        name = levelUp.attributes[p].Split(",")[0];
        string displayName = levelUp.attributes[p].Split(",")[1];
        string value = levelUp.attributes[p].Split(",")[2];
        percentage = levelUp.attributes[p].Split(",")[3] == "1" ? true : false;
        if (value[value.Length - 1] != '%')
        {
            floatValue = float.Parse(value);
            attribute += displayName + " by " + floatValue * 100 + "%";
        }
        else
        {
            floatValue = float.Parse(value.Substring(0, value.Length - 1));
            attribute += displayName + " by " + value;
        }
        
        txt.text = attribute;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        rectTransform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        rectTransform.localScale = Vector3.one;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        playerAttack.ModifyStat(name, floatValue, percentage);
        levelUpSelection.SetActive(false);
        Time.timeScale = 1;
    }
}
