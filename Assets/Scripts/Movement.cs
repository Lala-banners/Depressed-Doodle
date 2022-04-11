using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lara.Movement
{
    public class Movement : MonoBehaviour
    {
        [Header("Player Stuff")]
        public float moveSpeed = 5;
        public float jumpForce;
        private bool facingRight, isJumping;
        private float moveDir;
        private Rigidbody2D rb;

        [Header("Ground Stuff")]
        public LayerMask groundObj;
        public Transform groundCheck;
        private bool isGrounded;
        public float checkRadius;

        [Header("Jumping")]
        public int maxJumpCount;
        private int jumpCount;

        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            jumpCount = maxJumpCount;
        }

        void Update()
        {
            ProcessInput();

            AnimateMovement();
        }

        private void FixedUpdate()
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundObj);

            if (isGrounded)
            {
                jumpCount = maxJumpCount;
            }
            
            Move();
        }

        private void Flip()
        {
            facingRight = !facingRight;
            transform.Rotate(0f, 180f, 0f);
        }

        private void ProcessInput()
        {
            moveDir = Input.GetAxis("Horizontal");
            
            if (Input.GetKeyDown(KeyCode.Space) && jumpCount > 0)
            {
                isJumping = true;
            }
        }

        private void AnimateMovement()
        {
            if (moveDir > 0 && !facingRight)
                Flip();
            else if (moveDir < 0 && facingRight)
                Flip();
        }

        private void Move()
        {
            rb.velocity = new Vector2(moveDir * moveSpeed, rb.velocity.y);
            
            if (isJumping)
            {
                Vector2 jump = new Vector2(0f, 2f);
                rb.AddForce(jump * jumpForce, ForceMode2D.Impulse);
                jumpCount--;
            }

            isJumping = false;
        }
    }
}