namespace AFSInterview.Items
{
    using System;

    public class ItemActionResolver
    {
        private InventoryController inventoryController;

        public ItemActionResolver(InventoryController inventoryController)
        {
            this.inventoryController = inventoryController;
        }

        public void Resolve(ItemAction action)
        {
            switch (action.Type)
            {
                case ItemAction.ActionType.Sellable:
                    inventoryController.AddSellable(action.Value);
                    break;
                case ItemAction.ActionType.Equipment:
                    inventoryController.AddItem(new Item(action.Name, action.Value));
                    break;
                case ItemAction.ActionType.Consumable:
                    inventoryController.AddConsumable(action.Value);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(action), action.Type, "Unknown action type");
            }
        }
    }
}