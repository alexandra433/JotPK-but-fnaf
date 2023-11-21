using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] string sceneToLoad;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Collider2D>().enabled = false;
    }

    public void OpenSceneTransition() {
        GetComponent<Collider2D>().enabled = true;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
