using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public enum GameStatus
{
    GameWaiting,
    GameStarted
}

public class GameManager : Singleton<GameManager>
{
    #region Vars
    
    [Tooltip("Menu that is going to be used for the game.")]
    public MenuSO menu;
    [Tooltip("Game status that tells the system if we are playing or waiting to select a plate.")]
    public GameStatus gameStatus;
    [Tooltip("Transform point where the ingredients are going to be instantiated. That Transform should be placed over tableware.")]
    [SerializeField] private Transform instancePoint;

    private List<GameObject> selectedIngredients = new List<GameObject>();
    
    #endregion
    
    #region Unity

    void Start()
    {
        gameStatus = GameStatus.GameWaiting;
     
        // Esto para testear que se instancien bien
        // SetSelectedPlate(menu.MenuPlates()[0]);
    }
    
    #endregion
    
    #region Methods
    
    /*
     * Method that instantiates the ingredients for a selected plate  
     * It cleanses any previous selection of ingredients and place the new ones.
     */
    public void SetSelectedPlate(PlateSO plate)
    {
        if (gameStatus != GameStatus.GameWaiting)
            return;
        
        selectedIngredients.Clear();
        
        foreach(var ingredient  in plate.Ingredients())
        {
            Debug.Log($"Hay {ingredient.Quantity()} de {ingredient.Ingredient().name}");
            
            foreach (int value in Enumerable.Range(1, ingredient.Quantity()))
            {
                var go = Instantiate(ingredient.Ingredient(), instancePoint);
                selectedIngredients.Add(go);
            }
        }
    }

    /*
     * Method that is called when the GameStarts
     * In order for the game to start the fork/toothpick has to be grabbed.
     * Once the cutlery is grabbed, the ingredients start to run.
     */
    public void StartGame()
    {
        gameStatus = GameStatus.GameStarted;

        foreach (GameObject ingredient in selectedIngredients)
        {
            Debug.Log($"Comienza a moverse {ingredient.name}");

            // TODO 
            // Start movement in the Ingredient script movement controller 
            
        }
    }

    #endregion 
    
}
