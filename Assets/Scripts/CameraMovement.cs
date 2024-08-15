using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Camera cam;
    //camera zoom
    private float zoom;
    public float zoomMultiplier = 2f;
    public float zoomMin = 1f;
    public float zoomMax = 15f;
    public float velocity = 0f;
    public float smoothTimeZoom = 0.25f;
    //camera mvt
    private Vector3 camPosOriginal;
    private Vector3 differenceMvt;
    private Vector3 posOriginal;
    private bool drag = false;

    void Start()
    {
        posOriginal = cam.transform.position;
        zoom = cam.orthographicSize;
    }

    void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            //camera zoom linked to scrollwheel and updates accordingly
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            zoom -= scroll * zoomMultiplier;
            zoom = Mathf.Clamp(zoom, zoomMin, zoomMax);
            cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, zoom, ref velocity, smoothTimeZoom);

            //registers clicks as dragging
            if (Input.GetMouseButton(0))
            {
                differenceMvt = cam.ScreenToWorldPoint(Input.mousePosition) - cam.transform.position;
                if (drag == false)
                {
                    drag = true;
                    camPosOriginal = cam.ScreenToWorldPoint(Input.mousePosition);
                }
            }
            else
            {
                drag = false;
            }
            //when dragging, move the camera
            if (drag)
            {
                cam.transform.position = camPosOriginal - differenceMvt;
            }
            //if right clocks, resets the camera position
            if (Input.GetMouseButton(1))
            {
                cam.transform.position = posOriginal;
            }
        }

    }


}
