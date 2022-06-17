using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FoodBehaviour : MonoBehaviour
{
    #region Vars
    
    [SerializeField] private GameObject targetPos;
    [Range(0f, 20f)]
    [SerializeField] private float movementSpeed;

    [SerializeField] private bool isStarted;
    [SerializeField] private GameObject vfxGrabbing;
    private bool movementFinished;
    private XRGrabInteractable _interactable;
    
    #endregion
    
    #region Unity
    
    // Start is called before the first frame update
    void Start()
    {
        isStarted = false;
        movementFinished = true;
        targetPos = gameObject;
        _interactable = GetComponent<XRGrabInteractable>();
        if (_interactable)
            _interactable.selectEntered.AddListener(OnSelectEntered_Food);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentPos = transform.position;
        
        if (movementFinished && isStarted)
        {
            movementFinished = false;
            targetPos = FoodMovementManager.Instance.GetRandomMovementPoint();
            Debug.Log("Nuevo Destino");
        }
        
        transform.position = Vector3.Lerp(currentPos, targetPos.transform.position, Time.deltaTime * movementSpeed);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Mouth"))
        {
            Debug.Log($"Comido {other.name}");
            GameManager.Instance.IncreasePoints(gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("MovementPoint") && targetPos == other.gameObject)
        {
            movementFinished = true;
        }
    }
    
    private void OnSelectEntered_Food(SelectEnterEventArgs args)
    {
        Vector3 position = this.transform.position;
        position += Vector3.up * 0.1f; // Plus 0.1 in the Y scale
        Debug.Log("Instantiated grabbing VFX at position " + position);
        if(vfxGrabbing)
        {
            GameObject vfx = Instantiate(vfxGrabbing);
            vfx.transform.position = position;   
        }
        else
        {
            Debug.Log("VFX IS NULL WHEN GRABBING A CUBE");
        }
    }
    
    #endregion
    
    #region Methods
    
    public void StartMovement()
    {
        isStarted = true;
    }
    
    #endregion
}
