using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{

    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private RectTransform scrollArea;

    private Vector3 lastMousePosition;
    private bool isRotating = false;

    void Update()
    {

        if (IsMouseOverUI())
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            lastMousePosition = Input.mousePosition;
            isRotating = true;
        }

        
        if (Input.GetMouseButton(0) && isRotating)
        {
            Vector3 deltaMousePosition = Input.mousePosition - lastMousePosition;

            float rotationY = deltaMousePosition.x * rotationSpeed * Time.deltaTime;
            float rotationX = -deltaMousePosition.y * rotationSpeed * Time.deltaTime;

            transform.Rotate(Vector3.up, rotationY, Space.World);
            transform.Rotate(Vector3.right, rotationX, Space.Self);

            lastMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isRotating = false;
        }
    }

    private bool IsMouseOverUI()
    {
        if(scrollArea == null)
            return false;

        Vector2 localMousePosition = scrollArea.InverseTransformPoint(Input.mousePosition);
        return scrollArea.rect.Contains(localMousePosition);
    }
}
