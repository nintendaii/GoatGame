using Data.Stats;
using TMPro;
using UnityEngine;

namespace UI
{
    public class UnitTurnItem : MonoBehaviour
    {
        [SerializeField] private TMP_Text unitName;

        public UnitEntityData UnitEntityData;

        public void SetName(string value)
        {
            unitName.text = value;
        }
    }
    
}