using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailScript : MonoBehaviour
{
    public float moveSpeed = 1f;
    Rigidbody2D snailBody;
    Animator anim;
    public LayerMask playerLayer;
    bool isMoveLeft;

    bool isCanMove;
    bool isStunned;
    public Transform left_Collision, right_Collision, top_Collision, down_Collision;
    Vector3 left_Collision_Position, right_Collision_Position;

    private void Awake()
    {
        snailBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        left_Collision_Position = left_Collision.position;
        right_Collision_Position = right_Collision.position;
    }
    void Start()
    {
        isMoveLeft = true;
        isCanMove = true;
    }

    void Update()
    {
        //print("[Update] isCanMove  =" + isCanMove);
        if (isCanMove)
        {
            if(!isStunned)
            {
                if(isMoveLeft)
                {
                    //print("move left");
                    snailBody.velocity = new Vector2(-moveSpeed, snailBody.velocity.y);
                } else
                {
                    snailBody.velocity = new Vector2(moveSpeed, snailBody.velocity.y);
                    //print("move right");
                }

            }
        }
        CheckCollision();
    }

    void CheckCollision()
    {
        RaycastHit2D leftHit = Physics2D.Raycast(left_Collision.position, Vector2.left, 0.1f, playerLayer);
        RaycastHit2D rightHit = Physics2D.Raycast(right_Collision.position, Vector2.right, 0.1f, playerLayer);

        Collider2D topHit = Physics2D.OverlapCircle(top_Collision.position, 0.2f, playerLayer);

        if (topHit != null)
        {
            if(topHit.gameObject.tag == MyTags.PLAYER_TAG)
            {
                if (!isStunned)
                {
                    topHit.gameObject.GetComponent<Rigidbody2D>().velocity = 
                        new Vector2(topHit.gameObject.GetComponent<Rigidbody2D>().velocity.x, 7f);
                    isCanMove = false;
                    snailBody.velocity = new Vector2(0,0);
                    anim.Play("Stunned");
                    isStunned = true;

                    if(tag == MyTags.BEETLE_TAG)
                    {
                        anim.Play("Stunned");
                        StartCoroutine(Dead(0.5f));
                    }
                }
            }
        }

        if(leftHit)
        {
            if(leftHit.collider.gameObject.tag == MyTags.PLAYER_TAG)
            {
                if(!isStunned)
                {
                    //TODO kill player
                    leftHit.collider.gameObject.GetComponent<PlayerDamage>().DealDamage();
                } else
                {
                    if(tag != MyTags.BEETLE_TAG)
                    {
                        snailBody.velocity = new Vector2(15f, snailBody.velocity.y);
                        StartCoroutine(Dead(3f));
                    }
                }
            }
        } else if(rightHit)
        {
            if(rightHit.collider.gameObject.tag == MyTags.PLAYER_TAG)
            {
                if(!isStunned)
                {
                    // TODO kill player
                    rightHit.collider.gameObject.GetComponent<PlayerDamage>().DealDamage();
                } else
                {
                    if (tag != MyTags.BEETLE_TAG)
                    {
                        snailBody.velocity = new Vector2(-15f, snailBody.velocity.y);
                        StartCoroutine(Dead(3f));
                    }
                }
            }
        }
        if (!Physics2D.Raycast(down_Collision.position, Vector2.down, 0.1f))
        {
            ChangeDirection();
        }

    }

    IEnumerator Dead(float timer)
    {
        yield return new WaitForSeconds(timer); ;
        gameObject.SetActive(false);

    }

    void ChangeDirection()
    {
        isMoveLeft = !isMoveLeft;

        Vector3 tempScale = transform.localScale;

        if(isMoveLeft)
        {
            tempScale.x = Mathf.Abs(tempScale.x);
            left_Collision.position = left_Collision_Position;
            right_Collision.position = right_Collision_Position;
        } else
        {
            tempScale.x = -Mathf.Abs(tempScale.x);
            left_Collision.position = right_Collision_Position;
            right_Collision.position = left_Collision_Position;
         }

        transform.localScale = tempScale;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        
        if(collision.gameObject.tag == MyTags.PLAYER_TAG) 
        {
            anim.Play("Stunned");
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == MyTags.BULLET_TAG)
        {
            if(tag == MyTags.BEETLE_TAG)
            {
                anim.Play("Stunned");

                isCanMove = false;
                snailBody.velocity = new Vector2(0, 0);
                StartCoroutine(Dead(0.4f));
            }
            if (tag == MyTags.SNAIL_TAG)
            {
                if(!isStunned)
                {
                    anim.Play("Stunned");
                    isStunned = true;
                    isCanMove = false;
                    snailBody.velocity = new Vector2(0, 0);

                } else
                {
                    gameObject.SetActive(false);
                }

            }
        }    
    }
}
