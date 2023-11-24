using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
     [SerializeField] GameObject gameOverPanel;
     [SerializeField] GameObject UICanvas;
     [SerializeField] List<FloatValue> floatValuesToReset;

    public void DisplayGameOverScreen() {
        UICanvas.SetActive(false);
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RestartGame() {
        Time.timeScale = 1f;
        for (int i = 0; i < floatValuesToReset.Count; i++) {
            floatValuesToReset[i].RuntimeValue = floatValuesToReset[i].initialValue;
        }
        SceneManager.LoadScene("Scene1");
    }

    public void QuitToMain() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartMenu");
    }
}
