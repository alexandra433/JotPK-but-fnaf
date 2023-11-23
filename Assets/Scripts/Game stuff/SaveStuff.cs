using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveStuff : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // save the gameObject that is attached to this script
        DontDestroyOnLoad(gameObject);
    }
}
