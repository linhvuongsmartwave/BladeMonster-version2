using TMPro;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public int gold;

    private const string Gold = "Gold";


    private void Start()
    {
        LoadGold();
    }

    private void Update()
    {
    }

    public void SaveGold()
    {
        PlayerPrefs.SetInt(Gold, gold);
        PlayerPrefs.Save();
    }

    public void LoadGold()
    {
        if (PlayerPrefs.HasKey(Gold))
        {
            gold = PlayerPrefs.GetInt(Gold);
        }
    }
}
