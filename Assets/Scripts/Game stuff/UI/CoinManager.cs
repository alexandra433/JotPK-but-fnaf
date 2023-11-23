using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI numCoins;
    [SerializeField] FloatValue playerCoins;

    public void UpdateCoinsCount() {
        numCoins.text = "x " + playerCoins.RuntimeValue;
    }
}
