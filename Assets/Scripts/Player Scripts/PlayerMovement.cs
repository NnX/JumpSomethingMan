using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    Rigidbody2D playerBody;
    Animator anim;

    public Transform groundCheckPosition;
    public LayerMask groundLayer;

    bool isGrounded;
    bool isJumped;
    float jumpPower = 17f;

    private void Awake()
    {
        playerBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();    
    }

    void Start()
    {
        
    }

    void Update()
    {
        CheckIfGrounded();
        PlayedJump();

    }

    void FixedUpdate()
    {
        PlayerWalk();
    }

    void PlayerWalk()
    {
        float x = Input.GetAxisRaw("Horizontal");
        if(x > 0)
        {
            playerBody.velocity = new Vector2(speed, playerBody.velocity.y);
            changeDirection(1);
        } else if (x < 0)
        {
            playerBody.velocity = new Vector2(-speed, playerBody.velocity.y);
            changeDirection(-1);
        } else
        {
            playerBody.velocity = new Vector2(0f, playerBody.velocity.y);
        }

        anim.SetInteger("Speed", Mathf.Abs((int) playerBody.velocity.x));
    }

    void changeDirection(int direction)
    {
        Vector3 tempscale = transform.localScale;
        tempscale.x = direction;
        transform.localScale = tempscale;
    }

    void CheckIfGrounded()
    {
        isGrounded = Physics2D.Raycast(groundCheckPosition.position, Vector2.down, 0.1f, groundLayer);
        if(isGrounded)
        {
            if(isJumped)
            {
                isJumped = false;
                anim.SetBool("Jump", false);
            }
        }
    }

    void PlayedJump()
    {
        if(isGrounded)
        {
            if(Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow))
            {
                isJumped = true;
                playerBody.velocity = new Vector2(playerBody.velocity.x, jumpPower);
                anim.SetBool("Jump", true);
            }
        }
    }
}
