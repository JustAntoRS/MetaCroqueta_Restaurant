using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRGrabInteractable))]
public class CutleryBehaviour : MonoBehaviour
{
    private XRGrabInteractable _interactable;
    
    private void Start()
    {
        _interactable = GetComponent<XRGrabInteractable>();

        if (_interactable)
            _interactable.selectEntered.AddListener(OnSelectEntered_Cutlery);
    }

    private void OnSelectEntered_Cutlery(SelectEnterEventArgs args)
    {
        GameManager.Instance.StartGame();
    }
}
