using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// actually, this might not be necessary
public class EntranceBlockers : MonoBehaviour
{
    [SerializeField] Collider2D bottomBlocker;

    // Start is called before the first frame update
    void Start() {

    }

    public void DisableBottomBlocker() {
        bottomBlocker.enabled = false;
    }
}
