using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObjects/Menu", fileName = "New Menu")]
public class MenuSO : ScriptableObject
{
    [SerializeField] private List<PlateSO> menuPlates;
    
    public List<PlateSO> MenuPlates() => menuPlates;
}
