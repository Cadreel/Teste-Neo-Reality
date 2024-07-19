using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeManager : MonoBehaviour
{
    [SerializeField] private GameObject[] cubes;
    private Color[] originalColors;

    void Start()
    {
        // Armazena as cores originais dos cubos
        originalColors = new Color[cubes.Length];
        for (int i = 0; i < cubes.Length; i++)
        {
            Renderer renderer = cubes[i].GetComponent<Renderer>();
            if (renderer != null)
            {
                originalColors[i] = renderer.material.color;
            }
        }
    }

    public void HighlightCube(int index)
    {
        // Restaura as cores originais dos cubos
        for (int i = 0; i < cubes.Length; i++)
        {
            Renderer renderer = cubes[i].GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = originalColors[i];
            }
        }

        // Destaca o cubo selecionado
        if (index >= 0 && index < cubes.Length)
        {
            Renderer renderer = cubes[index].GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = Color.yellow; // Cor de destaque
            }
        }
    }
}
