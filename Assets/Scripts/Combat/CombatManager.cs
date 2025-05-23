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
        [Inject] private readonly StatusEffectSystem _statusEffectSystem;
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
    }
}