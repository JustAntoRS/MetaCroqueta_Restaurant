using System;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "ScriptableObjects/Plate", fileName = "New Plate")]
public class PlateSO : ScriptableObject
{
    [SerializeField] private Sprite plateImage;
    [SerializeField] private string plateName;
    [SerializeField] private List<IngredientQuantity> ingredients;

    public Sprite PlateImage() => plateImage;
    public string PlateName() => plateName;
    public List<IngredientQuantity> Ingredients() => ingredients;
}

[Serializable]
public struct IngredientQuantity
{
    [SerializeField] private GameObject ingredient;
    [SerializeField] private int quantity;

    public GameObject Ingredient() => ingredient;
    public int Quantity() => quantity;
}
