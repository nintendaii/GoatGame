using System.Collections.Generic;
using Turn;
using UnityEngine;
using Zenject;

namespace UI
{
    public class UnitsTurnOrderScreen : MonoBehaviour
    {
        public List<UnitTurnItem> UnitTurnItems;
        [Inject] private readonly TurnManager _turnManager;

        public void GenerateOrder()
        {
            var unitTurns = _turnManager.GenerateTurnOrder(UnitTurnItems.Count);
            for (var i = 0; i < UnitTurnItems.Count; i++)
            {
                var u = UnitTurnItems[i];
                u.SetName(unitTurns[i].UnitEntityController.unitEntityData.Name);
            }
        }
    }
}