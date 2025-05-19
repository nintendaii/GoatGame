using System;
using Data.Stats;
using SO;
using Unity.VisualScripting;
using UnityEngine;

namespace Unit
{
    public class UnitEntityController : MonoBehaviour
    {
        [NonSerialized] public UnitEntityData unitEntityData;
        [NonSerialized] public Sprite unitAvatarSprite;
        public SOUnitData UnitData;

        public bool IsAlive => unitEntityData.CoreStats.Health.Value >= 0;

        private void Awake()
        {
            unitEntityData = UnitData.Clone();
            unitAvatarSprite = UnitData.unitAvatarSprite;
        }

        public void DealDamage(float damage)
        {
            unitEntityData.CoreStats.Health.Value -= damage;
        }
    }
}