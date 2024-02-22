using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AFSInterview.Battle
{
    [CreateAssetMenu(fileName = nameof(BattleData), menuName = "Battle/" + nameof(BattleData), order = 1)]
    public class BattleData : ScriptableObject
    {
        [field: SerializeField] public List<UnitData> Player1Army { get; private set; }
        [field: SerializeField] public List<UnitData> Player2Army { get; private set; }
    }
}
