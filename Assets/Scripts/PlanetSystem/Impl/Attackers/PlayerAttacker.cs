using System;
using System.Collections.Generic;
using System.Linq;
using Core.Tools.Repository;
using PlanetSystem.Api;
using PlanetSystem.Data;
using RocketSystem.Data;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace PlanetSystem.Impl.Attackers
{
    public class PlayerAttacker : MonoBehaviour, IPlayerAttacker
    {
        public event Action<RocketType> OnPushRocket;

        [Inject] private Repository<RocketPreferences> _repository;


        [SerializeField, FoldoutGroup("Setup/PushRocket", false), ChildGameObjectsOnly]
        private Button _pushDefaultRocketButton;
        [SerializeField, FoldoutGroup("Setup/PushRocket", false), ChildGameObjectsOnly]
        private TMP_Text _pushDefaultRocketText;
        
        [SerializeField, FoldoutGroup("Setup/SpeedRocket", false), ChildGameObjectsOnly]
        private Button _pushSpeedRocketButton;
        [SerializeField, FoldoutGroup("Setup/SpeedRocket", false), ChildGameObjectsOnly]
        private TMP_Text _pushSpeedRocketText;
        
        [SerializeField, FoldoutGroup("Setup/DamageRocket", false), ChildGameObjectsOnly]
        private Button _pushDamageRocketButton;
        [SerializeField, FoldoutGroup("Setup/DamageRocket", false), ChildGameObjectsOnly]
        private TMP_Text _pushDamageRocketText;
        
        [SerializeField, FoldoutGroup("Setup", false), ChildGameObjectsOnly]
        private TMP_Text _cooldownTimer;

        private float Cooldown
        {
            get => _cooldown;
            set
            {
                _cooldown = value;
                
                if(_cooldown <= 0)
                    _cooldownTimer.SetText("Ready");
                else _cooldownTimer.SetText($"00:{Convert.ToInt32(_cooldown):00}");

                //TODO: will Update only change state
                UpdateUI();
            }
        }
        private float _cooldown = 0;

        private List<AmountRocketsOnPlanet> _amountPlanets = new List<AmountRocketsOnPlanet>(); 

        private void OnEnable()
        {
            _pushDefaultRocketButton.onClick.AddListener(OnPushDefaultRocket);
            _pushSpeedRocketButton.onClick.AddListener(OnPushSpeedRocket);
            _pushDamageRocketButton.onClick.AddListener(OnPushDamageRocket);
        }
        
        private void OnDisable()
        {
            _pushDefaultRocketButton.onClick.RemoveListener(OnPushDefaultRocket);
            _pushSpeedRocketButton.onClick.RemoveListener(OnPushSpeedRocket);
            _pushDamageRocketButton.onClick.RemoveListener(OnPushDamageRocket);
        }

        private void Update()
        {
            if (Cooldown > 0)
            {
                Cooldown -= Time.deltaTime;
            }
        }

        public void Setup(List<AmountRocketsOnPlanet> amountPlanets)
        {
            _amountPlanets = new List<AmountRocketsOnPlanet>(amountPlanets);
            UpdateUI();
        }
        
        public void Stop()
        {
            SceneManager.LoadScene(0);
        }
        
        private void OnPushDamageRocket()
        {
            var amountRocketsOnPlanet = GetRocketAmountOnType(RocketType.Damage);
            
            amountRocketsOnPlanet = PushRocket(amountRocketsOnPlanet);

            SetStateButton(_pushDamageRocketButton, _pushDamageRocketText, amountRocketsOnPlanet);
        }

        private void OnPushSpeedRocket()
        {
            var amountRocketsOnPlanet = GetRocketAmountOnType(RocketType.Speed);
            
            amountRocketsOnPlanet = PushRocket(amountRocketsOnPlanet);

            SetStateButton(_pushSpeedRocketButton, _pushSpeedRocketText, amountRocketsOnPlanet);
        }

        private void OnPushDefaultRocket()
        {
            var amountRocketsOnPlanet = GetRocketAmountOnType(RocketType.Default);

            amountRocketsOnPlanet = PushRocket(amountRocketsOnPlanet);
            
            SetStateButton(_pushDefaultRocketButton,_pushDefaultRocketText, amountRocketsOnPlanet);
        }

        private AmountRocketsOnPlanet PushRocket(AmountRocketsOnPlanet rocketsOnPlanet)
        {
            if(Cooldown > 0)
                return rocketsOnPlanet;
            
            var type = rocketsOnPlanet.RocketType;
            
            var indexOfPlanet= _amountPlanets.IndexOf(rocketsOnPlanet);
            rocketsOnPlanet.Amount--;

            if (rocketsOnPlanet.Amount <= 0)
            {
                _amountPlanets.RemoveAt(indexOfPlanet);
            }
            else
            {
                _amountPlanets[indexOfPlanet] = rocketsOnPlanet;
            }

            Cooldown = _repository.First(preference => preference.Type == type).Cooldown;
            
            OnPushRocket?.Invoke(rocketsOnPlanet.RocketType);
            
            return rocketsOnPlanet;
        }

        private void SetStateButton(Button button, TMP_Text text, AmountRocketsOnPlanet amountRocketsOnPlanet)
        {
            button.interactable = !amountRocketsOnPlanet.Equals(default) && amountRocketsOnPlanet.Amount > 0 && _cooldown <= 0;
            text.SetText($"{amountRocketsOnPlanet.RocketType.ToString()} {amountRocketsOnPlanet.Amount}");
        }
        
        private AmountRocketsOnPlanet GetRocketAmountOnType(RocketType type)
        {
            return _amountPlanets.FirstOrDefault(p => p.RocketType == type);
        }

        private void UpdateUI()
        {
            SetStateButton(_pushDefaultRocketButton,_pushDefaultRocketText, GetRocketAmountOnType(RocketType.Default));
            SetStateButton(_pushSpeedRocketButton, _pushSpeedRocketText, GetRocketAmountOnType(RocketType.Speed));
            SetStateButton(_pushDamageRocketButton, _pushDamageRocketText, GetRocketAmountOnType(RocketType.Damage));
        }

    }
}