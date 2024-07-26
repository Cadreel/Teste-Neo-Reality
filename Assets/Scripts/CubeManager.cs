using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeManager : MonoBehaviour
{
    [SerializeField] private GameObject[] cubes;
    private Color[] originalColors;

    void Start()
    {
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
        for (int i = 0; i < cubes.Length; i++)
        {
            Renderer renderer = cubes[i].GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = originalColors[i];
            }
        }

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
