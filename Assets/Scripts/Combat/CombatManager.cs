using System;
using Turn;
using Unit;
using UnityEngine;
using Zenject;

namespace Combat
{
    public class CombatManager: MonoBehaviour
    {
        [Inject] private readonly TurnManager _turnManager;
        [Inject] private readonly UnitsContainer unitsContainer;
        private void Start()
        {
            var unitsOrder = _turnManager.GenerateTurnOrder(10);
            var s = "";
            for (var i = 0; i < unitsOrder.Count; i++)
            {
                var u = unitsOrder[i];
                s += $"Turn {i + 1}: {unitsContainer.GetUnitById(u.Id).unitEntityData.Name}\n";
            }

            Debug.Log(s);
        }
    }
}