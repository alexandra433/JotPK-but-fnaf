using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class UsableItem : Collectible
{
    UsePowerUp useAction;
    protected bool isUsable;
    public SignalGame powerUpSignal;
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

    private void Start() {
    // bind action to a function
        useAction.UseItem.UseItem.performed += _ => ActivateItem();
    }

    public abstract void ActivateItem();

    public void RemoveItemWhenPlayerDies() {
        isUsable = false;
    }


    void OnTriggerEnter2D (Collider2D other) {
        if (other.CompareTag("Player")) {
            isUsable = true;
            //usableItemManager.AddItemToInventoryDisplay(this);
            //inventory.items.Add(this);
            //powerUpSignal.Raise(); // tell the ui to show the powerUp
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
