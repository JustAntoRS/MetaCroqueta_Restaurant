using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[Serializable]
public class MenuPlateUI : MonoBehaviour
{
    [SerializeField] private Image menuPlateImage;
    [SerializeField] private TMP_Text menuPlateName;
    
    public void SetMenuPlate(Image image, string name)
    {
        menuPlateImage = image;
        menuPlateName.text = name;
        
        gameObject.SetActive(true);
    }
}
