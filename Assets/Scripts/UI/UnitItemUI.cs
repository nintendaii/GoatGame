using System;
using TMPro;
using Unit;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UnitItemUI: MonoBehaviour
    {
        [SerializeField] private Image healthBarFillImage;
        [SerializeField] private Image unitAvatarImage;
        [SerializeField] private Button targetButton;
        [SerializeField] private TMP_Text unitName;
        [SerializeField] private TMP_Text healthText;

        public event Action<string> OnTargetSelected; 
        private float maxHealth;
        private UnitEntityController _unitEntityController;

        private void OnEnable()
        {
            targetButton.onClick.AddListener(SetTarget);
        }

        private void OnDisable()
        {
            targetButton.onClick.RemoveListener(SetTarget);
        }

        private void SetTarget()
        {
            OnTargetSelected?.Invoke(_unitEntityController.unitEntityData.Id);
        }

        public void Init(UnitEntityController unit)
        {
            _unitEntityController = unit;
            maxHealth = _unitEntityController.unitEntityData.CoreStats.Health.Value;
            unitName.text = _unitEntityController.unitEntityData.Name;
            unitAvatarImage.sprite = unit.UnitAvatarSprite;
            SetHealth(unit.unitEntityData.CoreStats.Health.Value);
        }
        
        public void SetHealth(float value)
        {
            healthBarFillImage.fillAmount = Mathf.Clamp01(value / maxHealth);
            healthText.text = value.ToString();
        }
    }
}