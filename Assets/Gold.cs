using TMPro;
using UnityEngine;

public class Gold : Singleton<Gold>
{
    public int gold;

    private const string goldstring = "goldstring";


    private void Start()
    {
        LoadGold();
    }

    private void Update()
    {
    }

    public void SaveGold()
    {
        PlayerPrefs.SetInt(goldstring, gold);
        PlayerPrefs.Save();
    }

    public void BuyGold(int gold)
    {
        this.gold += gold;
        SaveGold();
    }

    public void LoadGold()
    {
        if (PlayerPrefs.HasKey(goldstring))
        {
            gold = PlayerPrefs.GetInt(goldstring);
        }
    }
}
