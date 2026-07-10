using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlotUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    [Header("References")]
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI quantityText;

    private Image ghostIcon;
    private RectTransform ghostRoot;

    private InventoryModel model;
    private int slotIndex;
    private static int? draggedSlotIndex;

    public int SlotIndex => slotIndex;
    public bool IsInitialized => model != null;

    public void Initialize(int index, InventoryModel model, Image ghostIcon, RectTransform ghostRoot)
    {
        slotIndex = index;
        this.model = model;
        this.ghostIcon = ghostIcon;
        this.ghostRoot = ghostRoot;
    }

    public void Set(ItemStack stack)
    {
        if (stack.IsEmpty)
        {
            icon.enabled = false;
            quantityText.text = "";
        }
        else
        {
            icon.enabled = true;
            icon.sprite = stack.Definition.Icon;
            quantityText.text = stack.Quantity > 1 ? stack.Quantity.ToString() : "";
        }
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (model.GetSlot(slotIndex).IsEmpty) return;
        draggedSlotIndex = slotIndex;
        eventData.pointerDrag = gameObject;
        ShowGhost();
    }

    public void OnDrag(PointerEventData eventData)
    {
        UpdateGhostPosition(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        HideGhost();
        
        draggedSlotIndex = null;
    }

    public void OnDrop(PointerEventData eventData)
    {
        var draggedSlot = eventData.pointerDrag?.GetComponent<InventorySlotUI>();
        if (draggedSlot == null || !draggedSlot.IsInitialized) return;
        if (draggedSlot.SlotIndex == slotIndex) return;

        model.SwapSlots(draggedSlot.SlotIndex, slotIndex);
    }

    private void ShowGhost()
    {
        if (ghostIcon == null || ghostRoot == null) return;

        var canvasGroup = ghostRoot.GetComponent<CanvasGroup>();
        if (canvasGroup != null) canvasGroup.blocksRaycasts = false;

        ghostIcon.sprite = icon.sprite;
        ghostIcon.enabled = true;
        ghostIcon.raycastTarget = false;
        ghostRoot.gameObject.SetActive(true);
        icon.color = new Color(1, 1, 1, 0.5f);
    }

    private void UpdateGhostPosition(PointerEventData eventData)
    {
        if (ghostRoot == null) return;
        ghostRoot.position = eventData.position;
    }

    private void HideGhost()
    {
        if (ghostRoot == null) return;
        ghostRoot.gameObject.SetActive(false);
        icon.color = Color.white;
    }
}
