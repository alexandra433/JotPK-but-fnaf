using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Scripting;

// https://www.youtube.com/watch?v=9tsbUoFfAgo for pause menu with new input system
public class PauseManager : MonoBehaviour
{
    PauseAction action;
    private bool isPaused;
    [SerializeField] GameObject pausePanel;

    // initialize variables or states before the application starts
    private void Awake() {
        action = new PauseAction();
    }

    private void OnEnable() {
        action.Enable();
    }

    private void OnDisable() {
        action.Disable();
    }

    private void Start() {
        // bind action to a function
        action.Pause.PauseGame.performed += _ => DeterminePause();
    }

    private void DeterminePause() {
        if (isPaused) {
            ResumeGame();
        } else {
            PauseGame();
        }
    }

    public void PauseGame() {
        pausePanel.SetActive(true);
        isPaused = true;
        Time.timeScale = 0f;
    }

    public void ResumeGame() {
        isPaused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void QuitToMain() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartMenu");
    }
}
