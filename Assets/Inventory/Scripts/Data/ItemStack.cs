using UnityEngine;

public class ItemStack
{
    public ItemDefinition Definition { get; private set; }
    public int Quantity { get; private set; }
    public bool IsEmpty => Definition == null || Quantity <= 0;
    
}
