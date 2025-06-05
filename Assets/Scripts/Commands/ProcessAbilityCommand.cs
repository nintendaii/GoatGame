using System;
using System.Collections.Generic;
using System.Linq;
using Abilities;
using Combat;
using Data.Abilities;
using Signals;
using SO;
using Turn;
using UI;
using Unit;
using UnityEngine;
using Zenject;

namespace Commands
{
    public class ProcessAbilityCommand: ICommandWithParameters
    {
        [Inject] private readonly TurnManager _turnManager;
        [Inject] private readonly UnitsContainer _unitsContainer;
        [Inject] private readonly UnitsTurnOrderScreen _unitsTurnOrderScreen;
        [Inject] private readonly UnitControlsScreen _unitControlsScreen;
        [Inject] private readonly UnitItemsUIContainer _unitItemsUIContainer;
        [Inject] private readonly StatusEffectSystem _statusEffectSystem;
        [Inject] private readonly SignalBus _signalBus;
        [Inject] private readonly AbilityProcessorSystem _abilityProcessorSystem;
        [Inject] private readonly AbilityCooldownSystem _abilityCooldownSystem;
        [Inject] private readonly CombatManager _combatManager;

        private UnitEntityController currentTargetEntity;
        private UnitEntityController currentTurnEntity;
        
        public void Execute(ISignal signal)
        {
            var param = (ProcessAbilitySignal)signal;
            currentTargetEntity = _combatManager.currentTargetEntity;
            currentTurnEntity = _combatManager.currentTurnEntity;
            
            if (_statusEffectSystem.CheckIfUnitHasStatusEffect(currentTurnEntity, AbilityEffect.Silence))
            {
                Debug.LogWarning($"{currentTurnEntity.name} cant use abilities due to Silence");
                return;
            }
            var soAbilityData = param.AbilityData;
            var targets = new List<UnitEntityController>();
            if (ValidateAbilityCooldown(soAbilityData))
            {
                switch (soAbilityData.abilityType)
                {
                    case AbilityType.None:
                        break;
                    case AbilityType.Passive:
                        targets.Add(param.Source);
                        break;
                    case AbilityType.Target:
                        if (ValidateSpellImmunity(currentTargetEntity))
                        {
                            Debug.LogWarning($"{currentTargetEntity.name} has spell immunity so cant use {soAbilityData.abilityName}");
                            return;
                        }

                        targets.Add(currentTargetEntity);
                        break;
                    case AbilityType.Aura:
                        var validatedTargetAura = ValidateTargets(param.AbilityData.targetType, param.Source);
                        if (validatedTargetAura.Count==0 && param.AbilityData.targetType!=AbilityTarget.None)
                        {
                            Debug.Log("ValidateTargets list is empty");
                            return;
                        }
                        foreach (var t in validatedTargetAura)
                        {
                            if (!ValidateSpellImmunity(t))
                            {
                                targets.Add(t);
                            }
                        }
                        break;
                    case AbilityType.NoTarget:
                        var validatedTargets = ValidateTargets(param.AbilityData.targetType, currentTurnEntity);
                        if (validatedTargets.Count==0 && param.AbilityData.targetType!=AbilityTarget.None)
                        {
                            Debug.Log("ValidateTargets list is empty");
                            return;
                        }
                        foreach (var t in validatedTargets)
                        {
                            if (!ValidateSpellImmunity(t))
                            {
                                targets.Add(t);
                            }
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            else
            {
                Debug.Log("Cant use ability");
                return;
            }

            var effectProcessorData = new EffectProcessorData
            {
                Ability = param.AbilityData,
                Source = currentTurnEntity,
                Targets = targets
            };
            _abilityProcessorSystem.UseAbility(effectProcessorData);
            //Might need to replace later since _abilityProcessorSystem.UseAbility can fail
            if (soAbilityData.abilityType is AbilityType.Passive or AbilityType.Aura)
            {
                return;
            }
            _abilityCooldownSystem.SendAbilityOnCooldown(new AbilityOwnerData
            {
                AbilityData = param.AbilityData,
                UnitEntityController = currentTurnEntity
            });
        }

        private bool ValidateAbilityCooldown(SOAbilityData soAbilityData)
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
        
        private bool ValidateSpellImmunity(UnitEntityController unit)
        {
            var s = _statusEffectSystem.GetUnitStatusEffects(unit);
            return s?.Find(x=>x.StatusEffectApplied.effectType==AbilityEffect.SpellImmunity) != null;
        }

        private List<UnitEntityController> ValidateTargets(AbilityTarget target, UnitEntityController source)
        {
            var unitTeam = source.UnitTeam;
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
                    t =  _unitsContainer.GetAllyUnits(unitTeam).ToList();
                    break;
                case AbilityTarget.Enemies:
                    t =  _unitsContainer.GetEnemyUnits(unitTeam).ToList();
                    break;
                case AbilityTarget.EveryoneInclusive:
                    t = _unitsContainer.UnitEntityContainer.ToList();
                    break;
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