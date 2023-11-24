using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : UsableItem
{
    [SerializeField] FloatValue playerFireRate;
    [SerializeField] float newFireRate;

    public override void ActivateItem() {
        if (isUsable) {
            isUsable = false;
            playerFireRate.RuntimeValue = newFireRate;
            StartCoroutine(SpeedBoostCo());
            powerUpGoneSignal.Raise();
        }
    }

    public override void AutoActivateItem()
    {
        if (isUsable) {
            isUsable = false;
            playerFireRate.RuntimeValue = newFireRate;
            StartCoroutine(SpeedBoostCo());
        }
    }

    IEnumerator SpeedBoostCo() {
        yield return new WaitForSeconds(12f);
        playerFireRate.RuntimeValue = playerFireRate.initialValue;
        Destroy(this.gameObject);
    }
}
