using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] public float startingHealth;
    public float currentHealth { get; protected set; }
    protected void Awake()
    {
        currentHealth = startingHealth;
    }
    protected void Update()
    {
        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }
    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, startingHealth);
    }
}
