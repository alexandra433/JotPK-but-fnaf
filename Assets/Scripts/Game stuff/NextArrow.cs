using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextArrow : MonoBehaviour
{
    void Start()
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }

    public void RevealArrow() {
        GetComponent<SpriteRenderer>().enabled = true;
    }
}