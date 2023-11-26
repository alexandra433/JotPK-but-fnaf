using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    [SerializeField] List<FloatValue> floatValuesToReset;
    string sceneToLoad = "Scene1";

    public void NewGame() {
        SceneManager.LoadScene(sceneToLoad);
        for (int i = 0; i < floatValuesToReset.Count; i++) {
            floatValuesToReset[i].RuntimeValue = floatValuesToReset[i].initialValue;
        }
    }

    public void QuitToDesktop() {
        Application.Quit();
    }
}
