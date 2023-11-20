using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringtrapController : Enemy {

    protected override void Awake() {
        base.Awake();
        health = 1;
        speed = 0.85f * playerSpeed;
    }
}
