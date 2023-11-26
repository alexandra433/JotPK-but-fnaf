using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class UsableItem : Collectible
{
    UsePowerUp useAction;
    protected bool isUsable;
    public SignalGame powerUpAcquiredSignal;
    public SignalGame powerUpGoneSignal;
    public Sprite itemSprite;
    [SerializeField] protected Inventory inventory;
    // [SerializeField] Image affectedImage;
    // protected UsableItemManager usableItemManager;

    private void Awake() {
        useAction = new UsePowerUp();
    }

    private void OnEnable() {
        useAction.Enable();
    }

    private void OnDisable() {
        useAction.Disable();
    }

    protected override void Start() {
        base.Start();
    // bind action to a function
        useAction.UseItem.UseItem.performed += _ => ActivateItem();
    }

    public abstract void ActivateItem();

    // for activating items automatically when inventory is full
    public abstract void AutoActivateItem();

    public void RemoveItemWhenPlayerDies() {
        isUsable = false;
        Destroy(this.gameObject);
    }


    void OnTriggerEnter2D (Collider2D other) {
        if (other.CompareTag("Player")) {
            pickedUp = true;
            isUsable = true;
            if (inventory.item != null) {
                // if inventory is full, use the new item
                AutoActivateItem();
            } else {
                inventory.item = this;
                powerUpAcquiredSignal.Raise(); // tell the ui to show the powerUp
            }
            GetComponent<SaveStuff>().enabled = true; // enable dontdestroyonload script
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            this.gameObject.GetComponent<Collider2D>().enabled = false;
        }
    }
}
