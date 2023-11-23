using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soda : UsableItem {
    [SerializeField] float speedMultiplier;
    [SerializeField] FloatValue playerSpeed;

    public override void ActivateItem() {
        if (isUsable) {
            isUsable = false;
            playerSpeed.RuntimeValue *= speedMultiplier;
            StartCoroutine(SpeedBoostCo());
            //usableItemManager.RemoveItemFromInventoryDisplay();
        }
    }

    IEnumerator SpeedBoostCo() {
        yield return new WaitForSeconds(16f);
        playerSpeed.RuntimeValue = playerSpeed.initialValue;
        Destroy(this.gameObject);
    }
}
