using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaWheel : UsableItem
{
    // [SerializeField] PlayerShootingController playerShootingController;
    int gunTypeNum = 3;
    [SerializeField] FloatValue gunType;

    public override void ActivateItem()
    {
        if (isUsable)
        {
            isUsable = false;
            gunType.RuntimeValue = gunTypeNum;
            StartCoroutine(ActivateItemCo());
            powerUpGoneSignal.Raise();
        }
    }

    public override void AutoActivateItem()
    {
        if (isUsable)
        {
            isUsable = false;
            gunType.RuntimeValue = gunTypeNum;
            StartCoroutine(ActivateItemCo());
        }
    }

    IEnumerator ActivateItemCo()
    {
        yield return new WaitForSeconds(12f);
        gunType.RuntimeValue = gunType.initialValue; // change later
        Destroy(this.gameObject);
    }

}
