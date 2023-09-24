using System;
using Interfaces;
using UnityEngine;

namespace Events
{
    public class PlayerMadeAStep : MonoBehaviour, IEvent<PlayerMadeAStepEventArgs>
    {
        public event EventHandler<PlayerMadeAStepEventArgs> Event;
        
        public void Call(object sender, PlayerMadeAStepEventArgs eventArgs) => 
            Event?.Invoke(sender, eventArgs);
    }

    public class PlayerMadeAStepEventArgs : EventArgs
    {
        public readonly int FloorFMODMaterialIndex;

        public PlayerMadeAStepEventArgs(int floorFMODMaterialIndex) => 
            FloorFMODMaterialIndex = floorFMODMaterialIndex;
    }
}
