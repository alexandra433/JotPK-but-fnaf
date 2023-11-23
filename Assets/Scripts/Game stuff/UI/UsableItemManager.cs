using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class UsableItemManager : MonoBehaviour
{
    [SerializeField] Sprite defaultSprite;
    UsableItem currentItem;
    [SerializeField] Inventory inventory;

    void Start() {

    }

    public void AddItemToInventoryDisplay() {
        if (inventory.items.Count >= 1) {
            inventory.items[0].ActivateItem();
            inventory.items.RemoveAt(0);
        }
        currentItem = inventory.items[0];
        gameObject.GetComponent<UnityEngine.UI.Image>().sprite = currentItem.GetComponent<SpriteRenderer>().sprite;
    }
}
