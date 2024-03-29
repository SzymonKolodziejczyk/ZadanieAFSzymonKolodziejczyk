﻿namespace AFSInterview.Items
{
    using TMPro;
    using UnityEngine;

    public class ItemsManager : MonoBehaviour
    {
        [SerializeField] private InventoryController inventoryController;

        [Header("Items")]

        [Range(0.0F, 1000.0F)]
        [SerializeField] private int itemSellMaxValue;
        [SerializeField] private Transform itemSpawnParent;
        [SerializeField] private GameObject[] itemPrefabs;
        [SerializeField] private BoxCollider itemSpawnArea;
        [Range(0.0F, 60.0F)]
        [SerializeField] private float itemSpawnInterval;
        [SerializeField] private TextMeshProUGUI moneyText;

        [Header("UI elements")]
        
        private Camera cameraMain;
        private int layerMask;
        private ItemActionResolver itemActionResolver;

        private void Start()
        {
            cameraMain = Camera.main;
            layerMask = LayerMask.GetMask("Item");
            itemActionResolver = new ItemActionResolver(inventoryController);

            UpdateMoneyText();

            InvokeRepeating(nameof(SpawnNewItem), 0f, itemSpawnInterval);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
                TryPickUpItem();

            if (Input.GetKeyDown(KeyCode.E))
                inventoryController.UseConsumable(itemSellMaxValue);
                UpdateMoneyText();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                inventoryController.SellAllItemsUpToValue(itemSellMaxValue);
                UpdateMoneyText();
            }
        }

        private void UpdateMoneyText()
        {
            moneyText.text = "Money: " + inventoryController.Sellable;
        }

        private void SpawnNewItem()
        {
            var spawnAreaBounds = itemSpawnArea.bounds;
            var position = new Vector3(
                Random.Range(spawnAreaBounds.min.x, spawnAreaBounds.max.x),
                0f,
                Random.Range(spawnAreaBounds.min.z, spawnAreaBounds.max.z)
            );

            // Could be optimized by using object pooling if necessary
            Instantiate(itemPrefabs[Random.Range(0, itemPrefabs.Length)], position, Quaternion.identity, itemSpawnParent);
        }

        private void TryPickUpItem()
        {
            if (TryGetItemHolderOnPosition(Input.mousePosition, out IItemHolder itemHolder))
            {
                var item = itemHolder.GetItem(true);
                inventoryController.AddItem(item);
                Debug.Log("Picked up " + item.Name + " with value of " + item.Value + " and now have " + inventoryController.ItemsCount + " items");
            }
        }

        /*private void TryUseItem()
		{
			if (TryGetItemHolderOnPosition(Input.mousePosition, out IItemHolder itemHolder))
			{
				var item = itemHolder.GetItem(true);

				ItemAction action = item.Use();
				itemActionResolver.Resolve(action);
				UpdateMoneyText();
				Debug.Log("Used " + item.Name + " item and now have " + inventoryController.Consumable + " money and " + inventoryController.ItemsCount + " items");
			}
		}*/

        private bool TryGetItemHolderOnPosition(Vector3 position, out IItemHolder itemHolder)
        {
            itemHolder = null;

            var ray = cameraMain.ScreenPointToRay(position);
            if (!Physics.Raycast(ray, out var hit, 100f, layerMask) || !hit.transform.gameObject.TryGetComponent<IItemHolder>(out itemHolder))
                return false;

            return true;
        }
    }
}