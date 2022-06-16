using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[Serializable]
public class MenuPlateUI : MonoBehaviour
{
    [SerializeField] private int plateIndex;
    [SerializeField] private Image menuPlateImage;
    [SerializeField] private TMP_Text menuPlateName;

    public delegate void SelectMenuPlate(int index);
    public event SelectMenuPlate OnSelectedMenuPlate;
    
    public void SetMenuPlate(int index, Image image, string name)
    {
        plateIndex = index;
        menuPlateImage = image;
        menuPlateName.text = name;
        
        gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            Debug.Log(other.name);
            if(OnSelectedMenuPlate != null)
                OnSelectedMenuPlate(plateIndex);
        }
    }
}


