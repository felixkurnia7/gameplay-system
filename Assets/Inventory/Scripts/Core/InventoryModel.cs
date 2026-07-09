using System;

public class InventoryModel
{
    public ItemStack[] Slots { get; }
    public int SlotCount { get; }

    public event Action OnChanged;
    
    public InventoryModel(int slotCount)
    {
        SlotCount = slotCount;
        Slots = new ItemStack[slotCount];

        for (int i = 0; i < slotCount; i++)
            Slots[i] = new ItemStack();
    }

    public bool TryAdd(ItemDefinition definition, int amount)
    {
        if (definition == null || amount <= 0) return false;
        int remaining = amount;
        
        // First, try to stack with existing items
        for (int i = 0; i < SlotCount && remaining > 0; i++)
        {
            if (!Slots[i].IsEmpty && Slots[i].Definition.Id == definition.Id)
            {
                int added = Slots[i].AddQuantity(remaining);
                remaining -= added;
            }
        }

        // Then, try to add to empty slots
        for (int i = 0; i < SlotCount && remaining > 0; i++)
        {
            if (Slots[i].IsEmpty)
            {
                int toAdd = Math.Min(remaining, definition.MaxStack);   
                Slots[i].Set(definition, toAdd);
                remaining -= toAdd;
            }
        }

        // If there's still remaining, return false
        if (remaining > 0) return false;

        // If successful, invoke the OnChanged event
        OnChanged?.Invoke();
        return true;
    }

    public bool TryRemove(ItemDefinition definition, int amount)
    {
        if (definition == null || amount <= 0) return false;
        int remaining = amount;

        // First, try to remove from existing items
        for (int i = 0; i < SlotCount && remaining > 0; i++)
        {
            if (!Slots[i].IsEmpty && Slots[i].Definition.Id == definition.Id)
            {
                int removed = Slots[i].RemoveQuantity(remaining);
                remaining -= removed;
            }
        }

        // If there's still remaining, return false
        if (remaining > 0) return false;

        // If successful, invoke the OnChanged event
        OnChanged?.Invoke();
        return true;
    }

    public void SwapSlots(int slotIndex1, int slotIndex2)
    {
        var slotA = GetSlot(slotIndex1);
        var slotB = GetSlot(slotIndex2);

        if (slotIndex1 == slotIndex2)
            return;

        if (slotA.IsEmpty && slotB.IsEmpty)
            return;

        var defA = slotA.Definition;
        var qtyA = slotA.Quantity;
        var defB = slotB.Definition;
        var qtyB = slotB.Quantity;

        if (defB == null || qtyB <= 0)
            slotA.Clear();
        else
            slotA.Set(defB, qtyB);

        if (defA == null || qtyA <= 0)
            slotB.Clear();
        else
            slotB.Set(defA, qtyA);

        OnChanged?.Invoke();
    }

    public ItemStack GetSlot(int index)
    {
        if (index < 0 || index >= SlotCount)
            throw new ArgumentOutOfRangeException(nameof(index));

        return Slots[index];
    }
}
