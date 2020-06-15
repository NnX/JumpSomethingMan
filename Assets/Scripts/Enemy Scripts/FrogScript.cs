using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogScript : MonoBehaviour
{

    Animator anim;
    Rigidbody2D frogBody;

    bool is_animation_started;
    bool is_animation_finished;
    int jumperTimes;
    bool isJumpLeft = true;
    string coroutine_name = "FrogJump";
    public LayerMask playerLayer;

    private GameObject player;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        frogBody = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        StartCoroutine(coroutine_name);
        player = GameObject.FindGameObjectWithTag(MyTags.PLAYER_TAG);
    }

    private void Update()
    {
        if(Physics2D.OverlapCircle(transform.position, 0.5f, playerLayer))
        {
            player.GetComponent<PlayerDamage>().DealDamage();
        }
    }

    void LateUpdate()
    {
        if(is_animation_finished && is_animation_started)
        {
            is_animation_started = false;
            transform.parent.position = transform.position;
            transform.localPosition = Vector3.zero;
        }
    }

    IEnumerator FrogJump()
    {
        yield return new WaitForSeconds(Random.Range(1f, 4f));

        is_animation_started = true;
        is_animation_finished = false;

        jumperTimes++;
        if(isJumpLeft)
        {
            anim.Play("FrogJumpLeft");
        } else
        {
            anim.Play("FrogJumpRight");
        }

        StartCoroutine(coroutine_name);
    }

    void AnimationFinished()
    {
        is_animation_finished = true;

        if(isJumpLeft)
        {
            anim.Play("FrogIdleLeft");
        } else
        {
            anim.Play("FrogIdleRight");
        }

        if(jumperTimes == 3)
        {
            jumperTimes = 0;
            Vector3 tempScale = transform.localScale;
            tempScale.x = -1f;
            transform.localScale = tempScale;
            isJumpLeft = !isJumpLeft;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == MyTags.BULLET_TAG)
        {
            StopCoroutine(coroutine_name);
            frogBody.bodyType = RigidbodyType2D.Dynamic;
            GetComponent<BoxCollider2D>().isTrigger = true;

            gameObject.SetActive(false);

        }
    }
}
