using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    DieUiShow die;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false); 
        Cursor.lockState = CursorLockMode.Locked;    
        die = FindObjectOfType<DieUiShow>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }

        if(die.dieUi_IsOpen)
        {
            Destroy(pauseMenu);
            if(pauseMenu == null)
            {
                Debug.LogWarning("There is no Pause Menu maybe you have died");
            }
        }
        
    }

    public void ReturnToGame()
    {
        pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
    }
    public void MainMenu()
    {
        //Go to MainMenu but we dont have one still
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quiting the game");
    }
}
