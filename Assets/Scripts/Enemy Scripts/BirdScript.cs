using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdScript : MonoBehaviour
{
    Rigidbody2D birdBody;
    Animator anim;
    Vector3 moveDirection = Vector3.left;
    Vector3 originPosition;
    Vector3 movePosition;

    public GameObject birdStone;
    public LayerMask playerLayer;
    bool isAttacked = false;
    bool isCanMove;
    float speed = 3f;

    private void Awake()
    {
        birdBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }
    void Start()
    {
        originPosition = transform.position;
        originPosition.x += 6f;

        movePosition = transform.position;
        movePosition.x -= 6;
        isCanMove = true;
    }

    void Update()
    {
        MoveTheBird();
        DropTheEgg();
    }

    void MoveTheBird()
    {
        if(isCanMove)
        {
            transform.Translate(moveDirection * speed * Time.smoothDeltaTime);
            if(transform.position.x >= originPosition.x)
            {
                moveDirection = Vector3.left;
                ChangeDirection(0.5f);
            } else if (transform.position.x <= movePosition.x)
            {
                moveDirection = Vector3.right;
                ChangeDirection(-0.5f);
            }
        }
    }

    void ChangeDirection(float direction)
    {
        Vector3 tempScale = transform.localScale;
        tempScale.x = direction;
        transform.localScale = tempScale;
    }

    void DropTheEgg()
    {
        if(!isAttacked)
        {
            if(Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity, playerLayer))
            {
                Instantiate(birdStone, new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z), Quaternion.identity);
                isAttacked = true;
                anim.Play("BirdFly");
            }
        }
    }

    IEnumerator BirdDead()
    {
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == MyTags.BULLET_TAG)
        {
            anim.Play("BirdDead");
            GetComponent<BoxCollider2D>().isTrigger = true;
            birdBody.bodyType = RigidbodyType2D.Dynamic;
            isCanMove = false;
            StartCoroutine(BirdDead());
        }
    }
}
