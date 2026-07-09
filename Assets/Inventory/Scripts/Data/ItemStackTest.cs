using UnityEngine;

public class ItemStackTest : MonoBehaviour
{
    public ItemDefinition item;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var stack = new ItemStack();
        Debug.Log(stack.IsEmpty); // true

        stack.Set(item, item.MaxStack);
        Debug.Log(stack.Quantity); // 5

        // Test AddQuantity
        int added = stack.AddQuantity(3);
        Debug.Log($"Added: {added}, Total: {stack.Quantity}"); // 3, 8
        // Test stack penuh (assign item MaxStack = 10)
        stack.Set(item, 10);
        added = stack.AddQuantity(5);
        Debug.Log($"Added to full: {added}"); // 0
        // Test Clear
        stack.Clear();
        Debug.Log(stack.IsEmpty); // true
    }
}
