using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private float zoomSpeed = 10f; // Velocidade do zoom
    [SerializeField] private float minZoom = 5f; // Limite mínimo de zoom
    [SerializeField] private float maxZoom = 40f; // Limite máximo de zoom

    private float currentZoom; // Zoom atual

    void Start()
    {
        if (cam == null)
        {
            cam = Camera.main;
        }
        currentZoom = cam.fieldOfView; // Inicializa com o valor atual
    }

    void Update()
    {
        // Zoom com o mouse
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        if (scrollInput != 0f)
        {
            AdjustZoom(scrollInput);
        }

        // Zoom com toque em dispositivos móveis
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                // Usar o delta do toque para ajustar o zoom
                float touchDelta = touch.deltaPosition.y;
                AdjustZoom(touchDelta * 0.1f); // Ajuste a sensibilidade conforme necessário
            }
        }
    }

    private void AdjustZoom(float delta)
    {
        // Atualiza o zoom atual
        currentZoom -= delta * zoomSpeed;

        // Limita o zoom
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

        // Atualiza o campo de visão da câmera
        cam.fieldOfView = currentZoom;

        // Calcula o ponto no espaço 3D onde o mouse está apontando
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, Vector3.zero); // Ajustar o plano conforme necessário
        float distance;

        if (plane.Raycast(ray, out distance))
        {
            Vector3 pointOnPlane = ray.GetPoint(distance);

            // Move a câmera para manter o ponto no espaço 3D visível no centro da tela
            Vector3 directionToPoint = (cam.transform.position - pointOnPlane).normalized;
            cam.transform.position = pointOnPlane + directionToPoint * (cam.transform.position - pointOnPlane).magnitude;
        }
    }
}
