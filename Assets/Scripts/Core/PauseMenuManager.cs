using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject pause_menu;

    private void Start()
    {
        pause_menu.SetActive(false);
    }
    private void OnEnable()
    {
        PauseController.onPause += Pause;
        PauseController.onResume += Resume;
    }

    private void OnDisable()
    {
        PauseController.onPause -= Pause;
        PauseController.onResume -= Resume;
    }

    private void Resume()
    {
        pause_menu.SetActive(false);
    }

    private void Pause()
    {
        pause_menu.SetActive(true);
    }

    public void ResumeGame()
    {
        PauseController.Resume();
    }

    public void QuitGame()
    {
        // Call resume before level change to make sure TimeScale and other dependencies are reset outside of pause time
        PauseController.Resume();
        SceneManager.LoadScene("MainMenu");
    }
}
