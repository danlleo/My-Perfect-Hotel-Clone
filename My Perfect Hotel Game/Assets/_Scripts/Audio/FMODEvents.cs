using FMODUnity;
using Misc;
using UnityEngine;

namespace Audio
{
    public class FMODEvents : Singleton<FMODEvents>
    {
        public EventReference MusicHotel => _musicHotel;
        public EventReference CityAmbience => _cityAmbience;
        public EventReference PlayerFootSteps => _playerFootSteps;
        
        [SerializeField] private EventReference _musicHotel;
        [SerializeField] private EventReference _cityAmbience;
        [SerializeField] private EventReference _playerFootSteps;
    }
}
