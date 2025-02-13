using UnityEngine;

public class Destroyable : Health
{
    [SerializeField] private Sprite[] sprites;
    private void Awake()
    {
        base.Awake();
    }
    private void Update()
    {
        base.Update();
        if (currentHealth < startingHealth)
        {
            GetComponent<SpriteRenderer>().sprite = sprites[1];
        }
    }
}
