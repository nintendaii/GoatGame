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
        public UnitEntityController unitEntityController;
        private float maxHealth;

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
            OnTargetSelected?.Invoke(unitEntityController.unitEntityData.Id);
        }

        public void Init(UnitEntityController unit)
        {
            unitEntityController = unit;
            maxHealth = unitEntityController.unitEntityData.CoreStats.Health.Value;
            unitName.text = unitEntityController.unitEntityData.Name;
            unitAvatarImage.sprite = unit.unitAvatarSprite;
            SetHealth(unit.unitEntityData.CoreStats.Health.Value);
            SetDamageInfo();
            SetResistanceInfo();
        }

        private void SetResistanceInfo()
        {
            var s = "";
            s += $"ARM: {unitEntityController.unitEntityData.CoreStats.Armor.Value}\n";
            s += $"FIR: {unitEntityController.unitEntityData.CoreStats.ElementalResistances.Fire.Value}\n";
            s += $"ICE: {unitEntityController.unitEntityData.CoreStats.ElementalResistances.Ice.Value}\n";
            s += $"DAR: {unitEntityController.unitEntityData.CoreStats.ElementalResistances.Dark.Value}\n";
            s += $"LIT: {unitEntityController.unitEntityData.CoreStats.ElementalResistances.Lightning.Value}\n";
            resistanceInfoText.text = s;
        }

        private void SetDamageInfo()
        {
            var s = "";
            s += $"PHY: {unitEntityController.unitEntityData.CoreStats.PhysicalDamage.Value + unitEntityController.unitEntityData.Equipment.Weapon.BaseDamage}\n";
            s += $"FIR: {unitEntityController.unitEntityData.CoreStats.ElementalDamage.Fire.Value + unitEntityController.unitEntityData.Equipment.Weapon.ElementalDamage.Fire.Value}\n";
            s += $"ICE: {unitEntityController.unitEntityData.CoreStats.ElementalDamage.Ice.Value + unitEntityController.unitEntityData.Equipment.Weapon.ElementalDamage.Ice.Value}\n";
            s += $"DAR: {unitEntityController.unitEntityData.CoreStats.ElementalDamage.Dark.Value + unitEntityController.unitEntityData.Equipment.Weapon.ElementalDamage.Dark.Value}\n";
            s += $"LIT: {unitEntityController.unitEntityData.CoreStats.ElementalDamage.Lightning.Value + unitEntityController.unitEntityData.Equipment.Weapon.ElementalDamage.Lightning.Value}\n";
            damageInfoText.text = s;
        }

        public void SetHealth(float value)
        {
            if (value<=0)
            {
                value = 0;
                unitAvatarImage.color = Color.red;
            }
            else
            {
                unitAvatarImage.color = Color.white;
            }
            healthBarFillImage.fillAmount = Mathf.Clamp01(value / maxHealth);
            healthText.text = value.ToString();
        }

        public void UpdateData()
        {
            SetHealth(unitEntityController.unitEntityData.CoreStats.Health.Value);
            SetDamageInfo();
            SetResistanceInfo();
        }
    }
}