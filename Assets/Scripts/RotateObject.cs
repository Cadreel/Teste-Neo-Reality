using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotateObject : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private RectTransform scrollArea;
    [SerializeField] private Scrollbar scrollbar;
    [SerializeField] private Transform[] cubes;

    private Vector3 lastMousePosition;
    private bool isRotating = false;
    private Transform pivot;

    private void Start()
    {
        Vector3 center = Vector3.zero;

        foreach (Transform cube in cubes)
        {
            center += cube.position;
        }

        center /= cubes.Length;

        pivot = new GameObject("Pivot").transform;
        pivot.position = center;
        pivot.rotation = Quaternion.identity;

        foreach (Transform cube in cubes)
        {
            cube.SetParent(pivot, true);
        }

        pivot.SetParent(transform, false);
        pivot.localPosition = Vector3.zero;
    }

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
        if (scrollArea == null || scrollbar == null)
            return false;

        Vector2 localMousePosition = scrollArea.InverseTransformPoint(Input.mousePosition);
        if (scrollArea.rect.Contains(localMousePosition))
            return true;

        Vector2 localMousePositionScrollbar = scrollbar.GetComponent<RectTransform>().InverseTransformPoint(Input.mousePosition);
        return scrollbar.GetComponent<RectTransform>().rect.Contains(localMousePositionScrollbar);
    }
}
