using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseScript : MonoBehaviour
{
    // Start is called before the first frame update

    public static bool Paused;
    public GameObject menu;
    void Start()
    {
        Paused = false;
        menu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        { 
            if (Paused)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                menu.SetActive(false);
                Time.timeScale = 1f;
                Paused = false;
            }
            else if(!Paused)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                menu.SetActive(true);
                Time.timeScale = 0f;
                Paused = true;
            }
        }

    }
    public void resume()
    {
        Time.timeScale = 1f;
        menu.SetActive(false);
        Paused = false;
    }

    public void quit()
    {
        SceneManager.LoadScene("StartScene");
        resume();
    }
}