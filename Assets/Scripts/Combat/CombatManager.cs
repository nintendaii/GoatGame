using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Abilities;
using Data.Abilities;
using Formulas;
using Signals;
using SO;
using Turn;
using UI;
using Unit;
using UnityEngine;
using Zenject;

namespace Combat
{
    public class CombatManager: MonoBehaviour
    {
        [Inject] private readonly TurnManager _turnManager;
        [Inject] private readonly UnitsContainer _unitsContainer;
        [Inject] private readonly UnitsTurnOrderScreen _unitsTurnOrderScreen;
        [Inject] private readonly UnitControlsScreen _unitControlsScreen;
        [Inject] private readonly UnitItemsUIContainer _unitItemsUIContainer;
        [Inject] private readonly AbilityProcessorSystem _abilityProcessorSystem;
        [Inject] private readonly AbilityCooldownSystem _abilityCooldownSystem;
        [Inject] private readonly SignalBus _signalBus;

        public UnitEntityController currentTargetEntity;
        public UnitEntityController currentTurnEntity;

        private void Start()
        {
            StartCoroutine(CallInTwoSeconds(.5f));
        }

        private IEnumerator CallInTwoSeconds(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            StartBattle();
        }

        public void StartBattle()
        {
            Debug.Log("Battle started");
            _unitsContainer.Init();
            _turnManager.Init(_unitsContainer.UnitEntityContainer);
            _unitItemsUIContainer.Init(_unitsContainer.UnitEntityContainer);
            _signalBus.Fire(new AdvanceNextTurnSignal());
        }

        public void AddUnit(SOUnitData unitData)
        {
            //TODO handle this for summoning in runtime
        }

        public void SetTarget(string id)
        {
            currentTargetEntity = _unitsContainer.GetUnitById(id);
            _unitControlsScreen.SetTargetUnitName(currentTargetEntity.unitEntityData.Name);
        }

        public void DealDamage()
        {
            _signalBus.Fire(new DealAttackDamageUnitSignal(currentTurnEntity, new List<UnitEntityController>{currentTargetEntity}));
        }

        private bool ValidateAbility(SOAbilityData soAbilityData)
        {
            if (_abilityCooldownSystem.CheckIfAbilityOnCooldown(new AbilityOwnerData{ UnitEntityController = currentTurnEntity, AbilityData = soAbilityData}))
            {
                Debug.Log($"Ability {soAbilityData.name} on cooldown");
                return false;
            }
            if (soAbilityData.abilityType == AbilityType.Target)
            {
                if (currentTargetEntity!=null)
                {
                    return true;
                }

                return false;
            }

            return true;
        }

        public void UseAbility(SOAbilityData soAbilityData)
        {
            
            if (ValidateAbility(soAbilityData))
            {
                switch (soAbilityData.abilityType)
                {
                    case AbilityType.None:
                        break;
                    case AbilityType.Passive:
                        Debug.Log("ability is passive");
                        break;
                    case AbilityType.Aura:
                        Debug.Log("ability is aura");
                        break;
                    case AbilityType.Target:
                        currentTurnEntity.UseAbility(soAbilityData, new List<UnitEntityController>{currentTargetEntity});
                        break;
                    case AbilityType.NoTarget:
                        currentTurnEntity.UseAbility(soAbilityData, ValidateTargets(soAbilityData.targetType));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            else
            {
                Debug.Log("Cant use ability");
            }
        }

        private List<UnitEntityController> ValidateTargets(AbilityTarget target)
        {
            var unitTeam = currentTurnEntity.UnitTeam;
            var t = new List<UnitEntityController>();
            switch (target)
            {
                case AbilityTarget.None:
                    break;
                case AbilityTarget.Self:
                    t.Add(currentTurnEntity);
                    break;
                case AbilityTarget.Unit:
                    t.Add(currentTargetEntity);
                    break;
                case AbilityTarget.Allies:
                    return _unitsContainer.GetAllyUnits(unitTeam);
                case AbilityTarget.Enemies:
                    return _unitsContainer.GetEnemyUnits(unitTeam);
                case AbilityTarget.EveryoneInclusive:
                    return _unitsContainer.UnitEntityContainer;
                case AbilityTarget.EveryoneExclusive:
                    var l = _unitsContainer.UnitEntityContainer.ToList();
                    l.Remove(currentTurnEntity);
                    return l;
                case AbilityTarget.Custom:
                    //TODO implement if needed custom targets, like selection of multiple units
                default:
                    throw new ArgumentOutOfRangeException(nameof(target), target, null);
            }

            return t;
        }
    }
}