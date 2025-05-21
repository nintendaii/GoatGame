using System;
using System.Collections.Generic;
using Turn;
using UnityEngine;
using Zenject;

namespace Unit
{
    public class UnitsContainer: MonoBehaviour
    {
        public List<UnitEntityController> UnitEntityContainer = new();
        [Inject] private readonly TurnManager _turnManager;
        public void AddUnit(UnitEntityController unit)
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

        public List<UnitEntityController> GetAllyUnits()
        {
            var t = new List<UnitEntityController>();
            foreach (var u in UnitEntityContainer)
            {
                if (u.IsAlly)
                {
                    t.Add(u);
                }
            }

            return t;
        }
        
        public List<UnitEntityController> GetEnemyUnits()
        {
            var t = new List<UnitEntityController>();
            foreach (var u in UnitEntityContainer)
            {
                if (!u.IsAlly)
                {
                    t.Add(u);
                }
            }

            return t;
        }
    }
}