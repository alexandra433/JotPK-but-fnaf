using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneChanger : MonoBehaviour
{
    public float changeTime;
    [SerializeField] string sceneToLoad;

    // Update is called once per frame
    void Update()
    {
        changeTime -= Time.deltaTime;
        if (changeTime <= 0) {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
