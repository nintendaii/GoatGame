using System;
using System.Collections.Generic;
using Abilities;
using Combat;
using Data.Abilities;
using Signals;
using SO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class UnitControlsScreen: MonoBehaviour
    {
        [SerializeField] private TMP_Text unitName;
        [SerializeField] private TMP_Text targetName;
        [SerializeField] private Button attackButton;
        [SerializeField] private Button passButton;
        [SerializeField] private Image avatarImage;
        [SerializeField] private List<AbilityItemUI> _abilityItemUis;

        [Inject] private readonly CombatManager _combatManager;
        [Inject] private readonly SignalBus _signalBus;

        private void OnEnable()
        {
            passButton.onClick.AddListener(PassTurn);
            attackButton.onClick.AddListener(Attack);
            foreach (var a in _abilityItemUis)
            {
                a.OnAbilityPressed += OnAbilityExecuted;
            }
        }

        private void OnDisable()
        {
            passButton.onClick.RemoveListener(PassTurn);
            attackButton.onClick.RemoveListener(Attack);
            foreach (var a in _abilityItemUis)
            {
                a.OnAbilityPressed -= OnAbilityExecuted;
            }
        }

        private void PassTurn()
        {
            _signalBus.Fire(new AdvanceNextTurnSignal());
        }
        
        private void Attack()
        {
            _combatManager.DealDamage();
        }

        public void SetUnitName(string unitNameText)
        {
            unitName.text = unitNameText;
        }
        public void SetTargetUnitName(string targetNameText)
        {
            targetName.text = $"Target: {targetNameText}";
        }

        public void SetAvatar(Sprite sprite)
        {
            avatarImage.sprite = sprite;
        }

        public void SetAbilities(List<AbilityRuntimeData> abilityDatas)
        {
            var amount = abilityDatas.Count;
            for (var i = 0; i < amount; i++)
            {
                _abilityItemUis[i].SetUp(abilityDatas[i].AbilityData);
                if (!abilityDatas[i].IsReady)
                {
                    _abilityItemUis[i].SetActiveStatus(false);
                }
            }
            for (var i = amount; i < _abilityItemUis.Count; i++)
            {
                _abilityItemUis[i].SetActiveStatus(false);
            }
        }
        
        private void OnAbilityExecuted(SOAbilityData obj)
        {
            _signalBus.Fire(new ProcessAbilitySignal(obj));
        }
    }
}