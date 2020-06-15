using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerDamage : MonoBehaviour
{
    private Text lifeText;
    private int lifeScoreCount;
    private bool isCanDamage;

    private void Awake()
    {
        lifeText = GameObject.Find("LifeText").GetComponent<Text>();
        lifeScoreCount = 1;
        lifeText.text = "x" + lifeScoreCount;
        isCanDamage = true;
    }

    public void DealDamage()
    {
        print("Damage");
        if(isCanDamage)
        {
            lifeScoreCount--;
            if(lifeScoreCount >= 0)
            {
                lifeText.text = "x" + lifeScoreCount;
            }

            if (lifeScoreCount < 0)
            {
                Time.timeScale = 0f;
                StartCoroutine(RestartGame());
            }

            isCanDamage = false;
            StartCoroutine(WaitForDamage());
        }
    }

   IEnumerator WaitForDamage()
    {
        yield return new WaitForSeconds(2f);
        isCanDamage = true;
    }

    IEnumerator RestartGame()
    {
        yield return new WaitForSecondsRealtime(2f);
        SceneManager.LoadScene("GamePlay");
        Time.timeScale = 1f;
    }

}
