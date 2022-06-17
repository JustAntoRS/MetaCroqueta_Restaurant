using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshCollider))]
[RequireComponent(typeof(MeshRenderer))]
public class PlateBehaviour : MonoBehaviour
{
    private MeshRenderer _mesh;
    private MeshCollider _collider;
    
    void Start()
    {
        _mesh = GetComponent<MeshRenderer>();
        _collider = GetComponent<MeshCollider>();
    }

    public void GameStatusChangeEventHandler(GameStatus status)
    {
        if (status == GameStatus.GameWaiting)
        {
            show();
        }
        else if (status == GameStatus.GameStarted)
        {
            hide();
        }
    }
    private void hide()
    { 
        _mesh.enabled = false; 
        _collider.enabled = false;
    }

    private void show()
    {
        _mesh.enabled = true; 
        _collider.enabled = true;
    }
}
