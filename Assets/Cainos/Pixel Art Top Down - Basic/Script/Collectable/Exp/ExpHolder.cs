using UnityEngine;

public class ExpHolder : MonoBehaviour
{
    [SerializeField] private GameObject[] exps;
    [SerializeField] private PlayerAttack playerAttack;
    private void Awake()
    {
    }
    private void Update()
    {
    }
    public void Spawn(Vector3 position)
    {
        FindExp().GetComponent<Exp>().Spawn(position, 1);
    }
    private GameObject FindExp()
    {
        for (int i = 0; i < exps.Length; i++)
        {
            if (!exps[i].activeInHierarchy)
            {
                return exps[i];
            }
        }
        return exps[0];
    }
}
