using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Control player movement
public class PlayerController : MonoBehaviour {
    [SerializeField] FloatValue baseMoveSpeed;
    //public float moveSpeed;
    [SerializeField] float collisionOffset;
    public ContactFilter2D movementFilter;
    Vector2 movementInput;
    Rigidbody2D rb;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    Animator animator;

    [SerializeField] PlayerShootingController playerShootingController;

    // [SerializeField]
    // GameObject bulletPrefab;
    // Vector2 shootDirection;
    // // https://docs.unity3d.com/ScriptReference/Time-time.html
    // //public float fireRate = 0.3f;
    // [SerializeField] FloatValue fireRate;
    // float nextFire = 0.0f;
    // // 1 - default, 3 - cone thing, 2 - wagon wheel
    // int gunType = 1;

    [SerializeField] VectorValue startingPosition;

    [SerializeField] SignalGame playerDeathSignal;
    [SerializeField] SignalGame gameOverSignal;
    [SerializeField] FloatValue playerLives;

    // Start is called before the first frame update
    void Start() {
        playerShootingController = GetComponent<PlayerShootingController>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        // playerDeathSignal.Raise(); // make sure ui displays correct # of lives
        transform.position = startingPosition.initialValue;
        //moveSpeed = baseMoveSpeed.RuntimeValue;
    }

    // void Update() {
    //     if (shootDirection != Vector2.zero && Time.time > nextFire) {
    //         nextFire = Time.time + fireRate.RuntimeValue;
    //         animator.SetFloat("Shoot X", shootDirection.x);
    //         animator.SetFloat("Shoot Y", shootDirection.y);
    //         animator.SetBool("isShooting", true);
    //         //ShootBullet();
    //         DecideGun();
    //     } else if (shootDirection == Vector2.zero) {
    //         animator.SetBool("isShooting", false);
    //     }
    // }

    // private void DecideGun() {
    //     switch(gunType)
    //     {
    //         case 1: // default gun
    //             ShootBullet();
    //             break;
    //         case 2: // wagon wheel takes predence over cone-shape
    //             break;
    //         case 3: // cone shape
    //             break;
    //     }
    // }

    private void FixedUpdate() {
        // if movement input is not 0, try to move
        if (movementInput != Vector2.zero) {
            int count = rb.Cast(
                movementInput, // X and Y values btwn -1 and 1 representing the direction from the body to look for collisions
                movementFilter, // the settings that determine where a collision can occur (like layers to collide with)
                castCollisions, // List of collisions to store the found collisions into after the Cast is done
                baseMoveSpeed.RuntimeValue * Time.fixedDeltaTime + collisionOffset); // amount to cast is equal to the movement plus an offset
            if (count == 0) { // no collisions
                rb.MovePosition(rb.position + movementInput * baseMoveSpeed.RuntimeValue * Time.fixedDeltaTime);
                animator.SetBool("isMoving", true);
            }
        } else {
            animator.SetBool("isMoving", false);
        }
    }

    void OnMove(InputValue movementValue) {
        movementInput = movementValue.Get<Vector2>();
    }

    // void OnFire(InputValue shootDirectionValue) {
    //     shootDirection = shootDirectionValue.Get<Vector2>();
    // }

    // void ShootBullet() {
    //     // Quaternion.identity means no rotation
    //     GameObject bulletObject = Instantiate(bulletPrefab, rb.position + shootDirection * 0.5f, Quaternion.identity);
    //     BulletScript bullet = bulletObject.GetComponent<BulletScript>();
    //     bullet.Shoot(shootDirection);
    // }

    // Player dies after touching an Enemy
    public void Die() {
        playerLives.RuntimeValue -= 1;
        playerShootingController.ResetFireRate();
        if (playerLives.RuntimeValue >= 0) {
            playerDeathSignal.Raise();
            StartCoroutine(PlayerDeathCo());
        } else {
            gameOverSignal.Raise();
        }
        // Destroy(GetComponent<BoxCollider>());
        // Destroy(gameObject, 0.4f);
    }

    IEnumerator PlayerDeathCo() {
        baseMoveSpeed.RuntimeValue = 0.0f;
        GetComponent<Collider2D>().enabled = false;
        animator.Play("player_death");
        // wait more than 0.4 seconds
        yield return new WaitForSeconds(1f);
        this.gameObject.SetActive(false);
        transform.position = startingPosition.initialValue;
        GetComponent<Collider2D>().enabled = true;
        baseMoveSpeed.RuntimeValue = baseMoveSpeed.initialValue;
        this.gameObject.SetActive(true);
    }
}
