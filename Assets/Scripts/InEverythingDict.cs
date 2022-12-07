using System.Collections.Generic;
using static Inventory;

public class InventoryOp
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


    public Dictionary<InventoryEnum, bool> InInventoryOpDict = new Dictionary<InventoryEnum, bool>();


    public InventoryOp(bool mill = false,
        bool farm = false,
        bool coffee_shop = false,
        bool water_plant = false,
        bool population = false,
        bool wood=false,
        bool water = false,
        bool brick = false,
        bool steel = false,
        bool hemp = false,
        bool joints = false,
        bool coins = false,
        bool gems = false)
    {
        this.mill = mill;
        this.farm = farm;
        this.coffee_shop = coffee_shop;
        this.water_plant = water_plant;
        this.population = population;
        this.wood = wood;
        this.water = water;
        this.brick = brick;
        this.steel = steel;
        this.hemp = hemp;
        this.joints = joints;
        this.coins = coins;
        this.gems = gems;


        InInventoryOpDict.Add(InventoryEnum.Mill, mill);
        InInventoryOpDict.Add(InventoryEnum.Farm, farm);
        InInventoryOpDict.Add(InventoryEnum.Coffee_Shop, coffee_shop);
        InInventoryOpDict.Add(InventoryEnum.Water_Plant, water_plant);
        InInventoryOpDict.Add(InventoryEnum.Population, population);
        InInventoryOpDict.Add(InventoryEnum.Wood, wood);
        InInventoryOpDict.Add(InventoryEnum.Water, water);
        InInventoryOpDict.Add(InventoryEnum.Brick, brick);
        InInventoryOpDict.Add(InventoryEnum.Steel, steel);
        InInventoryOpDict.Add(InventoryEnum.Hemp, hemp);
        InInventoryOpDict.Add(InventoryEnum.Joints, joints);
        InInventoryOpDict.Add(InventoryEnum.Coins, coins);
        InInventoryOpDict.Add(InventoryEnum.Gems, gems);

    }
}