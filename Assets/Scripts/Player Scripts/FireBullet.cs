using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{

    float speed = 10f;
    Animator anim;
    bool isCanMove;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }
    void Start()
    {
        isCanMove = true;
        StartCoroutine(DisableBullet(5f));
    }

    void Update()
    {
        Move();
    }
    void Move()
    {
        if(isCanMove)
        {
            Vector3 temp = transform.position;
            temp.x += speed * Time.deltaTime;
            transform.position = temp;

        }
    }

    public float Speed {
        get {
            return speed;
        }
        set {
            speed = value;
        }
    }

    IEnumerator DisableBullet (float timer)
    {
        yield return new WaitForSeconds(timer);
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        print("[FireBullet] OnTriggerEnter2D AAAAAAAAAAA = " + collision.gameObject.tag);
        if(collision.gameObject.tag == MyTags.BEETLE_TAG || collision.gameObject.tag == MyTags.SNAIL_TAG)
        {
            print("[FireBullet]  OnCollisionEnter2D");
            anim.Play("Explode");
            isCanMove = false;
            StartCoroutine(DisableBullet(0.2f));
            //collision.gameObject.SetActive(false);
        }
    }
}
