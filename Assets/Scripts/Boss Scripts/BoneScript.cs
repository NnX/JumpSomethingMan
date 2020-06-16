using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneScript : MonoBehaviour
{


    void Start()
    {
        Invoke("Deactivate", 4f);
    }

    void Deactivate()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == MyTags.PLAYER_TAG)
        {
            gameObject.SetActive(false);
            collision.GetComponent<PlayerDamage>().DealDamage();
        }
    }
}
