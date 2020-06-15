using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpiderScript : MonoBehaviour
{
    Rigidbody2D spiderBody;
    Animator anim;
    Vector3 moveDirection = Vector3.down;

    string corutine_name = "ChangeMovement";

    private void Awake()
    {
        spiderBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        StartCoroutine(corutine_name);        
    }

    void Update()
    {
        MoveSpider();
    }

    void MoveSpider()
    {
        transform.Translate(moveDirection * Time.smoothDeltaTime);
    }

    IEnumerator ChangeMovement()
    {
        yield return new WaitForSeconds(Random.Range(2f, 5f));

        if(moveDirection == Vector3.down)
        {
            moveDirection = Vector3.up;
        } else
        {
            moveDirection = Vector3.down;
        }
        StartCoroutine(corutine_name);    
    }
    IEnumerator SpiderDead()
    {
        yield return new WaitForSeconds(3f);

        gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == MyTags.BULLET_TAG)
        {
            anim.Play("SpiderDead");
            spiderBody.bodyType = RigidbodyType2D.Dynamic;

            StartCoroutine(SpiderDead());
            StopCoroutine(corutine_name);
        }
        if(collision.tag == MyTags.PLAYER_TAG)
        {
            collision.GetComponent<PlayerDamage>().DealDamage();
        }
    }
}
