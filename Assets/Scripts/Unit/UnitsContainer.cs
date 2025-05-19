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

        private void Start()
        {
            var units = FindObjectsByType<UnitEntityController>(FindObjectsSortMode.None);
            foreach (var u in units)
            {
                AddUnit(u);
            }
            _turnManager.Init(UnitEntityContainer);
        }
    }
}