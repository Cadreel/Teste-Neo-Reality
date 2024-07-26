using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private float zoomSpeed = 10f; // Velocidade do zoom
    [SerializeField] private float minZoom = 5f; // Limite m�nimo de zoom
    [SerializeField] private float maxZoom = 40f; // Limite m�ximo de zoom
    [SerializeField] private float initialZoom; // Zoom inicial

    private float currentZoom; // Zoom atual
    private Vector3 initialPosition; // Posi��o inicial da c�mera
    private Quaternion initialRotation; // Rota��o inicial da c�mera

    void Start()
    {
        if (cam == null)
        {
            cam = Camera.main;
        }
        currentZoom = cam.fieldOfView; // Inicializa com o valor atual
        initialZoom = cam.fieldOfView; // Salva o zoom inicial
        initialPosition = new Vector3(0, 1, -10); // Salva a posi��o inicial
        initialRotation = cam.transform.rotation; // Salva a rota��o inicial
    }

    void Update()
    {
        // Zoom com o mouse
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        if (scrollInput != 0f)
        {
            AdjustZoom(scrollInput);
        }

        // Zoom com toque em dispositivos m�veis
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                // Usar o delta do toque para ajustar o zoom
                float touchDelta = touch.deltaPosition.y;
                AdjustZoom(touchDelta * 0.1f); // Ajuste a sensibilidade conforme necess�rio
            }
        }
    }

    private void AdjustZoom(float delta)
    {
        // Atualiza o zoom atual
        currentZoom -= delta * zoomSpeed;

        // Limita o zoom
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

        // Atualiza o campo de vis�o da c�mera
        cam.fieldOfView = currentZoom;

        // Calcula o ponto no espa�o 3D onde o mouse est� apontando
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, Vector3.zero); // Ajustar o plano conforme necess�rio
        float distance;

        if (plane.Raycast(ray, out distance))
        {
            Vector3 pointOnPlane = ray.GetPoint(distance);

            // Move a c�mera para manter o ponto no espa�o 3D vis�vel no centro da tela
            Vector3 directionToPoint = (cam.transform.position - pointOnPlane).normalized;
            cam.transform.position = pointOnPlane + directionToPoint * (cam.transform.position - pointOnPlane).magnitude;
        }
    }

    public void ResetZoom()
    {
        cam.fieldOfView = initialZoom; // Reseta o campo de vis�o
        cam.transform.position = initialPosition; // Reseta a posi��o da c�mera
        cam.transform.rotation = initialRotation; // Reseta a rota��o da c�mera
    }
}
