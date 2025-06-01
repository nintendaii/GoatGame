using System;
using System.Collections.Generic;
using Data.General;
using Turn;
using UnityEngine;
using Zenject;

namespace Unit
{
    public class UnitsContainer: MonoBehaviour
    {
        public List<UnitEntityController> UnitEntityContainer = new();
        [Inject] private readonly TurnManager _turnManager;
        [Inject] private readonly DiContainer _diContainer;

        private void AddUnit(UnitEntityController unit)
        {
            UnitEntityContainer.Add(unit);
        }

        public UnitEntityController GetUnitById(string id)
        {
            return UnitEntityContainer.Find(x => x.unitEntityData.Id == id);
        }

        public void Init()
        {
            var units = FindObjectsByType<UnitEntityController>(FindObjectsSortMode.None);
            foreach (var u in units)
            {
                AddUnit(u);
            }
        }

        public UnitEntityController SpawnUnit(UnitEntityController unit)
        {
            var go = _diContainer.InstantiatePrefab(unit, transform);
            var component = go.GetComponent<UnitEntityController>();
            AddUnit(component);
            return component;
        }

        public List<UnitEntityController> GetAllyUnits(UnitTeam unitTeam)
        {
            var t = new List<UnitEntityController>();
            foreach (var u in UnitEntityContainer)
            {
                if (u.UnitTeam==unitTeam)
                {
                    t.Add(u);
                }
            }

            return t;
        }
        
        public List<UnitEntityController> GetEnemyUnits(UnitTeam unitTeam)
        {
            var t = new List<UnitEntityController>();
            foreach (var u in UnitEntityContainer)
            {
                if (u.UnitTeam!=unitTeam)
                {
                    t.Add(u);
                }
            }

            return t;
        }
    }
}