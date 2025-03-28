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
        floatValue = float.Parse(value);
        attribute += displayName + " by " + floatValue * 100 + "%";
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
        playerAttack.ModifyStat(name, floatValue, true);
        levelUpSelection.SetActive(false);
        Time.timeScale = 1;
    }
}
