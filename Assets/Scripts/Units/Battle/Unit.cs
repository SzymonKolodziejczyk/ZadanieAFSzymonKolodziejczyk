using System;
using UnityEngine;

namespace AFSInterview.Battle
{
    public class Unit : MonoBehaviour
    {
        public event Action<Unit> OnUnitHoverStarted;
        public event Action<Unit> OnUnitHoverEnded;
        public event Action<Unit> OnUnitClicked;

        public bool HadTurn { get; private set; }
        public PlayerNumber PlayerNumber { get; private set; }
        public int currentHealthPoints { get; private set; }
        public int currentCooldown { get; private set; }
        public UnitData UnitData { get; private set; }

        public bool CanUseSkill => currentCooldown == 0;
        public bool IsAlive => currentHealthPoints > 0;

        private void OnMouseEnter()
        {
            OnUnitHoverStarted?.Invoke(this);
        }

        private void OnMouseExit()
        {
            OnUnitHoverEnded?.Invoke(this);
        }

        private void OnMouseDown()
        {
            OnUnitClicked?.Invoke(this);
        }

        public void Initialize(UnitData unitData, PlayerNumber playername)
        {
            UnitData = unitData;
            HadTurn = false;
            PlayerNumber = playername;

            currentHealthPoints = UnitData.unitStats.healthPoints;
            currentCooldown = 0;
        }

        public void StartTurn()
        {
            currentCooldown = Math.Max(currentCooldown - 1, 0);
        }

        public void SkipTurn()
        {
            HadTurn = true;
        }

        public void RefreshTurn()
        {
            HadTurn = false;
        }

        public void AttackUnit(Unit target, Action onDealDamage)
        {
            HadTurn = true;
            currentCooldown = UnitData.unitStats.attackInterval;

            Vector3 startPosition = transform.position;
            Vector3 targetPosition = target.transform.position;

            float distance = Vector3.Distance(startPosition, targetPosition);
            float t = (distance - 2) / distance;

            Vector3 destination = Vector3.LerpUnclamped(startPosition, targetPosition, t);

            OnDealDamage();

            void OnDealDamage()
            {
                onDealDamage?.Invoke();
            }
        }

        public void DealDamage(int damage)
        {
            currentHealthPoints -= damage;
            currentHealthPoints = Math.Max(currentHealthPoints, 0);
        }

        public void KillUnit()
        {
            Destroy(gameObject);
        }
        public string GetDescription(int damage)
        {
            string description = string.Empty;

            description += UnitData.unitName + "\n";
            description += $"HP: {currentHealthPoints}";
            if (damage > 0)
            {
                description += $"<color=red>-{damage}</color>";
            }

            description += $"/{UnitData.unitStats.healthPoints}\n";
            description += $"Damage: {UnitData.unitStats.attackDamage}\n";
            description += $"Armor: {UnitData.unitStats.armorPoints}\n";
            description += $"Cooldown: {UnitData.unitStats.attackInterval}({currentCooldown})\n";
            if (UnitData.attributeTypes.Count > 0)
            {
                description += "Attributes: ";
                for (int i = 0; i < UnitData.attributeTypes.Count; i++)
                {
                    description += $"{UnitData.attributeTypes[i]}";

                    if (i < UnitData.attributeTypes.Count - 1)
                    {
                        description += ", ";
                    }
                }
                description += "\n";
            }

            if (UnitData.unitStats.supereffectiveDamage.Count > 0)
            {
                description += "Bonus damage:\n";
                for (int i = 0; i < UnitData.unitStats.supereffectiveDamage.Count; i++)
                {
                    description += $"{UnitData.unitStats.supereffectiveDamage[i].attributeType}: {UnitData.unitStats.supereffectiveDamage[i].attackDamage:+#;-#;0}\n";
                }
            }

            return description;
        }
    }
}