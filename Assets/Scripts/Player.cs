using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField] private float maxSpeed;
    [SerializeField] private float jumpVelocity;

    private Rigidbody2D rb;

    private float horizontal;
    private bool shouldJump;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (!shouldJump && Input.GetButtonDown("Jump")) {
            shouldJump = true;
        }
    }

    void FixedUpdate() {
        Vector2 velocity = rb.velocity;
        velocity.x = horizontal * maxSpeed * Time.fixedDeltaTime;

        if (shouldJump) {
            velocity.y = jumpVelocity;
            shouldJump = false;
        }

        rb.velocity = velocity;
    }
}
