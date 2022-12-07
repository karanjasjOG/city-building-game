using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;
using static Inventory;

public class GridBuildingSystem : MonoBehaviour
{

    public static GridBuildingSystem current;


    public GridLayout gridLayout;
    public Tilemap MainTilemap;
    public Tilemap TempTilemap;

    private static Dictionary<TileType, TileBase> tileBases = new Dictionary<TileType, TileBase>();
    [HideInInspector]
    public GameObject buildingGameobject;
    [HideInInspector]
    public Building buildingScript;


    public bool isEdit=false;

    #region Unity Methods


    private void Awake()
    {
        if (current != null)
            Destroy(this);
        else current = this;


    }

    private void Start()
    {

        string tilePath = @"Tiles\";
        tileBases.Add(TileType.Empty, null);
        tileBases.Add(TileType.White, Resources.Load<TileBase>(tilePath + "white"));
        tileBases.Add(TileType.Red, Resources.Load<TileBase>(tilePath + "red"));
        tileBases.Add(TileType.Green, Resources.Load<TileBase>(tilePath + "green"));

    }



    #endregion

    #region Tilemap Management

    public enum TileType
    {
        Empty,
        White,
        Green,
        Red
    }

    private static TileBase[] GetTileBases(BoundsInt area, Tilemap tilemap)
    {
        TileBase[] array = new TileBase[area.size.x * area.size.y * area.size.z];
        int counter = 0;

        foreach (var v in area.allPositionsWithin)
        {

            Vector3Int pos = new Vector3Int(v.x, v.y, v.z);

            array[counter] = tilemap.GetTile(pos);
            counter++;
        }

        return array;
    }


    private static void SetTilesBlock(BoundsInt area, TileType type, Tilemap tilemap)

    {
        int size = area.size.x * area.size.y * area.size.z;
        TileBase[] tileArray = new TileBase[size];
        FillTiles(tileArray, type);
        tilemap.SetTilesBlock(area, tileArray);
    }

    private static void FillTiles(TileBase[] arr, TileType type)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            arr[i] = tileBases[type];
        }
    }

    public void TakeArea(BoundsInt area)
    {

        SetTilesBlock(area, TileType.Empty, TempTilemap);
        SetTilesBlock(area, TileType.Green, MainTilemap);
    }


    #endregion

    #region Building Placement

    public void InitializeWithBuilding(GameObject building)
    {

        var name = building.name;

        InventoryOp inventoryOp = new InventoryOp();

        switch (name)
        {
            case "Mill":

                inventoryOp = new InventoryOp(mill: true);

                break;
            case "Farm":

                inventoryOp = new InventoryOp(farm: true);

                break;
            case "Coffee Shop":

                inventoryOp = new InventoryOp(coffee_shop: true);

                break;
            case "Water Plant":

                inventoryOp = new InventoryOp(water_plant: true);

                break;
            case "Population 1":

                inventoryOp = new InventoryOp(population: true);

                break;
        }



        foreach (var item in inventoryOp.InInventoryOpDict)
        {
            if (item.Value)
            {
                if (Inventory.current.getFromInventory(item.Key) <= 0)
                {

                    GameObject.Find("AlertCanvas").transform.Find("AlertPleaseBuy").gameObject.SetActive(true);
                    return;
                }
            }
        }

        if (!DoesInventoryHasIt(building.GetComponent<Building>()))
        {
            GameObject.Find("AlertCanvas").transform.Find("AlertYouDon'tHaveMaterials").gameObject.SetActive(true);
            return;
        }

        Instantiate(building, Vector3.zero, Quaternion.identity);
        FollowBuilding();
    }

    bool DoesInventoryHasIt(Building buildingScriptTemp)
    {

        if (buildingScriptTemp.needWater >= Inventory.current.getFromInventory(InventoryEnum.Water))
        {


            if (buildingScriptTemp.needSteel <= Inventory.current.getFromInventory(InventoryEnum.Steel))
            {

                if (buildingScriptTemp.needBricks <= Inventory.current.getFromInventory(InventoryEnum.Brick))
                {

                    if (buildingScriptTemp.needWood <= Inventory.current.getFromInventory(InventoryEnum.Wood))
                    {

                        Inventory.current.deductFromInventory(InventoryEnum.Water, buildingScriptTemp.needWater);
                        Inventory.current.deductFromInventory(InventoryEnum.Steel, buildingScriptTemp.needSteel);
                        Inventory.current.deductFromInventory(InventoryEnum.Brick, buildingScriptTemp.needBricks);
                        Inventory.current.deductFromInventory(InventoryEnum.Wood, buildingScriptTemp.needWood);

                        return true;
                    }
                }
            }
        }



        return false;
    }

    //public void InitializeWithBuilding(GameObject building)
    //{
    //    Instantiate(building, Vector3.zero, Quaternion.identity);
    //    FollowBuilding();
    //}

    public void AreaAndPos()
    {
        buildingScript.area.position = gridLayout.WorldToCell(buildingGameobject.transform.position);
    }


    public void ClearMainArea(BoundsInt area)
    {
        TileBase[] toClear = new TileBase[area.size.x * area.size.y * area.size.z];
        FillTiles(toClear, TileType.White);
        MainTilemap.SetTilesBlock(area, toClear);
    }
    public void ClearTempArea(BoundsInt area)
    {

        TileBase[] toClear = new TileBase[area.size.x * area.size.y * area.size.z];
        FillTiles(toClear, TileType.Empty);
        TempTilemap.SetTilesBlock(area, toClear);
    }

    public void FollowBuilding()
    {



        ClearTempArea(buildingScript.prevArea);



        AreaAndPos();



        BoundsInt buildingArea = buildingScript.area;

        TileBase[] baseArray = GetTileBases(buildingArea, MainTilemap);
        int size = baseArray.Length;
        TileBase[] tileArray = new TileBase[size];



        if (CanTakeArea())
        {
            FillTiles(tileArray, TileType.Green);
            buildingScript.ChangeColorToGreen();
        }
        else
        {

            FillTiles(tileArray, TileType.Red);
            buildingScript.ChangeColorToRed();

        }



        TempTilemap.SetTilesBlock(buildingArea, tileArray);
        buildingScript.prevArea = buildingArea;

    }

    public bool CanTakeArea()
    {
        AreaAndPos();
        TileBase[] baseArray = GetTileBases(buildingScript.area, MainTilemap);
        foreach (var b in baseArray)
        {

            if (b != tileBases[TileType.White])
            {

                return false;
            }

        }
        return true;
    }

    public void PlaceIt()
    {


        if (buildingScript.CanBePlaced())
        {
            buildingScript.Place();
        }
        else
        {

            buildingScript.TakeItBack();
        }
    }
    #endregion

    public void FlipIsEdit()
    {
        isEdit = !isEdit;

        if (isEdit)
        {

        }
        else
        {

        }
    }

}
