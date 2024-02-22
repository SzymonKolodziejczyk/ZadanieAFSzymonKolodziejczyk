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
        public int CurrentHealthPoints { get; private set; }
        public int CurrentCooldown { get; private set; }
        public UnitData UnitData { get; private set; }

        public bool CanUseSkill => CurrentCooldown == 0;
        public bool IsAlive => CurrentHealthPoints > 0;

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

            CurrentHealthPoints = UnitData.unitStats.healthPoints;
            CurrentCooldown = 0;
        }

        public void StartTurn()
        {
            CurrentCooldown = Math.Max(CurrentCooldown - 1, 0);
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
            CurrentCooldown = UnitData.unitStats.attackInterval;

            Vector3 startPosition = transform.position;
            Vector3 targetPosition = target.transform.position;

            float distance = Vector3.Distance(startPosition, targetPosition);
            float t = (distance - 2) / distance;

            Vector3 destination = Vector3.LerpUnclamped(startPosition, targetPosition, t);
        }

        public void DealDamage(int damage)
        {
            CurrentHealthPoints -= damage;
            CurrentHealthPoints = Math.Max(CurrentHealthPoints, 0);
        }

        public void KillUnit()
        {
            Destroy(gameObject);
        }
    }
}
