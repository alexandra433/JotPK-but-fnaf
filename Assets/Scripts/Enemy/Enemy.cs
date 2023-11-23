/*
Description
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour {
    public int currentHealth {get {return health;}}
    [SerializeField] protected int health;
    [SerializeField] protected float speed;
    protected Animator animator;
    protected Rigidbody2D rb;
    protected Transform player;
    // not sure how to get player speed from player script
    [SerializeField] FloatValue basePlayerSpeed;
    protected float playerSpeed;
    protected Vector2 movement; // The direction that the enemy should move in

    [SerializeField] protected LootTable thisLoot;

    protected virtual void Awake() {
        // virtual methods can be overridden in subclasses
        // Awake() instead of Start() because enemies will be spawned in
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player").transform;
        playerSpeed = basePlayerSpeed.initialValue;
    }

    // Uses the player's current position to calculate the direction the enemy
    // should move in to reach that position.
    protected virtual void Update() {
        // transform.position is the position of the enemy
        if (player != null) {
            Vector2 direction = player.position - transform.position;
            direction.Normalize();
            movement = direction;
        } else {
            // Stop moving
            movement = Vector2.zero;
        }
    }

    protected virtual void FixedUpdate() {
        Move();
    }

    protected virtual void Move() {
        // Randomly choose to move in x direction or y direction
        int randomNum = Random.Range(0, 2);
        Vector2 position = transform.position;
        if (randomNum == 0) {
            // move along x-axis
            position.x += movement.x * speed * Time.deltaTime;
        } else {
            // move along y-axis
            position.y += movement.y * speed * Time.deltaTime;
        }
        rb.MovePosition(position);
    }

    // Decreases the health of the enemy by the given amount.
    public void DecreaseHealth(int healthLost) {
        health -= healthLost;
        if (health <= 0) {
            Die();
        }
    }

    // Removes the enemy from the game and plays its death animation.
    public void Die() {
        // Remove the box collider so that the player cannot collide with an enemy
        // that is already dead.
        Destroy(GetComponent<BoxCollider>());
        animator.SetTrigger("Die");
        MakeLoot();
        // Wait 0.4 seconds for the death animation to finish playing.
        Destroy(gameObject, 0.4f);
    }

    private void MakeLoot() {
        if (thisLoot != null)
        {
            Collectible current = thisLoot.LootCollectible();
            if (current != null) {
                Instantiate(current.gameObject, transform.position, Quaternion.identity);
            }
        }
    }

    // Player dies after colliding with an enemy.
    protected virtual void OnCollisionEnter2D(Collision2D other) {
        // Debug.Log("Enemy Collision with " + other.gameObject);
        PlayerController p = other.gameObject.GetComponent<PlayerController>();
            if (p != null) {
                p.Die();
            }
    }

    // Enemies might drop an item when they die
    protected void DropItem() {

    }
}
