using UnityEngine;
using System.Collections;

public class DangerIcon : MonoBehaviour
{
    public float flashedTime;
    private void Awake()
    {
        gameObject.SetActive(false);
    }
    private void Update()
    {
        
    }
    public void Activate(Vector2 position)
    {
        gameObject.SetActive(true);
        transform.position = position;
        GetComponent<SpriteRenderer>().enabled = true;
    }
    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
    public void ReverseState()
    {
        GetComponent<SpriteRenderer>().enabled = !GetComponent<SpriteRenderer>().enabled;
    }
    public bool GetActiveInHierarchy()
    {
        return gameObject.activeInHierarchy;
    }
}
