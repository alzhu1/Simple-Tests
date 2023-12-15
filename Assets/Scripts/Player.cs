using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField] private float maxSpeed;
    [SerializeField] private float jumpVelocity;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayers;

    private Rigidbody2D rb;

    private float horizontal;
    private bool shouldJump;
    private bool grounded;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (grounded && !shouldJump && Input.GetButtonDown("Jump")) {
            shouldJump = true;
        }
    }

    void FixedUpdate() {
        Vector2 velocity = rb.velocity;
        velocity.x = horizontal * maxSpeed * Time.fixedDeltaTime;

        grounded = Physics2D.OverlapCircle(groundCheck.position, 0.05f, groundLayers);

        if (shouldJump) {
            velocity.y = jumpVelocity;
            shouldJump = false;
        }

        rb.velocity = velocity;

        // I hate this
        EventBus.instance.TriggerOnMove(rb.velocity.magnitude >= 0.1f);
    }
}
