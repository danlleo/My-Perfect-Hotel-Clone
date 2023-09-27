using System;
using Events;
using FMOD.Studio;
using FMODUnity;
using StaticEvents;
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

        private void OnEnable()
        {
            _playerMadeAStep.Event += Player_OnMadeAStep;
            OnAnyOutdoorsRoomTookPlayer.Event += AnyOutdoorsRoom_OnTookPlayer;
            OnAnyOutdoorsRoomLostPlayer.Event += AnyOutdoorsRoom_OnLostPlayer;
        }

        private void OnDisable()
        {
            _playerMadeAStep.Event -= Player_OnMadeAStep;
            OnAnyOutdoorsRoomTookPlayer.Event -= AnyOutdoorsRoom_OnTookPlayer;
            OnAnyOutdoorsRoomLostPlayer.Event -= AnyOutdoorsRoom_OnLostPlayer;
        }

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

        private void AnyOutdoorsRoom_OnTookPlayer(object sender, EventArgs e)
        {
            _ambienceEventInstance.setParameterByName(FMODEventParams.Outdoors, 1);
            _musicHotelEventInstance.setParameterByName(FMODEventParams.Outdoors, 1);
        }
        
        private void AnyOutdoorsRoom_OnLostPlayer(object sender, EventArgs e)
        {
            _ambienceEventInstance.setParameterByName(FMODEventParams.Outdoors, 0);
            _musicHotelEventInstance.setParameterByName(FMODEventParams.Outdoors, 0);
        }
    }
}