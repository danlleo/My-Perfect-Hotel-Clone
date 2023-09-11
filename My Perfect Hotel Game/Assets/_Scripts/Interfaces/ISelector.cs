using InteractableObject;
using UnityEngine;

public interface ISelector
{
    public void Check(Ray ray);
    
    public Selectable GetSelected();
}
