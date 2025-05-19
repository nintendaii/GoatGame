using System.Collections.Generic;
using Combat;
using Unit;
using UnityEngine;
using Zenject;

namespace UI
{
    public class UnitItemsUIContainer: MonoBehaviour
    {
        public List<UnitItemUI> UnitItemUis;

        [Inject] private readonly CombatManager _combatManager;

        public void Init(List<UnitEntityController> units)
        {
            for (var index = 0; index < units.Count; index++)
            {
                var unitItemUi = UnitItemUis[index];
                unitItemUi.Init(units[index]);
                unitItemUi.OnTargetSelected += OnTargetSelected;
            }
        }

        private void OnTargetSelected(string obj)
        {
            _combatManager.SetTarget(obj);
        }
    }
}