using UnityEngine;

public class ExpBar : MonoBehaviour
{
    [SerializeField] private float baseExpRequired;
    public int currentLevel;
    public float expRequired;
    public float currentExp;
    private void Awake()
    {
        expRequired = baseExpRequired;
        currentLevel = 1;
        currentExp = 0;
    }
    private void Update()
    {
        if (currentExp >= expRequired)
        {
            currentExp -= expRequired;
            currentLevel += 1;
            CalcExpRequired();
        }
    }
    private void CalcExpRequired()
    {
        expRequired = baseExpRequired * Mathf.Pow(1.2f, currentLevel - 1);
    }
    public void IncExp(float amount)
    {
        currentExp += amount;
    }
}
