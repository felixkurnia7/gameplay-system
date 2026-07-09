public class ItemStack
{
    public ItemDefinition Definition { get; private set; }
    public int Quantity { get; private set; }
    public bool IsEmpty => Definition == null || Quantity <= 0;

    public int GetRemainingStackSpace()
    {
        if (IsEmpty) return 0;
        return Definition.MaxStack - Quantity;
    }
    
    public bool CanStackWith(ItemStack other)
    {
        if (IsEmpty || other.IsEmpty)
            return false;

        return Definition.Id == other.Definition.Id && GetRemainingStackSpace() > 0;
    }

    public void Set(ItemDefinition definition, int quantity)
    {
        if (definition == null || quantity <= 0)
        {
            Clear();
            return;
        }
        Definition = definition;
        Quantity = quantity < definition.MaxStack ? quantity : definition.MaxStack;
    }

    public int AddQuantity(int amount)
    {
        if (IsEmpty || amount <= 0) return 0;

        int remainingSpace = GetRemainingStackSpace();
        int added = amount < remainingSpace ? amount : remainingSpace;
        Quantity += added;
        return added;
    }

    public int RemoveQuantity(int amount)
    {
        if (IsEmpty || amount <= 0) return 0;
        int removed = amount < Quantity ? amount : Quantity;
        Quantity -= removed;
        if (Quantity == 0) Clear();
        return removed;
    }

    public void Clear()
    {
        Definition = null;
        Quantity = 0;
    }
}
