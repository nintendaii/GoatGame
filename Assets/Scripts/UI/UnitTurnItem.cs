using Data.Stats;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UnitTurnItem : MonoBehaviour
    {
        [SerializeField] private TMP_Text unitName;
        [SerializeField] private Image avatar;

        public UnitEntityData UnitEntityData;

        public void SetName(string value)
        {
            unitName.text = value;
        }

        public void SetSprite(Sprite sprite)
        {
            avatar.sprite = sprite;
        }
    }
    
}