using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    #region Vars
    
    [Header("References")]
    [SerializeField] private List<MenuPlateUI> platesUI;
   
    private MenuSO _menu;

    #endregion
    
    #region Unity

    /*
     * Gets the menu from the GameManager and ends if menu is null or does not have plates
     * Otherwise the plates are shown inside the menu
     */
    private void Start()
    {
        
        _menu = GameManager.Instance.menu;
        DisableMenuPlates();

        if (_menu == null || _menu.MenuPlates().Count < 1)
        {
            Debug.LogError($"[{gameObject.name}] No plates in the menu.", GameManager.Instance.gameObject);
            return;
        }

        InitMenuPlates();
    }

    private void OnDisable()
    {
        for (int i = 0; i < _menu.MenuPlates().Count; i++)
        {
            PlateSO plate = _menu.MenuPlates()[i];
            platesUI[i].OnSelectedMenuPlate -= PickMenuPlate;
        }
    }

    #endregion
    
    #region Methods

    /*
     * Hide every plateUI panel from the Menu 
     */
    private void DisableMenuPlates()
    {
        foreach (var plate in platesUI)
        {
            plate.gameObject.SetActive(false);
        }
    }
    
    /*
     * Shows each plate by using the plateUI to update the image and name 
     */
    private void InitMenuPlates()
    {
        for (int i = 0; i < _menu.MenuPlates().Count; i++)
        {
            PlateSO plate = _menu.MenuPlates()[i];
            platesUI[i].SetMenuPlate(i, plate.PlateImage(), plate.PlateName());
            platesUI[i].OnSelectedMenuPlate += PickMenuPlate;
        }
    }

    private void PickMenuPlate(int index)
    {
        Debug.Log($"Elegido plato {index}");
        GameManager.Instance.SetSelectedPlate(_menu.MenuPlates()[index]);
    }
    
    #endregion
}
