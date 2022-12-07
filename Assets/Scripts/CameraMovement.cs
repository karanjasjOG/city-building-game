using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraMovement : MonoBehaviour
{

    Camera cam;
    public float maxZoom = 5;
    private float minZoom = 10;
    public float sensitivity = 1;
    public float zoomSpeed = 30;
    public float moveSpeed = 30;

    public float boundaryTop;
    public float boundaryRight;
    public float boundaryBottom;
    public float boundaryLeft;


    float targetZoom;
    bool isGrabed = false;

    private enum Boundaries
    {
        Top, Right, Bottom, Left
    }

    private Dictionary<Boundaries, float> cameraBoundaries = new Dictionary<Boundaries, float>();

    private void Awake()
    {
        cam = Camera.main;
        cameraBoundaries.Add(Boundaries.Top, boundaryTop);
        cameraBoundaries.Add(Boundaries.Bottom, boundaryBottom);
        cameraBoundaries.Add(Boundaries.Right, boundaryRight);
        cameraBoundaries.Add(Boundaries.Left, boundaryLeft);
    }
    void Update()
    {
        

        if (Input.mouseScrollDelta.y != 0)
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            targetZoom -= Input.mouseScrollDelta.y * sensitivity;
            targetZoom = Mathf.Clamp(targetZoom, maxZoom, minZoom);
            if (IsItTouchingEdges())
            {
                Vector3 targetPos = new Vector3(0, 0, -10);
                transform.position = Vector3.MoveTowards(transform.position, targetPos, zoomSpeed * 2 * Time.deltaTime);
                float newSize = Mathf.MoveTowards(cam.orthographicSize, targetZoom, zoomSpeed * Time.deltaTime);
                cam.orthographicSize = newSize;
                if (newSize == 10)
                {
                    transform.position = new Vector3(0, 0, -10);
                }
            }
            else
            {
                float newSize = Mathf.MoveTowards(cam.orthographicSize, targetZoom, zoomSpeed * Time.deltaTime);
                cam.orthographicSize = newSize;
            }
        }
        if (Input.GetMouseButtonDown(2))
        {
            isGrabed = true;
        }
        if (Input.GetMouseButtonUp(2))
        {
            isGrabed = false;
        }
        if (isGrabed)
        {
            float movementX = -Input.GetAxis("Mouse X");
            float movementY = -Input.GetAxis("Mouse Y");
            Vector3 targetPos = transform.position + GetMovementXY(movementX, movementY);
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
        }

    }

    Vector3 GetMovementXY(float movementX1, float movementY1)
    {
        float x = transform.position.x;
        float y = transform.position.y;
        if (y >= 0)
        {
            y = y + cam.orthographicSize;
        }
        else
        {
            y = y - cam.orthographicSize;
        }
        if (x >= 0)
        {
            x = x + cam.orthographicSize * 1.77f;
        }
        else
        {
            x = x - cam.orthographicSize * 1.77f;
        }
        float movementX = movementX1;
        float movementY = movementY1;


        #region Actual Logic



        if (y >= 0)
        {
            if (y >= cameraBoundaries[Boundaries.Top])
            {

                if (movementY > 0)
                {
                    movementY = 0;
                }

            }
            if (x >= cameraBoundaries[Boundaries.Right])
            {
                if (movementX > 0)
                    movementX = 0;
            }
            if (x <= cameraBoundaries[Boundaries.Left])
            {
                if (movementX < 0)
                    movementX = 0;
            }


        }
        if (y < 0)
        {
            if (y <= cameraBoundaries[Boundaries.Bottom])
            {

                if (movementY < 0)
                {
                    movementY = 0;
                }

            }
            if (x >= cameraBoundaries[Boundaries.Right])
            {
                if (movementX > 0)
                    movementX = 0;
            }
            if (x <= cameraBoundaries[Boundaries.Left])
            {
                if (movementX < 0)
                    movementX = 0;
            }
        }

        #endregion

        return new Vector3(movementX, movementY, 0);
    }

    bool IsItTouchingEdges()
    {
        float x = transform.position.x;
        float y = transform.position.y;

        if (y >= 0)
        {
            y = y + cam.orthographicSize;
        }
        else
        {
            y = y - cam.orthographicSize;
        }
        if (x >= 0)
        {
            x = x + cam.orthographicSize * 1.77f;
        }
        else
        {
            x = x - cam.orthographicSize * 1.77f;
        }

        #region Actual Logic

        if (y >= cameraBoundaries[Boundaries.Top] || y <= cameraBoundaries[Boundaries.Bottom])
        {
            return true;

        }
        if (x >= cameraBoundaries[Boundaries.Right] || x <= cameraBoundaries[Boundaries.Left])
        {
            return true;
        }

        #endregion

        return false;

    }


}
