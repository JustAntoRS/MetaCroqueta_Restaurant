using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class OffsetGrab : XRGrabInteractable
{
    private Vector3 _interactorPosition = Vector3.zero;
    private Quaternion _interactorRotation = Quaternion.identity;

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        StoreInteractor(args.interactorObject as XRBaseInteractor);
        MatchAttachmentPoints(args.interactorObject as XRBaseInteractor);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        ResetAttachmentPoints(args.interactorObject as XRBaseInteractor);
        ClearInteractor(args.interactorObject as XRBaseInteractor);
    }

    private void StoreInteractor(XRBaseInteractor interactor)
    {
        _interactorPosition = interactor.attachTransform.localPosition;
        _interactorRotation = interactor.attachTransform.localRotation;
    }

    private void MatchAttachmentPoints(XRBaseInteractor interactor)
    {
        bool hasAttach = attachTransform != null;
        interactor.attachTransform.position = hasAttach ? attachTransform.position : transform.position;
        interactor.attachTransform.rotation = hasAttach ? attachTransform.rotation : transform.rotation;
    }

    private void ResetAttachmentPoints(XRBaseInteractor interactor)
    {
        interactor.attachTransform.localPosition = _interactorPosition;
        interactor.attachTransform.localRotation = _interactorRotation;
    }

    private void ClearInteractor(IXRSelectInteractor interactor)
    {
        _interactorPosition = Vector3.zero;
        _interactorRotation = Quaternion.identity;
    }
}
