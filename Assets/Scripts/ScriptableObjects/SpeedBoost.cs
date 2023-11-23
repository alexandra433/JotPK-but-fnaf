using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// might not use
[CreateAssetMenu]
public class SpeedBoost : PowerUpEffect {
    public float amount;
    private float changeTime;

    public override void Apply(GameObject target) {
        PlayerController p = target.GetComponent<PlayerController>();
        if (p) {
            p.moveSpeed *= 2;
        }
    }
}
