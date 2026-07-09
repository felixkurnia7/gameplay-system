using UnityEngine;

public class InventoryModelTest : MonoBehaviour
{
    public ItemDefinition item;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var inventory = new InventoryModel(20);
        inventory.TryAdd(item, 25);
        for (int i = 0; i < inventory.SlotCount; i++)
        {
            Debug.Log($"Slot {i}: {inventory.Slots[i].Quantity}");
        }

        inventory.TryRemove(item, 7);
        for (int i = 0; i < inventory.SlotCount; i++)
        {
            Debug.Log($"Slot {i}: {inventory.Slots[i].Quantity}");
        }

        inventory.SwapSlots(0, 1);
        for (int i = 0; i < inventory.SlotCount; i++)
        {
            Debug.Log($"Slot {i}: {inventory.Slots[i].Quantity}");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
