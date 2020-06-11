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
                    print("Damage left");
                } else
                {
                    snailBody.velocity = new Vector2(15f, snailBody.velocity.y);
                }
            }
        } else if(rightHit)
        {
            if(rightHit.collider.gameObject.tag == MyTags.PLAYER_TAG)
            {
                if(!isStunned)
                {
                    // TODO kill player
                    print("Damage right");
                } else
                {
                    snailBody.velocity = new Vector2(-15f, snailBody.velocity.y);
                }
            }
        }
        if (!Physics2D.Raycast(down_Collision.position, Vector2.down, 0.1f))
        {
            ChangeDirection();
        }

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
        print("OnCollisionEnter2D");
        if(collision.gameObject.tag == MyTags.PLAYER_TAG) 
        {
            anim.Play("Stunned");
        }
    }
}
