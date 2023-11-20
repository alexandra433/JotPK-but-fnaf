using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeballController : Enemy {
    public bool isSpike = false;

    // amount of time spikeball has to move before becoming a spike
    float moveTime = 5f;
    float startTime = 0.0f;
    Vector3 spikePosition; // the position that the spike wants to get to

    protected override void Awake() {
        base.Awake();
        health = 2;
        speed = playerSpeed;

        // pick a location for spike
        spikePosition.x = Random.Range(-6f, 6f);
        spikePosition.y = Random.Range(-6f, 6f);
        // Debug.Log(spikePosition);
        animator.SetBool("IsSpike", false);
    }

    protected override void Update()
    {
        // after a Spikeball turns into a spike, it stops moving
        if (!isSpike && startTime < moveTime) {
            Vector2 direction = spikePosition - transform.position;
            direction.Normalize();
            movement = direction;
        } else if (!isSpike) {
            BecomeSpike();
            isSpike = true;
        }
        startTime += Time.deltaTime;
    }

    protected override void FixedUpdate() {
        if (!isSpike) {
            Move();
        }
    }

    // Transform into a spike
    void BecomeSpike() {
        // PROBLEM
        if (!isSpike) {
            health += 5;
            speed = 0;
            animator.SetTrigger("Spike");
            animator.SetBool("IsSpike", true);
        }
    }
}
