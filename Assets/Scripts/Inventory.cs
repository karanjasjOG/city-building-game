using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory current;

    public float mill = 1;
    public float farm = 1;
    public float coffee_shop = 1;
    public float water_plant = 1;
    public float population = 1;
    public float wood = 1;
    public float water = 1;
    public float brick = 1;
    public float steel = 1;
    public float hemp = 1;
    public float joints = 1;
    public float coins = 1;
    public float gems = 1;


    public enum InventoryEnum
    {
        Mill,
        Farm,
        Coffee_Shop,
        Water_Plant,
        Population,
        Wood,
        Water,
        Brick,
        Steel,
        Hemp,
        Joints,
        Coins,
        Gems
    }

    private void Awake()
    {
        if (current != null)
            Destroy(this);
        else current = this;
    }

    public Dictionary<InventoryEnum, float> inventoryDict = new Dictionary<InventoryEnum, float>();
    private void Start()
    {
        inventoryDict.Add(InventoryEnum.Mill, mill);
        inventoryDict.Add(InventoryEnum.Farm, farm);
        inventoryDict.Add(InventoryEnum.Coffee_Shop, coffee_shop);
        inventoryDict.Add(InventoryEnum.Water_Plant, water_plant);
        inventoryDict.Add(InventoryEnum.Population, population);
        inventoryDict.Add(InventoryEnum.Wood, wood);
        inventoryDict.Add(InventoryEnum.Water, water);
        inventoryDict.Add(InventoryEnum.Brick, brick);
        inventoryDict.Add(InventoryEnum.Steel, steel);
        inventoryDict.Add(InventoryEnum.Hemp, hemp);
        inventoryDict.Add(InventoryEnum.Joints, joints);
        inventoryDict.Add(InventoryEnum.Coins, coins);
        inventoryDict.Add(InventoryEnum.Gems, gems);

    }

    public float getFromInventory(InventoryEnum key)
    {
        return inventoryDict[key];
    }

    public void addToInventory(InventoryEnum key,float value)
    {
        inventoryDict[key] += value;
    }

    public void deductFromInventory(InventoryEnum key, float value)
    {
        inventoryDict[key] -= value;
    }

}
