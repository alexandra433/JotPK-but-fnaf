using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UsableItem : MonoBehaviour
{
    UsePowerUp useAction;
    protected bool isUsable;
    public SignalGame powerUpSignal;
    [SerializeField] protected Inventory inventory;

    private void Awake() {
        useAction = new UsePowerUp();
    }

    private void OnEnable() {
        useAction.Enable();
    }

    private void OnDisable() {
        useAction.Disable();
    }

    private void Start() {
    // bind action to a function
        useAction.UseItem.UseItem.performed += _ => ActivateItem();
    }

    public abstract void ActivateItem();
}
