using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffSpringtapController : Enemy {
    protected override void Awake() {
        base.Awake();
        health = 2;
        speed = 0.55f * playerSpeed;
    }

    protected override void OnCollisionEnter2D(Collision2D other) {
        base.OnCollisionEnter2D(other);
        // destroy spikeballs on contact
    }
}
