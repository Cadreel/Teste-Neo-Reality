using UnityEngine;
using UnityEngine.EventSystems;

public class HighlightOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public CubeManager cubeManager;
    public int cubeIndex;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (cubeManager != null)
        {
            cubeManager.HighlightCube(cubeIndex);
        }
    }

    [SerializeField]
    public void OnPointerExit(PointerEventData eventData)
    {
        if (cubeManager != null)
        {
            cubeManager.HighlightCube(-1);
        }
    }

    public void Teste()
    {

    }
}
