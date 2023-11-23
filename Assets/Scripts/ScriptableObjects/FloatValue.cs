using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
// not reset when scene is changed
public class FloatValue : ScriptableObject, ISerializationCallbackReceiver {
    public float initialValue;
    public float RuntimeValue;

    public void OnAfterDeserialize(){
        RuntimeValue = initialValue;
    }

    public void OnBeforeSerialize(){}
}
