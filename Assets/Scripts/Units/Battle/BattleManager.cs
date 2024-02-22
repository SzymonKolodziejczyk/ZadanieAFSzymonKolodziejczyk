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
        }

        private void SpawnArmy(List<UnitData> armyList, Transform parent, PlayerNumber playername)
        {
            for (int i = 0; i < armyList.Count; i++)
            {
                UnitData unitData = armyList[i];

                Vector3 position = parent.position + Vector3.forward * 2.5f * i;

                Unit unit = Instantiate(unitData.unitPrefab, position, new Quaternion(), parent);
                unit.Initialize(unitData, playername);

                AllUnits.Add(unit);
            }
        }
    }
}
