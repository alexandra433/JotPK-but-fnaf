using UnityEngine;

public class UsableItemManager : MonoBehaviour
{
    [SerializeField] Sprite defaultSprite;
    UsableItem currentItem;
    [SerializeField] Inventory inventory;

    void Start() {
        Debug.Log("currentItem " + currentItem);
        currentItem = inventory.item;
        UpdateDisplay();
    }

    public void AddItemToInventoryDisplay() {
        // if (currentItem) {
        //     currentItem.ActivateItem();
        // }
        currentItem = inventory.item;
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
