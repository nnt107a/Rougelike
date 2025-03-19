using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EnemySelect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private int pos;
    [SerializeField] private Image[] images;
    [SerializeField] private GameObject enemySelection;
    private Text text;
    private RectTransform rectTransform;
    private void Awake()
    {
        text = GetComponent<Text>();
        rectTransform = GetComponent<RectTransform>();
    }
    private void Update()
    {
    }
    public void Activate()
    {
        images[0].sprite = enemySelection.GetComponent<EnemySelection>().sprites[enemySelection.GetComponent<EnemySelection>().ints1[pos]];
        images[1].sprite = enemySelection.GetComponent<EnemySelection>().sprites[enemySelection.GetComponent<EnemySelection>().ints2[pos]];
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
