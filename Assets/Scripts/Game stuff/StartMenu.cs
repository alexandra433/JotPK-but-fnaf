using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    string sceneToLoad = "Scene1";

    public void NewGame() {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void QuitToDesktop() {
        Application.Quit();
    }
}
