using System;
using System.Collections.Generic;
using UnityEngine;

namespace AFSInterview.Battle
{
    [Serializable]
    public class SupereffectiveDamage
    {
        [field: SerializeField] public AttributeType attributeType { get; set; }
        [field: SerializeField] public int attackDamage { get; set; }
    }
}
