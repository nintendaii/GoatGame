using System;
using Data.Stats;
using Data.Turn;
using UnityEngine;

namespace Unit
{
    public class UnitEntityController : MonoBehaviour
    {
        [SerializeField] public UnitEntityData unitEntityData;
        public Sprite UnitAvatarSprite;

        public bool IsAlive => unitEntityData.CoreStats.Health.Value <= 0;

        private void Awake()
        {
            unitEntityData.Id = Guid.NewGuid().ToString();
        }
    }
}