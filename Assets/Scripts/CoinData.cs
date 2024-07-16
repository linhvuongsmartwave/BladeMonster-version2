using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gold Data", menuName = "Gold Data")]

public class CoinData : ScriptableObject
{
    [HideInInspector] public int coin;
}
