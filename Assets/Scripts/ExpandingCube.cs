using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ExpandingCube : MonoBehaviour
{

    [SerializeField] private Animator animator;
    [SerializeField] private Toggle toggle;
    [SerializeField] private Text nameToggle;
    [SerializeField] private RectTransform cube;

    [SerializeField] private GameObject[] cubes;
    [SerializeField] private TMP_Text[] nameCubes;

    [SerializeField] private Image[] images; 
    private Color[] originalColors;
    // Start is called before the first frame update
    void Start()
    {
        //toggle.isOn = !toggle.isOn;
        nameToggle.text = "Expanding";

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

    public void OpenCube()
    {
        animator.SetBool("OpenCube", toggle.isOn);
    }

    public void CloseCube() 
    { 
        animator.SetBool("OpenCube", !toggle.isOn);
    }

    public void Reset()
    {
        animator.SetBool("OpenCube", false);
        cube.transform.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;
        cube.transform.GetComponent<RectTransform>().rotation = Quaternion.identity;
        toggle.isOn = true;
    }

    private void Update()
    {
        if (!toggle.isOn)
            nameToggle.text = "Close";
        else
            nameToggle.text = "Expanding";


        for (int i = 0; i < cubes.Length; i++)
        {
            nameCubes[i].text = cubes[i].name ;

            
            images[i].color =  originalColors[i];
            //images[i].color = renderer.material.color;
        }
    }
    
}
