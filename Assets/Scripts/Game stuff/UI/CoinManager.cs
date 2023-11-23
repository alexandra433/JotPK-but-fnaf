using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.PlayerLoop;

public class CoinManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI numCoins;
    [SerializeField] FloatValue playerCoins;

    void Start()
    {
        UpdateCoinsCount();
    }

    public void UpdateCoinsCount() {
        numCoins.text = "x " + playerCoins.RuntimeValue;
    }
}
