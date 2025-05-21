using System.Collections.Generic;
using SO;
using Turn;
using Unit;
using UnityEngine;
using Zenject;

namespace Abilities
{
    public class AbilityCooldownSystem
    {
        [Inject] private readonly TurnManager _turnManager;
        private Dictionary<AbilityOwnerData, int> _abilitiesCooldownDict = new();

        public void SendAbilityOnCooldown(AbilityOwnerData data)
        {
            _abilitiesCooldownDict[data] = data.AbilityData.cooldown + _turnManager.CurrentTurn;
            Debug.Log($"Sending {data.AbilityData.name} to {data.AbilityData.cooldown + _turnManager.CurrentTurn}");
        }

        public void CheckAbilitiesCooldown(int turn)
        {
            //Potential issue Ensure that AbilityCooldownData implements proper Equals and GetHashCode if it's used as a dictionary key. Otherwise, removal might silently fail.
            var keysToRemove = new List<AbilityOwnerData>();
            Debug.Log($"Checking on turn {turn}");
            foreach (var kvp in _abilitiesCooldownDict)
            {
                if (kvp.Value <= turn)
                {
                    kvp.Key.UnitEntityController.CooldownAbility(kvp.Key.AbilityData);
                    keysToRemove.Add(kvp.Key);
                    Debug.Log($"{kvp.Key.AbilityData.name} ready");
                }
                else
                {
                    Debug.Log($"{kvp.Key.AbilityData.name} has {kvp.Value} cooldown");
                }
            }

            foreach (var key in keysToRemove)
            {
                _abilitiesCooldownDict.Remove(key);
            }
        }

        public bool CheckIfAbilityOnCooldown(AbilityOwnerData abilityOwnerData)
        {
            return _abilitiesCooldownDict.ContainsKey(abilityOwnerData);
        }
    }
}