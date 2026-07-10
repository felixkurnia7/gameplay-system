using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class InventoryView : MonoBehaviour
{
    private InventoryModel inventory;
    [SerializeField] private List<InventorySlotUI> slots;
    [SerializeField] private Transform slotsParent;
    [SerializeField] private int slotCount;

    [SerializeField] private RectTransform ghostRoot;
    [SerializeField] private Image ghostIcon;

    public ItemDefinition item;

    private void Start()
    {
        ghostRoot.gameObject.SetActive(false);

        if (slotsParent == null)
        {
            Debug.LogError("Slots parent is not set", this);
            return;
        }

        slots = new List<InventorySlotUI>(slotsParent.GetComponentsInChildren<InventorySlotUI>());
        slotCount = slots.Count;
        inventory = new InventoryModel(slotCount);

        for (int i = 0; i < slotCount; i++)
            slots[i].Initialize(i, inventory, ghostIcon, ghostRoot);

        inventory.OnChanged += Refresh;

        Refresh();
    }

    private void OnDestroy()
    {
        if (inventory != null)
        {
            inventory.OnChanged -= Refresh;
        }
    }

    private void Refresh()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].Set(inventory.GetSlot(i));
        }
    }

    public void OnAdd1() => inventory.TryAdd(item, 1);

    public void OnAdd5() => inventory.TryAdd(item, 5);

    public void OnRemove1() => inventory.TryRemove(item, 1);
}
