using System;
using Events;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

namespace Audio
{
    public class AudioManager : MonoBehaviour
    {
        private EventInstance _musicHotelEventInstance;
        private EventInstance _ambienceEventInstance;
        private EventInstance _footStepsEventInstance;
        private PlayerMadeAStep _playerMadeAStep;

        private void Awake() => _playerMadeAStep = GameGlobalStorage.Instance.GetPlayer().StepEvent;

        private void Start()
        {
            _musicHotelEventInstance = InitializeAndStart(FMODEvents.Instance.MusicHotel);
            _ambienceEventInstance = InitializeAndStart(FMODEvents.Instance.CityAmbience);
            _footStepsEventInstance = InitializeAndStart(FMODEvents.Instance.PlayerFootSteps);
        }

        private void OnEnable() => 
            _playerMadeAStep.Event += Player_OnMadeAStep;
        
        private void OnDisable() => 
            _playerMadeAStep.Event -= Player_OnMadeAStep;

        private EventInstance InitializeAndStart(EventReference musicEventReference)
        {
            EventInstance eventInstance = RuntimeManager.CreateInstance(musicEventReference.Path);
            
            eventInstance.start();
            
            return eventInstance;
        }
        
        private void Player_OnMadeAStep(object sender, PlayerMadeAStepEventArgs e)
        {
            _footStepsEventInstance.getDescription(out EventDescription description);

            description.getParameterDescriptionByName(FMODEventParams.Material, out PARAMETER_DESCRIPTION parameterDescription);

            description.getParameterLabelByName(FMODEventParams.Material, e.FloorFMODMaterialIndex, out string label);

            _footStepsEventInstance.setParameterByIDWithLabel(parameterDescription.id, label);
            
            _footStepsEventInstance.start();
        }
    }
}