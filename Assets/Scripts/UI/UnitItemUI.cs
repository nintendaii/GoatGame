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
        [SerializeField] private TMP_Text damageInfoText;
        [SerializeField] private TMP_Text resistanceInfoText;

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
            SetDamageInfo();
            SetResistanceInfo();
        }

        private void SetResistanceInfo()
        {
            var s = "";
            s += $"ARM: {_unitEntityController.unitEntityData.CoreStats.Armor.Value}\n";
            s += $"FIR: {_unitEntityController.unitEntityData.CoreStats.Resistances.Fire.Value}\n";
            s += $"ICE: {_unitEntityController.unitEntityData.CoreStats.Resistances.Ice.Value}\n";
            s += $"DAR: {_unitEntityController.unitEntityData.CoreStats.Resistances.Dark.Value}\n";
            s += $"LIT: {_unitEntityController.unitEntityData.CoreStats.Resistances.Lightning.Value}\n";
            resistanceInfoText.text = s;
        }

        private void SetDamageInfo()
        {
            var s = "";
            s += $"PHY: {_unitEntityController.unitEntityData.CoreStats.PhysicalDamage.Value}";
            damageInfoText.text = s;
        }

        public void SetHealth(float value)
        {
            healthBarFillImage.fillAmount = Mathf.Clamp01(value / maxHealth);
            healthText.text = value.ToString();
        }
    }
}