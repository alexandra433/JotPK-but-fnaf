using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soda : UsableItem {
    [SerializeField] float speedMultiplier;
    [SerializeField] FloatValue playerSpeed;
    public UsableItemManager usableItemManager;

    public override void ActivateItem() {
        if (isUsable) {
            isUsable = false;
            playerSpeed.RuntimeValue *= speedMultiplier;
            StartCoroutine(SpeedBoostCo());
        }
    }

    void OnTriggerEnter2D (Collider2D other) {
        if (other.CompareTag("Player")) {
            isUsable = true;
            //inventory.items.Add(this);
            powerUpSignal.Raise(); // tell the ui to show the powerUp
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    IEnumerator SpeedBoostCo() {
        yield return new WaitForSeconds(16f);
        playerSpeed.RuntimeValue = playerSpeed.initialValue;
    }
}
