using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRNestedInteractor : MonoBehaviour
{
    private XRGrabInteractable _parentInteractable;
    private XRDirectInteractor _nestedInteractor;

    private ActionBasedController _xrController;
    
    #region Unity
    
    // Start is called before the first frame update
    private void Start()
    {
        _parentInteractable = GetComponentInParent<XRGrabInteractable>();
        
        _nestedInteractor = GetComponent<XRDirectInteractor>();
        _xrController = GetComponent<ActionBasedController>();
    }

    private void OnEnable()
    {
        _parentInteractable.activated.AddListener(OnActivated_ParentInteractable);
        _parentInteractable.deactivated.AddListener(OnDeactivated_ParentInteractable);
    }

    private void OnDisable()
    {
        _parentInteractable.activated.RemoveListener(OnActivated_ParentInteractable);
        _parentInteractable.deactivated.RemoveListener(OnDeactivated_ParentInteractable);
    }
    
    #endregion
    
    #region Callbacks

    private void OnActivated_ParentInteractable(ActivateEventArgs args)
    {
        var controller = args.interactorObject.transform.GetComponent<XRBaseControllerInteractor>();
        _nestedInteractor.xrController = controller.xrController;
    }

    private void OnDeactivated_ParentInteractable(DeactivateEventArgs args)
    {
        _nestedInteractor.xrController = _xrController;
    }
    
    #endregion
}
