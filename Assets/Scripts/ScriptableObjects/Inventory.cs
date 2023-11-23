using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Inventory : ScriptableObject, ISerializationCallbackReceiver
{
    public UsableItem item;

    public void OnAfterDeserialize()
    {
        item = null;
    }

    public void OnBeforeSerialize()
    {

    }
}
