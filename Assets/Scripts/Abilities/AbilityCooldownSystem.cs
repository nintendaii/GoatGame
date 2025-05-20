using System.Collections.Generic;
using SO;
using Turn;
using Unit;
using Zenject;

namespace Abilities
{
    public class AbilityCooldownSystem
    {
        [Inject] private readonly TurnManager _turnManager;
        private Dictionary<AbilityOwnerData, int> _abilitiesCooldownDict = new();

        public void SendAbilityOnCooldown(AbilityOwnerData data)
        {
            _abilitiesCooldownDict[data] = data.AbilityData.cooldown + _turnManager.currentTurn;
        }

        public void CheckAbilitiesCooldown(int turn)
        {
            //Potential issue Ensure that AbilityCooldownData implements proper Equals and GetHashCode if it's used as a dictionary key. Otherwise, removal might silently fail.
            var keysToRemove = new List<AbilityOwnerData>();

            foreach (var kvp in _abilitiesCooldownDict)
            {
                if (kvp.Value >= turn)
                {
                    kvp.Key.UnitEntityController.CooldownAbility(kvp.Key.AbilityData);
                    keysToRemove.Add(kvp.Key);
                }
            }

            foreach (var key in keysToRemove)
            {
                _abilitiesCooldownDict.Remove(key);
            }
        }

    }
}