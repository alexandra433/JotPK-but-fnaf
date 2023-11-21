using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LifeManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI numLives;
    [SerializeField] FloatValue playerLives;

    public void UpdateLivesCount() {
        numLives.text = "x " + playerLives.RuntimeValue;
    }
}
