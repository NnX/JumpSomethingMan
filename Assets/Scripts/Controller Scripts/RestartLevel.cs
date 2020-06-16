using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartLevel : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == MyTags.PLAYER_TAG)
        {
            SceneManager.LoadScene("GamePlay");
        }
    }
}
