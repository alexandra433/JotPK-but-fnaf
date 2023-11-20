using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeballController : Enemy {
    public bool isSpike = false;

    // amount of time spikeball has to move before becoming a spike
    float moveTime = 5f;
    float startTime = 0.0f;
    Vector3 spikePosition;

    protected override void Awake() {
        base.Awake();
        health = 2;
        speed = 0.95f * playerSpeed;

        // pick a location for spike
        spikePosition.x = Random.Range(-6f, 6f);
        spikePosition.y = Random.Range(-6f, 6f);
        Debug.Log(spikePosition);
        animator.SetBool("IsSpike", false);
    }

    protected override void FixedUpdate() {
        // after a Spikeball turns into a spike, it stops moving
        if (!isSpike && startTime + Time.fixedDeltaTime < moveTime) {
            Vector2 direction = spikePosition - transform.position;
            direction.Normalize();
            movement = direction;
            Move();
            startTime += Time.fixedDeltaTime;
        } else if (!isSpike) {
            BecomeSpike();
        }
    }

    // Transform into a spike
    void BecomeSpike() {
        // PROBLEM
        if (!isSpike) {
            isSpike = true;
            health = health + 5;
            Debug.Log(health);
            animator.SetTrigger("Spike");
            animator.SetBool("IsSpike", true);
        }
    }

}
