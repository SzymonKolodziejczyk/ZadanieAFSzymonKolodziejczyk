using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AFSInterview
{
    [CreateAssetMenu]
    public class Unit : ScriptableObject
    {
        [SerializeField] public enum Attributes { Light, Armored, Mechanical };

        [SerializeField] private string unitName;
        [SerializeField] private Attributes unitAttribute;
        [SerializeField] public int armorPoints;
        [SerializeField] public int attackInterval;
        [SerializeField] public int attackDamage;
    }
}