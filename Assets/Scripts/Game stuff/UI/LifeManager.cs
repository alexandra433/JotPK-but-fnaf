using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.PlayerLoop;

public class LifeManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI numLives;
    [SerializeField] FloatValue playerLives;

    void Start()
    {
        UpdateLivesCount();
    }

    public void UpdateLivesCount()
    {
        numLives.text = "x " + playerLives.RuntimeValue;
    }
}
