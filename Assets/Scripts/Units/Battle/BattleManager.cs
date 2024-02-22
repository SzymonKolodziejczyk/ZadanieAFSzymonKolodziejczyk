using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AFSInterview.Battle
{
    public class BattleManager : MonoBehaviour
    {
        [field: SerializeField] private BattleData BattleData { get; set; }
        [field: SerializeField] private Transform Player1ArmyTransform { get; set; }
        [field: SerializeField] private Transform Player2ArmyTransform { get; set; }
        [field: SerializeField] private UnitIndicator UnitIndicator { get; set; }

        [field: Header("UI")]
        [field: SerializeField] public Tooltip Tooltip { get; set; }
        private List<Unit> AllUnits { get; set; } = new List<Unit>();

        private Unit currentUnit { get; set; }
        private bool isCombat { get; set; }

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            AllUnits.Clear();
            SpawnArmy(BattleData.Player1Army, Player1ArmyTransform, PlayerNumber.Player1);
            SpawnArmy(BattleData.Player2Army, Player2ArmyTransform, PlayerNumber.Player2);

            AllUnits = AllUnits.OrderBy(x => Random.value).ToList();
            isCombat = true;

            if (AllUnits.Count == 0)
            {
                Debug.LogWarning($"No units in armies");
                return;
            }

            UnitIndicator.Hide();

            CheckConditions();
            StartNextTurn();
        }

        private void SpawnArmy(List<UnitData> armyList, Transform parent, PlayerNumber playername)
        {
            for (int i = 0; i < armyList.Count; i++)
            {
                UnitData unitData = armyList[i];

                Vector3 position = parent.position + Vector3.forward * 2.5f * i;

                Unit unit = Instantiate(unitData.unitPrefab, position, new Quaternion(), parent);
                unit.Initialize(unitData, playername);
                unit.OnUnitClicked += Unit_OnUnitClicked;

                unit.OnUnitHoverStarted += Unit_OnUnitHoverStarted;
                unit.OnUnitHoverEnded += Unit_OnUnitHoverEnded;

                AllUnits.Add(unit);
            }
        }
        
        private void StartNextTurn()
        {
            if (!isCombat)
            {
                return;
            }

            currentUnit = AllUnits.FirstOrDefault(x => x.HadTurn == false);

            if (!currentUnit)
            {
                AllUnits.ForEach(x => x.RefreshTurn());
                StartNextTurn();
                return;
            }

            currentUnit.StartTurn();

            if (!currentUnit.CanUseSkill)
            {
                currentUnit.SkipTurn();
                StartNextTurn();
                return;
            }

            UnitIndicator.Show(currentUnit.transform.position);
        }

        private void CheckConditions()
        {
            List<PlayerNumber> currentPlayers = new List<PlayerNumber>();

            for (int i = 0; i < AllUnits.Count; i++)
            {
                Unit unit = AllUnits[i];
                if (!currentPlayers.Contains(unit.PlayerNumber))
                {
                    currentPlayers.Add(unit.PlayerNumber);
                }
            }

            if (currentPlayers.Count <= 1)
            {
                isCombat = false;
                Debug.Log($"Battle Ended");
            }
        }

        private void Unit_OnUnitHoverEnded(Unit unit)
        {
            Tooltip.HideText();
        }

        private void Unit_OnUnitHoverStarted(Unit unit)
        {
            int damage = 0;

            if (currentUnit.PlayerNumber != unit.PlayerNumber)
            {
                damage = BattleRules.GetResultDamage(currentUnit.UnitData, unit.UnitData);
            }
            Tooltip.ShowTooltip(unit.transform.position, unit.GetDescription(damage));
        }

        private void Unit_OnUnitClicked(Unit target)
        {
            if (currentUnit == null || currentUnit.HadTurn)
            {
                return;
            }

            if (currentUnit.PlayerNumber == target.PlayerNumber)
            {
                return;
            }

            UnitIndicator.Hide();
            Tooltip.HideText();

            int damage = BattleRules.GetResultDamage(currentUnit.UnitData, target.UnitData);
            currentUnit.AttackUnit(target, DealDamage);

            void DealDamage()
            {
                target.DealDamage(damage);

                if (!target.IsAlive)
                {
                    AllUnits.Remove(target);
                    target.KillUnit();
                    CheckConditions();
                }
            }
            StartNextTurn();
        }
    }
}
