using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://www.youtube.com/watch?v=PkNRPOrtyls&t=703s
public abstract class PowerUpEffect : ScriptableObject {
    // target - the target of the powerup (i.e., the player)
    public abstract void Apply(GameObject target);
}
