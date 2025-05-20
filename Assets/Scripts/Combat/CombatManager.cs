using System;
using System.Collections;
using System.Collections.Generic;
using Abilities;
using Formulas;
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

        private UnitEntityController _currentTurnEntity;
        private UnitEntityController _currentTargetEntity;

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
            _currentTurnEntity = unit.UnitEntityController;
            _unitControlsScreen.SetUnitName(_currentTurnEntity.unitEntityData.Name);
            _unitControlsScreen.SetAvatar(_currentTurnEntity.unitAvatarSprite);
        }

        public void SetTarget(string id)
        {
            _currentTargetEntity = _unitsContainer.GetUnitById(id);
            _unitControlsScreen.SetTargetUnitName(_currentTargetEntity.unitEntityData.Name);
        }

        public void DealDamage()
        {
            if (_currentTargetEntity.unitEntityData.Id==_currentTurnEntity.unitEntityData.Id)
            {
                Debug.LogWarning("Cant attack self");
                return;
            }

            if (!_currentTargetEntity.IsAlive)
            {
                Debug.LogWarning($"{_currentTargetEntity.unitEntityData.Name} is dead");
                return;
            }

            var damage = DamageCalculator.CalculateAttackFinalDamage(_currentTurnEntity.unitEntityData,
                _currentTargetEntity.unitEntityData);
            _currentTargetEntity.DealDamage(damage);
            _unitItemsUIContainer.UpdateHealth(_currentTargetEntity.unitEntityData.CoreStats.Health.Value,_currentTargetEntity.unitEntityData.Id);
            if (!_currentTargetEntity.IsAlive)
            {
                _turnManager.RemoveUnitFromQueue(_currentTargetEntity);
            }
            ExecuteTurn();
        }
    }
}