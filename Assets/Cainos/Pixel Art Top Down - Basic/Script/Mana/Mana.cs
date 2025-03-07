using UnityEngine;
using System.Collections;

public class Mana : MonoBehaviour
{
    [SerializeField] public float startingMana;
    [SerializeField] private float manaRegenerateSpeed;
    public float currentMana { get; private set; }
    private void Awake()
    {
        currentMana = startingMana;
        StartCoroutine(RecoverMana());
    }
    private void Update()
    {
        
    }
    private IEnumerator RecoverMana()
    {
        currentMana = Mathf.Clamp(currentMana + manaRegenerateSpeed, 0, startingMana);
        yield return new WaitForSeconds(1);
        StartCoroutine(RecoverMana());
    }
    public void UseMana(float _change)
    {
        currentMana = Mathf.Clamp(currentMana - _change, 0, startingMana);
    }
}
