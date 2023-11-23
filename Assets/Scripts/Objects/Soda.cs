using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soda : PowerUp {
    [SerializeField] float speedMultiplier;
    [SerializeField] FloatValue playerSpeed;

    void OnTriggerEnter2D (Collider2D other) {
        if (other.CompareTag("Player")) {
            playerSpeed.RuntimeValue *= speedMultiplier;
            powerUpSignal.Raise(); // tell the ui to show the powerUp
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine(SpeedBoostCo());
        }
    }

    IEnumerator SpeedBoostCo() {
        yield return new WaitForSeconds(16f);
        playerSpeed.RuntimeValue = playerSpeed.initialValue;
    }
}
