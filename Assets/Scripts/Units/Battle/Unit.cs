using System;
using UnityEngine;

namespace AFSInterview.Battle
{
    public class Unit : MonoBehaviour
    {
        public bool HadTurn { get; private set; }
        public PlayerNumber PlayerNumber { get; private set; }
        public int CurrentHealthPoints { get; private set; }
        public int CurrentCooldown { get; private set; }
        public UnitData UnitData { get; private set; }

        public bool CanUseSkill => CurrentCooldown == 0;
        public bool IsAlive => CurrentHealthPoints > 0;

        public void Initialize(UnitData unitData, PlayerNumber playername)
        {
            UnitData = unitData;
            HadTurn = false;
            PlayerNumber = playername;

            CurrentHealthPoints = UnitData.unitStats.healthPoints;
            CurrentCooldown = 0;
        }
    }
}
