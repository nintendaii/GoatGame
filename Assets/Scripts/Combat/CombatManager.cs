using System;
using System.Collections;
using System.Collections.Generic;
using Abilities;
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
            ExecuteTurn();
        }

        public void AddUnit(SOUnitData unitData)
        {
            //TODO handle this for summoning in runtime
        }

        public void ExecuteTurn()
        {
            _unitsTurnOrderScreen.GenerateOrder();
            var unit = _turnManager.GetNextUnit();
            currentTurnEntity = unit.UnitEntityController;
            _unitControlsScreen.SetUnitName(currentTurnEntity.unitEntityData.Name);
            _unitControlsScreen.SetAvatar(currentTurnEntity.unitAvatarSprite);
            _unitControlsScreen.SetAbilities(currentTurnEntity.AbilitiesRuntime);
            _signalBus.Fire(new AdvanceNextTurnSignal());
        }

        public void SetTarget(string id)
        {
            currentTargetEntity = _unitsContainer.GetUnitById(id);
            _unitControlsScreen.SetTargetUnitName(currentTargetEntity.unitEntityData.Name);
        }

        public void DealDamage()
        {
            if (currentTargetEntity.unitEntityData.Id==currentTurnEntity.unitEntityData.Id)
            {
                Debug.LogWarning("Cant attack self");
                return;
            }

            if (!currentTargetEntity.IsAlive)
            {
                Debug.LogWarning($"{currentTargetEntity.unitEntityData.Name} is dead");
                return;
            }

            var damage = DamageCalculator.CalculateAttackFinalDamage(currentTurnEntity.unitEntityData,
                currentTargetEntity.unitEntityData);
            currentTargetEntity.DealDamage(damage);
            _unitItemsUIContainer.UpdateHealth(currentTargetEntity.unitEntityData.CoreStats.Health.Value,currentTargetEntity.unitEntityData.Id);
            if (!currentTargetEntity.IsAlive)
            {
                _turnManager.RemoveUnitFromQueue(currentTargetEntity);
            }
            ExecuteTurn();
        }
    }
}