using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AFSInterview.Battle
{
    [CreateAssetMenu(fileName = nameof(UnitData), menuName = "Battle/" + nameof(UnitData), order = 1)]
    public class UnitData : ScriptableObject
    {
        [field: SerializeField] public string unitName { get; private set; }
        [field: SerializeField] public UnitType unitType { get; private set; }
        [field: SerializeField] public List<AttributeType> attributeTypes { get; private set; } = new List<AttributeType>();
        [field: SerializeField] public UnitStats unitStats { get; private set; }

        public bool HasAttribute(AttributeType attributeType)
        {
            return attributeTypes.Contains(attributeType);
        }
    }
}