using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    bool isPause = false;
    public GameObject menu;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPause = !isPause;
            if (isPause)
                Time.timeScale = 0;
            else
                Time.timeScale = 1;
        }
    }

    

    void OnGUI()
    {
        if (isPause)
        {
            menu.SetActive(true);
        }
        else
        {
            menu.SetActive(false);
        }

    }

	public void resume(){
		isPause = false;
        Time.timeScale = 1;
	}

	public void irMenu(){
		SceneManager.LoadScene(1);
	}

    public void restart()
    {
        SceneManager.LoadScene(4);
    }

}