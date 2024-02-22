namespace AFSInterview.Items
{
    using System;
    using UnityEngine;

    [Serializable]
    public class ItemAction
    {
        public enum ActionType
        {
            Sellable,
            Equipment,
            Consumable
        }
        [field: SerializeField] public ActionType Type { get; private set; }
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public int Value { get; private set; }
    }
}