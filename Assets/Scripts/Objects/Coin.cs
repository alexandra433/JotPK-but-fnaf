using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : PowerUp
{
    [SerializeField] FloatValue playerCoins;
    [SerializeField] float amountToIncrease;

    void OnTriggerEnter2D (Collider2D other) {
        if (other.CompareTag("Player")) {
            playerCoins.RuntimeValue += amountToIncrease;
            powerUpSignal.Raise();
            Destroy(this.gameObject);
        }
    }
}
