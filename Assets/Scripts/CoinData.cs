using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New goldstring Data", menuName = "goldstring Data")]

public class CoinData : ScriptableObject
{
    [HideInInspector] public int coin;
}
