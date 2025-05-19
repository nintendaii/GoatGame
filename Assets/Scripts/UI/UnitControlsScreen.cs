using System;
using Combat;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class UnitControlsScreen: MonoBehaviour
    {
        [SerializeField] private TMP_Text unitName;
        [SerializeField] private TMP_Text targetName;
        [SerializeField] private Button attackButton;
        [SerializeField] private Button passButton;
        [SerializeField] private Image avatarImage;

        [Inject] private readonly CombatManager _combatManager;

        private void OnEnable()
        {
            passButton.onClick.AddListener(PassTurn);
        }

        private void OnDisable()
        {
            passButton.onClick.RemoveListener(PassTurn);
        }

        private void PassTurn()
        {
            _combatManager.ExecuteTurn();
        }

        public void SetUnitName(string unitNameText)
        {
            unitName.text = unitNameText;
        }
        public void SetTargetUnitName(string targetNameText)
        {
            targetName.text = $"Target: {targetNameText}";
        }

        public void SetAvatar(Sprite sprite)
        {
            avatarImage.sprite = sprite;
        }
    }
}