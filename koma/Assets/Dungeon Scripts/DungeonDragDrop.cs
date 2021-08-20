using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DungeonDragDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler//, IDropHandler
{
    //used for dragging and dropping stuff around units around.
    [SerializeField] private DungeonManager dManager;
    [SerializeField] private Canvas partyMenuCanvas;
    [SerializeField] private CanvasGroup groupCanvasGroup;

    private RectTransform rect;
    private Vector2 originalPos;
    public static int pickedUpID;

    void Awake()
    {
        pickedUpID = -1;
        rect = GetComponent<RectTransform>();
        originalPos = rect.anchoredPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {

        //disable the dragged item from capturing events
        groupCanvasGroup.blocksRaycasts = false;
        //also, make it slightly transparent
        groupCanvasGroup.alpha = 0.6f;

        //tells us what box we were dragging. (0 - 5)

        pickedUpID = gameObject.GetComponent<DungeonUnitBox>().get_boxID();
        
        //Debug.Log("id = " + pickedUpID);

    }

    public void OnDrag(PointerEventData eventData)
    {
        if (DungeonManager.state == DungeonState.COMBAT || DungeonManager.canSwapUnits == false)
        {
            return;
        }
        rect.anchoredPosition += (eventData.delta / partyMenuCanvas.scaleFactor);
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        //disable the dragged item from capturing events
        pickedUpID = -1;
        //can now capture events again.
        groupCanvasGroup.blocksRaycasts = true;

        //return transparency to normal
        groupCanvasGroup.alpha = 1f;

        //finally, snap the unit back to its original position.
        rect.anchoredPosition = originalPos;
    }

    public void reset_after_drop()
    {

        //disable the dragged item from capturing events
        pickedUpID = -1;
        //can now capture events again.
        groupCanvasGroup.blocksRaycasts = true;

        //return transparency to normal
        groupCanvasGroup.alpha = 1f;

        //finally, snap the unit back to its original position.
        rect.anchoredPosition = originalPos;


    }

}
