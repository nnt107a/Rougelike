using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class EnemySelect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private int pos;
    [SerializeField] private Image[] images;
    [SerializeField] private GameObject enemySelection;
    private string attribute;
    private Text txt;
    private RectTransform rectTransform;
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
        attribute = "Attribute: ";
        ArrayList tmp = new ArrayList();
        int p1 = enemySelection.GetComponent<EnemySelection>().ints1[pos];
        int p2 = enemySelection.GetComponent<EnemySelection>().ints2[pos];
        images[0].sprite = enemySelection.GetComponent<EnemySelection>().sprites[p1];
        images[1].sprite = enemySelection.GetComponent<EnemySelection>().sprites[p2];
        foreach (string attr in enemySelection.GetComponent<EnemySelection>().spawners[p1].GetComponent<Spawner>().attributes)
        {
            tmp.Add(attr);
        }
        foreach (string attr in enemySelection.GetComponent<EnemySelection>().spawners[p2].GetComponent<Spawner>().attributes)
        {
            if (!tmp.Contains(attr))
            {
                tmp.Add(attr);
            }
        }
        foreach (string item in tmp)
        {
            attribute = attribute + item + ", ";
        }
        attribute = attribute.Remove(attribute.Length - 2);
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
        for (int i = 0; i < enemySelection.GetComponent<EnemySelection>().spawners.Length; i++)
        {
            if (i != enemySelection.GetComponent<EnemySelection>().ints1[pos] && i != enemySelection.GetComponent<EnemySelection>().ints2[pos])
            {
                enemySelection.GetComponent<EnemySelection>().spawners[i].GetComponent<Spawner>().enabled = false;
                enemySelection.GetComponent<EnemySelection>().spawners[i].SetActive(false);
            }
            else
            {
                enemySelection.GetComponent<EnemySelection>().spawners[i].GetComponent<Spawner>().enabled = true;
                enemySelection.GetComponent<EnemySelection>().spawners[i].SetActive(true);
            }
        }
        enemySelection.GetComponent<EnemySelection>().spawnerHandler.ActiveNextWave();
        enemySelection.SetActive(false);
        Time.timeScale = 1;
    }
}
