using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DieUiShow : MonoBehaviour
{
    [SerializeField] GameObject dieUI;
    Player player;
    PauseMenu pauseMenu;
    public bool dieUi_IsOpen {get; private set;}

    void Start()
    {
        dieUI.SetActive(false);
        player = FindObjectOfType<Player>();
        pauseMenu = FindObjectOfType<PauseMenu>();
    }

    void Update()
    {
        if(player.health_P == 0)
        {
            dieUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            dieUi_IsOpen = true;
        }
    }

    public void RestartCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


}
