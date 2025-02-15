using UnityEngine;

public class Mana : MonoBehaviour
{
    [SerializeField] public float startingMana;
    [SerializeField] private float manaRegenerateSpeed;
    public float currentMana { get; private set; }
    private void Awake()
    {
        currentMana = startingMana;
    }
    private void Update()
    {
        currentMana = Mathf.Clamp(currentMana + manaRegenerateSpeed * Time.deltaTime, 0, startingMana);
    }
    public void UseMana(float _change)
    {
        currentMana = Mathf.Clamp(currentMana - _change, 0, startingMana);
    }
}
