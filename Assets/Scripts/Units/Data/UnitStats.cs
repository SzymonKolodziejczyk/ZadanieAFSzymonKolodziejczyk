using System;
using System.Collections.Generic;
using UnityEngine;

namespace AFSInterview.Battle
{
    [Serializable]
    public class UnitStats
    {
        [field: SerializeField] public int healthPoints { get; set; }
        [field: SerializeField] public int armorPoints { get; set; }
        [field: SerializeField] public int attackInterval { get; set; }
        [field: SerializeField] public int attackDamage { get; set; }
    }
}