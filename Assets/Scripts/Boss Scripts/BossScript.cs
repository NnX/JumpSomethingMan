using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{

    public GameObject bossBone;
    public Transform attackInstatniate;

    private Animator anim;

    private string coroutine_Attack = "StartAttack";


    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        StartCoroutine(coroutine_Attack);
    }

    void BackToIdle()
    {
        anim.Play("BossIdle");
    }

    void ThrowBone ()
    {
        GameObject obj = Instantiate(bossBone, attackInstatniate.position, Quaternion.identity);
        obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-300f, -700f), 0f));
    }

    IEnumerator StartAttack()
    {
        yield return new WaitForSeconds(Random.Range(2f, 5f));

        anim.Play("BossAttack");
        StartCoroutine(coroutine_Attack);

    }

    public void DeactivateBossScript()
    {
        StopCoroutine(coroutine_Attack);
        enabled = false;
    }
}
