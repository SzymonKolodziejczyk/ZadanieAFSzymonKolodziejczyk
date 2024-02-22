namespace AFSInterview.Items
{
	using System.Collections.Generic;
	using UnityEngine;

	public class InventoryController : MonoBehaviour
	{
		[SerializeField] private List<Item> items;
		[SerializeField] private int money;

		public int Sellable => money;
		public int Consumable => money;
		public int ItemsCount => items.Count;

		public void SellAllItemsUpToValue(int maxValue)
		{
			List<Item> itemsToRemove = new List<Item>();

			for (var i = items.Count - 1; i >= 0; i--)
			{
				var itemValue = items[i].Value;
				var itemType = items[i].action.Type;

				if (itemValue >= maxValue || itemType != ItemAction.ActionType.Sellable)
					continue;

				money += itemValue;
				itemsToRemove.Add(items[i]);
			}

			foreach (var itemToRemove in itemsToRemove)
			{
				items.Remove(itemToRemove);
			}
		}

		public void UseConsumable()
		{
			var random = UnityEngine.Random.Range(0, 2);
            if (random == 0)
            {
				money += 15;
				AddSellable(money);
				Debug.Log("Used consumable and added money. You now have " + ItemsCount + " items");
			}
            else
            {
				var randomItem = new Item("RustySword", 35);
				AddItem(randomItem);
				Debug.Log("Used consumable and added " + randomItem.Name + " with value of " + randomItem.Value + " and you now have " + ItemsCount + " items");
			}
        }

		public void AddSellable(int value)
		{
			money += value;
		}

		public void AddConsumable(int value)
		{
			UseConsumable();
		}

		public void AddItem(Item item)
		{
			items.Add(item);
		}
	}
}