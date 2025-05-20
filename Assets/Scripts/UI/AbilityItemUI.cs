using System;
using SO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class AbilityItemUI: MonoBehaviour
    {
        [SerializeField] private TMP_Text abilityName;
        [SerializeField] private Button button;
        [SerializeField] private Image avatarImage;

        private SOAbilityData _abilityData;
        public event Action<SOAbilityData> OnAbilityPressed;

        private void OnEnable()
        {
            button.onClick.AddListener(ExecuteAbility);
        }

        private void OnDisable()
        {
            button.onClick.RemoveListener(ExecuteAbility);
        }

        private void ExecuteAbility()
        {
            OnAbilityPressed?.Invoke(_abilityData);
        }

        public void SetUp(SOAbilityData abilityData)
        {
            SetActiveStatus(true);
            _abilityData = abilityData;
            abilityName.text = abilityData.abilityName;
            avatarImage.sprite = abilityData.icon;
        }

        public void SetActiveStatus(bool isActive)
        {
            avatarImage.color = isActive ? Color.white : Color.gray;
            avatarImage.sprite = default;
            button.interactable = isActive;
            abilityName.text = string.Empty;
        }
    }
}