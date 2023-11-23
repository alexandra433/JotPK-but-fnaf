using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class UsableItemManager : MonoBehaviour
{
    [SerializeField] Sprite defaultSprite;
    UsableItem currentItem;
    [SerializeField] Inventory inventory;

    void Start() {
        currentItem = inventory.item;
        UpdateDisplay();
    }

    public void AddItemToInventoryDisplay(UsableItem newItem) {
        if (currentItem) {
            currentItem.ActivateItem();
        }
        inventory.item = newItem;
        currentItem = newItem;
        UpdateDisplay();
    }

    public void RemoveItemFromInventoryDisplay() {
        currentItem = null;
        inventory.item = null;
        UpdateDisplay();
    }

    private void UpdateDisplay() {
        if (currentItem) {
            GetComponent<UnityEngine.UI.Image>().sprite =
                currentItem.itemSprite;
        } else {
            gameObject.GetComponent<UnityEngine.UI.Image>().sprite = defaultSprite;
        }
    }
}
