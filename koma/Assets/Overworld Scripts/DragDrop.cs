using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler//, IDropHandler
{
    //used for dragging and dropping stuff around units around.
    [SerializeField] private GameObject removalSlot;
    [SerializeField] private PrepDungeonManager prepDun;
    [SerializeField] private Canvas prepDungeonMenuCanvas;
    [SerializeField] private CanvasGroup groupCanvasGroup;
    [SerializeField] private CanvasGroup reserveGroup;


    private RectTransform rect;
    private Vector2 originalPos;
    public static int pickedUpID;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
        originalPos = rect.anchoredPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (gameObject.GetComponent<ReserveUnitBox>() != null)
        {
            if (gameObject.GetComponent<ReserveUnitBox>().canDrag == false)
            {
                return;
            }
        }

        if (reserveGroup != null)
        {
            reserveGroup.blocksRaycasts = false;
            reserveGroup.alpha = 0.6f;
        }

        //disable the dragged item from capturing events
        groupCanvasGroup.blocksRaycasts = false;
        //also, make it slightly transparent
        groupCanvasGroup.alpha = 0.6f;
         
        if (removalSlot != null) removalSlot.SetActive(true);

        //tells us what box we were dragging. (0 - 5)
        if (gameObject.GetComponent<UnitBox>() != null)
        {
            pickedUpID = gameObject.GetComponent<UnitBox>().get_boxID();
        }
        else
        {
            pickedUpID = gameObject.GetComponent<ReserveUnitBox>().get_id();

        }
        //Debug.Log("id = " + pickedUpID);

    }

    public void OnDrag(PointerEventData eventData)
    {
        if (gameObject.GetComponent<ReserveUnitBox>() != null)
        {
            if (gameObject.GetComponent<ReserveUnitBox>().canDrag == false)
            {
                return;
            }
        }
        rect.anchoredPosition += (eventData.delta / prepDungeonMenuCanvas.scaleFactor);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //disable the dragged item from capturing events
        pickedUpID = -1;
        //can now capture events again.
        groupCanvasGroup.blocksRaycasts = true;

        //return transparency to normal
        groupCanvasGroup.alpha = 1f;

        if (reserveGroup != null)
        {
            reserveGroup.blocksRaycasts = true;
            reserveGroup.alpha = 1f;
        }

        //finally, snap the unit back to its original position.
        rect.anchoredPosition = originalPos;
        if (removalSlot != null) removalSlot.SetActive(false);


    }
    public void reset_after_drop()
    {
        //disable the dragged item from capturing events
        pickedUpID = -1;
        //can now capture events again.
        groupCanvasGroup.blocksRaycasts = true;

        //return transparency to normal
        groupCanvasGroup.alpha = 1f;

        if (reserveGroup != null)
        {
            reserveGroup.blocksRaycasts = true;
            reserveGroup.alpha = 1f;
        }

        //finally, snap the unit back to its original position.

        rect.anchoredPosition = originalPos;
        if (removalSlot != null) removalSlot.SetActive(false);

    }

}
