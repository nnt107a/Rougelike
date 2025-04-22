using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Item")]
public class ItemData : ScriptableObject
{
    public string name;
    public string description;
    public string rarity;
    public string piece = "";
    public Sprite sprite;
    public string[] bonus;
    public string[] GetData()
    {
        string[] res = new string[5];
        int cnt = 0;
        foreach (string temp in bonus)
        {
            string stat = temp.Split(",")[1];
            string value = temp.Split(",")[2];
            float fValue = float.Parse(value);
            res[cnt++] = "+" + (fValue * 100).ToString() + "% " + stat;
        }
        return res;
    }
    public void HandleModifyStat(PlayerAttack playerAttack, bool inc)
    {
        foreach (string temp in bonus)
        {
            string stat = temp.Split(",")[0];
            string value = temp.Split(",")[2];
            float fValue = float.Parse(value);
            bool percentage = temp.Split(",")[3] == "1" ? true : false;
            playerAttack.ModifyStat(stat, (percentage ? fValue : fValue * 100) * (inc ? 1 : -1), percentage);
        }
    }
}
