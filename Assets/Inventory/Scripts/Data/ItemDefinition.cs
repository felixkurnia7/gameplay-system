using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item Definition")]
public class ItemDefinition : ScriptableObject
{
    [field: SerializeField] public string Id { get; private set; }
    [field: SerializeField] public string DisplayName { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }
    [field: SerializeField] public ItemType Type { get; private set; }
    [field: SerializeField] public int MaxStack { get; private set; }
    [field: SerializeField] public string Description { get; private set; }

    public bool IsStackable => MaxStack > 1;
}