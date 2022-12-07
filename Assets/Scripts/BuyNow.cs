using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Inventory;

public class BuyNow : MonoBehaviour
{
    

    public bool mill = false;
    public bool farm = false;
    public bool coffee_shop = false;
    public bool water_plant = false;
    public bool population = false;
    public bool wood = false;
    public bool water = false;
    public bool brick = false;
    public bool steel = false;
    public bool hemp = false;
    public bool joints = false;
    public bool coins = false;
    public bool gems = false;


    InventoryOp inventoryOp;

    private void Awake()
    {
        inventoryOp = new InventoryOp(mill: mill, farm: farm, coffee_shop: coffee_shop, water_plant: water_plant, population: population, wood: wood, water: water, brick: brick
            , steel: steel, hemp: hemp, joints: joints, coins: coins, gems: gems);
    }

    public void AddInInventory()
    {
        
        foreach (var item in inventoryOp.InInventoryOpDict)
        {
            if (item.Value)
            {
                Inventory.current.addToInventory(item.Key, 1);
            }
        }
    }
}
