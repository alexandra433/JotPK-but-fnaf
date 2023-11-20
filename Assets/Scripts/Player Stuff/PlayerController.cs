using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Control player movement
public class PlayerController : MonoBehaviour {
    public float moveSpeed = 2f;
    [SerializeField]
    float collisionOffset;
    public ContactFilter2D movementFilter;
    Vector2 movementInput;
    Rigidbody2D rb;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    Animator animator;

    [SerializeField]
    GameObject bulletPrefab;
    Vector2 shootDirection;
    // https://docs.unity3d.com/ScriptReference/Time-time.html
    public float fireRate = 0.3f;
    float nextFire = 0.0f;


    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update() {
        if (shootDirection != Vector2.zero && Time.time > nextFire) {
            nextFire = Time.time + fireRate;
            animator.SetFloat("Shoot X", shootDirection.x);
            animator.SetFloat("Shoot Y", shootDirection.y);
            animator.SetBool("isShooting", true);
            ShootBullet();
        } else if (shootDirection == Vector2.zero) {
            animator.SetBool("isShooting", false);
        }
    }

    private void FixedUpdate() {
        // if movement input is not 0, try to move
        if (movementInput != Vector2.zero) {
            int count = rb.Cast(
                movementInput, // X and Y values btwn -1 and 1 representing the direction from the body to look for collisions
                movementFilter, // the settings that determine where a collision can occur (like layers to collide with)
                castCollisions, // List of collisions to store the found collisions into after the Cast is done
                moveSpeed * Time.fixedDeltaTime + collisionOffset); // amount to cast is equal to the movement plus an offset
            if (count == 0) { // no collisions
                rb.MovePosition(rb.position + movementInput * moveSpeed * Time.fixedDeltaTime);
                animator.SetBool("isMoving", true);
            }
        } else {
            animator.SetBool("isMoving", false);
        }
    }

    void OnMove(InputValue movementValue) {
        movementInput = movementValue.Get<Vector2>();
    }

    void OnFire(InputValue shootDirectionValue) {
        shootDirection = shootDirectionValue.Get<Vector2>();
    }

    void ShootBullet() {
        // Quaternion.identity means no rotation
        GameObject bulletObject = Instantiate(bulletPrefab, rb.position + shootDirection * 0.5f, Quaternion.identity);
        BulletScript bullet = bulletObject.GetComponent<BulletScript>();
        bullet.Shoot(shootDirection);
    }

    // Player dies after touching an Enemy
    public void Die() {
        moveSpeed = 0.0f;
        animator.Play("player_death");
        Destroy(GetComponent<BoxCollider>());
        Destroy(gameObject, 0.4f);
    }
}
