using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soda : UsableItem {
    [SerializeField] float speedMultiplier;
    [SerializeField] FloatValue playerSpeed;

    public override void ActivateItem() {
        if (isUsable) {
            isUsable = false;
            //playerSpeed.RuntimeValue *= speedMultiplier;
            playerSpeed.RuntimeValue = playerSpeed.initialValue * 2;
            StartCoroutine(SpeedBoostCo());
            powerUpGoneSignal.Raise();
        }
    }

    public override void AutoActivateItem()
    {
        if (isUsable) {
            isUsable = false;
            //playerSpeed.RuntimeValue *= speedMultiplier;
            playerSpeed.RuntimeValue = playerSpeed.initialValue * 2;
            StartCoroutine(SpeedBoostCo());
        }
    }

    IEnumerator SpeedBoostCo() {
        yield return new WaitForSeconds(16f);
        // if (playerSpeed.RuntimeValue - playerSpeed.initialValue > playerSpeed.initialValue) {
        //     playerSpeed.RuntimeValue -= playerSpeed.initialValue;
        // } else {
        //     playerSpeed.RuntimeValue = playerSpeed.initialValue;
        // }
        playerSpeed.RuntimeValue = playerSpeed.initialValue;
        Destroy(this.gameObject);
    }
}
