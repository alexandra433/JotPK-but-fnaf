using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
// not reset when scene is changed
public class FloatValue : ScriptableObject {
    public float initialValue;
    public float RuntimeValue;
}
