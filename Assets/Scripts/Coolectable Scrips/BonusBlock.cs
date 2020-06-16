using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusBlock : MonoBehaviour
{

    public Transform bottom_Collision;
    private Animator anim;
    public LayerMask playerLayer;
    private Vector3 moveDirection = Vector3.up;
    private Vector3 animPosition;
    private Vector3 originPosition;
    private bool isStartAnim;
    private bool canAnim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        originPosition = transform.position;
        animPosition = transform.position;
        animPosition.y += 0.15f;
        canAnim = true;
    }

    void Update()
    {
        CheckForCollision();
        AnimateUpDown();
    }

    void CheckForCollision()
    {
        if(canAnim)
        {
            RaycastHit2D hit = Physics2D.Raycast(bottom_Collision.position, Vector2.down, 0.1f, playerLayer);

            if(hit)
            {
                if(hit.collider.gameObject.tag == MyTags.PLAYER_TAG)
                {
                    anim.Play("BonusBlockEmpty");
                    isStartAnim = true;
                    canAnim = false;
                }
            }
        }
    }

    void AnimateUpDown()
    {
        if(isStartAnim)
        {
            transform.Translate(moveDirection * Time.smoothDeltaTime);
            if(transform.position.y >= animPosition.y)
            {
                moveDirection = Vector3.down;
            } else if (transform.position.y <= originPosition.y)
            {
                isStartAnim = false;
            }
        }

    }
}
