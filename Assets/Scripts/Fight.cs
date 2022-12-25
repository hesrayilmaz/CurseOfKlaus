using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fight : MonoBehaviour
{
    public GameObject holyWater;
    public Animator demonAnimator;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitForThrow());
    }

    IEnumerator WaitForThrow()
    {
        for (int i = 0; i < 9; i++)
        {
            Instantiate(holyWater);
            yield return new WaitForSeconds(0.5f);
        }
        demonAnimator.SetBool("isDead", true);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(3);
    }
}
