using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    public GameObject pause;
    public bool ispause;
    void Start()
    {

        Time.timeScale=1;
        ispause=false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (ispause==false)
            {
                Time.timeScale=0;
                pause.SetActive(true);
                ispause=true;
            }
            else
            {
                Time.timeScale=1;
                pause.SetActive(false);
                ispause=false;
            }
        }
    }
    public void mainmenu()
    {
        SceneManager.LoadScene(0);
    }
    public void Muted()
    {
        AudioManager.Instance.ToggleMusic();
         AudioManager.Instance.ToggleSFX();
    }
    public void paused()
    {
         if (ispause==false)
            {
                Time.timeScale=0;
                pause.SetActive(true);
                ispause=true;
            }
            else
            {
                Time.timeScale=1;
                pause.SetActive(false);
                ispause=false;
            }
    }
}
