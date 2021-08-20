using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragCamera : MonoBehaviour
{
    //don't mess with this file lol.

    //used to drag the camera to view more portions of the map.
    //but: do not drag if the player is dragging ui stuff, though.

    //also: camera is clamped to the game world, you cannot keep dragging forever.

    [SerializeField] private DungeonManager dMan;
    [SerializeField] private Camera cam;
    [SerializeField] private float minZoom;
    [SerializeField] private float maxZoom;
    [SerializeField] private float zoomStep;

    private Vector3 dragOrigin;

    // Update is called once per frame
    void Update()
    {       
        //zooming camera
        if (Input.GetAxis("Mouse ScrollWheel") != 0f) zoom_camera();


        //dragging camera
        if (dMan.get_canDragScreen() == true)
        {
            pan_camera();
        }
        else
        {
            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    void zoom_camera()
    {
        //zooms on mousewheel scrolling. 
        //cannot zoom out more than maxZoom. 
        //cannot zoom in more than minZoon.

        //positive means up, negative means down.
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            
            float newSize = cam.orthographicSize - zoomStep;
            cam.orthographicSize = Mathf.Clamp(newSize, minZoom, maxZoom);
        }
        else
        {
            float newSize = cam.orthographicSize + zoomStep;
            cam.orthographicSize = Mathf.Clamp(newSize, minZoom, maxZoom);           
        }

    }

    void pan_camera()
    {
        //save position in world space when drag starts

        if (DungeonDragDrop.pickedUpID != -1) return;

        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);
        }

        //calc distance between drag origin and new position if still held down
        if (Input.GetMouseButton(0))
        {
            Vector3 diff = dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition);
            
            //move the camera by that distance
            cam.transform.position += diff;

        }

    }

}
