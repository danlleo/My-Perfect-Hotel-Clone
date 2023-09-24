using Events;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

namespace Audio
{
    public class AudioManager : MonoBehaviour
    {
        private PlayerMadeAStep _playerMadeAStep;

        private void Awake() => _playerMadeAStep = GameGlobalStorage.Instance.GetPlayer().StepEvent;

        private void OnEnable() => 
            _playerMadeAStep.Event += Player_OnMadeAStep;
        
        private void OnDisable() => 
            _playerMadeAStep.Event -= Player_OnMadeAStep;
        
        private void Player_OnMadeAStep(object sender, PlayerMadeAStepEventArgs e)
        {
            EventReference eventReference = FMODEvents.Instance.PlayerFootSteps;
            EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference.Path);
            
            eventInstance.getDescription(out EventDescription description);

            description.getParameterDescriptionByIndex(0, out PARAMETER_DESCRIPTION parameterDescription);

            description.getParameterLabelByIndex(0, e.FloorFMODMaterialIndex, out string label);

            print(label);

            eventInstance.setParameterByIDWithLabel(parameterDescription.id, label);
            
            eventInstance.start();
            
            eventInstance.release();
        }
    }
}