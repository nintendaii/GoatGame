using System.Collections.Generic;
using Data.Turn;
using Turn;
using UnityEngine;
using Zenject;

namespace UI
{
    public class UnitsTurnOrderScreen : MonoBehaviour
    {
        public List<UnitTurnItem> UnitTurnItems;
        public UnitTurnItem currentTurnItem;
        [Inject] private readonly TurnManager _turnManager;

        public void GenerateOrder()
        {
            var unitTurns = _turnManager.GenerateTurnOrder(UnitTurnItems.Count);
            for (var i = 0; i < UnitTurnItems.Count; i++)
            {
                var u = UnitTurnItems[i];
                u.SetName(unitTurns[i].UnitEntityController.unitEntityData.Name);
                u.SetSprite(unitTurns[i].UnitEntityController.unitAvatarSprite);
            }
        }

        public void SetCurrentTurnItem(TurnUnitData unit)
        {
            currentTurnItem.SetName(unit.UnitEntityController.unitEntityData.Name);
            currentTurnItem.SetSprite(unit.UnitEntityController.unitAvatarSprite);
        }
    }
}