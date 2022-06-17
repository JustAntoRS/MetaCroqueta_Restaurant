using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum GameStatus
{
    GameWaiting,
    GameStarted
}

[Serializable]
public class SpawnPosition
{
    public GameObject position;
    public bool isVisited;

    public void StartPosition()
    {
        isVisited = false;
    }

    public void SetVisited()
    {
        isVisited = true;
    }

}

public class GameManager : Singleton<GameManager>
{
    #region Vars
    
    [Tooltip("Menu that is going to be used for the game.")]
    public MenuSO menu;
    [Tooltip("Game status that tells the system if we are playing or waiting to select a plate.")]
    public GameStatus gameStatus;
    [Tooltip("Transform point where the ingredients are going to be instantiated. That Transform should be placed over tableware.")]
    [SerializeField] private List<SpawnPosition> spawnPositions;

    // [SerializeField] private GameObject vfxEating;
    [SerializeField] private GameObject sfxEating;
     
    private List<GameObject> selectedIngredients = new List<GameObject>();
    
    [Tooltip("Event that tracks the current game status")]
    public UnityEvent<GameStatus> gameStatusChangeEvent;
    [Tooltip("Event that tracks how many points the player won")]
    public UnityEvent<int> increasePoints;
    
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
        
        ClearIngredientsFromPlate();
        ClearSpawningPoints();
        
        foreach(var ingredient  in plate.Ingredients())
        {
            foreach (int value in Enumerable.Range(1, ingredient.Quantity()))
            {
                GameObject spawnPoint = null;
                
                foreach (var point in spawnPositions)
                {
                    if (!point.isVisited)
                    {
                        Debug.Log($"Se utiliza {point.position.name}");
                        spawnPoint = point.position;
                        point.SetVisited();
                        break;
                    }
                }
                var go = Instantiate(ingredient.Ingredient(), spawnPoint.transform);
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
        if (gameStatus == GameStatus.GameStarted)
            return;
        
        gameStatus = GameStatus.GameStarted;
        gameStatusChangeEvent.Invoke(gameStatus);

        foreach (GameObject ingredient in selectedIngredients)
        {
            ingredient.GetComponent<FoodBehaviour>().StartMovement();
        }
    }

    private void ClearIngredientsFromPlate()
    {
        foreach (var ingredient in selectedIngredients)
        {
            Destroy(ingredient);
        }
        
        selectedIngredients.Clear();
    }
    
    private void ClearSpawningPoints()
    {
        foreach (var point in spawnPositions)
        {
            point.StartPosition();
        }
    }

    public void IncreasePoints(GameObject ingredient)
    {
        
        Vector3 position = ingredient.transform.position;
        // GameObject vfx = Instantiate(vfxEating);
        // vfx.transform.position = position;
        
        GameObject sfx = Instantiate(sfxEating);
        sfx.transform.position = position;
        
        selectedIngredients.Remove(ingredient);
        Destroy(ingredient);
        
        increasePoints.Invoke(100);
        Debug.Log("+100 points");

        if (selectedIngredients.Count == 0)
        {
            gameStatus = GameStatus.GameWaiting;
            gameStatusChangeEvent.Invoke(gameStatus);
        }
    }
    
    #endregion
}
