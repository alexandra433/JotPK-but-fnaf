using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : PowerUp
{
    [SerializeField] FloatValue playerHealth;
    private float amountToIncrease = 1;

    void OnTriggerEnter2D (Collider2D other) {
        if (other.CompareTag("Player")) {
            pickedUp = true;
            playerHealth.RuntimeValue += amountToIncrease;
            powerUpSignal.Raise();
            Destroy(this.gameObject);
        }
    }
}
