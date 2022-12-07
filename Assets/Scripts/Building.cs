using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Building : MonoBehaviour
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

    public float waterRate=0;
    public float woodRate=0;
    public float steelRate=0;
    public float brickRate=0;
    public float hempRate=0;

    public float needWater=20;
    public float needSteel=20;
    public float needBricks=20;
    public float needWood=20;


    float second = 1;


    public bool Placed { get; private set; }

    [HideInInspector]
    public bool isPlacedEvenOnce;
    [HideInInspector]
    public bool isPlaceOrNotActive;

    public BoundsInt area;
    [HideInInspector]
    public BoundsInt prevArea;
    private Vector3 prevPos;
    private BoundsInt orignalArea;

    private SpriteRenderer childRenderer;
    
    

    #region Unity Methods

    private void Awake()
    {
        inventoryOp = new InventoryOp(mill: mill, farm: farm, coffee_shop: coffee_shop, water_plant: water_plant, population: population, wood: wood, water: water, brick: brick
           , steel: steel, hemp: hemp, joints: joints, coins: coins, gems: gems);

        GiveThisToGrid();

        GetPlaceOrNot();
        GridBuildingSystem.current.AreaAndPos();
        isPlacedEvenOnce = false;
        isPlaceOrNotActive = true;

    }

    private void Update()
    {
        second -= Time.deltaTime;
        if (second <= 0)
        {
            second = 1;


            Inventory.current.inventoryDict[Inventory.InventoryEnum.Hemp] += hempRate;
            Inventory.current.inventoryDict[Inventory.InventoryEnum.Water] += waterRate;
            Inventory.current.inventoryDict[Inventory.InventoryEnum.Wood] += woodRate;
            Inventory.current.inventoryDict[Inventory.InventoryEnum.Steel] += steelRate;
            Inventory.current.inventoryDict[Inventory.InventoryEnum.Brick] += brickRate;

        }
    }

    private void OnMouseDown()
    {
        if (GridBuildingSystem.current.isEdit)
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                GridBuildingSystem.current.ClearMainArea(orignalArea);
                GiveThisToGrid();
                GetPlaceOrNot();
            }
        }
        else
        {

        }
    }
    private void OnMouseUpAsButton()
    {
        if (!GridBuildingSystem.current.isEdit)
        {
            GameObject.Find("TradeCanvas").transform.Find("SellPanel").gameObject.SetActive(true);
        }
    }

    private void OnMouseDrag()
    {
        if (GridBuildingSystem.current.isEdit)
        {
            if (GridBuildingSystem.current.buildingGameobject == gameObject)
            {
                Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                Vector3 cellPos = GridBuildingSystem.current.gridLayout.WorldToCell(touchPos);
                if (prevPos != cellPos)
                {
                    Vector3 tempPos;
                    tempPos = GridBuildingSystem.current.gridLayout.CellToLocalInterpolated(cellPos);
                    GridBuildingSystem.current.AreaAndPos();
                    transform.position = tempPos;
                    GridBuildingSystem.current.FollowBuilding();
                    //PlaceOrNot.current.gameObject.transform.position = Camera.main.WorldToScreenPoint(GetLeastYPos() + new Vector3(0.5f, -.5f, 0));
                    prevPos = cellPos;
                }

            }
        }

    }




    #endregion





    #region Build Methods

    private void GiveThisToGrid()
    {

        GridBuildingSystem.current.buildingGameobject = gameObject;

        GridBuildingSystem.current.buildingScript = this;

        childRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();

    }

    public bool CanBePlaced()
    {

        GridBuildingSystem.current.AreaAndPos();
        if (GridBuildingSystem.current.CanTakeArea())
        {
            return true;
        }
        return false;
    }

    public void Place()
    {

        GridBuildingSystem.current.AreaAndPos();

        Placed = true;

        orignalArea = area;
        GridBuildingSystem.current.TakeArea(area);
        SetDefaultSortingLayer();
        isPlacedEvenOnce = true;
        isPlaceOrNotActive = false;
        ChangeColorToWhite();
    }
    public void PlaceHere()
    {

        GridBuildingSystem.current.PlaceIt();
    }
    public void TakeItBack()
    {

        isPlaceOrNotActive = false;
        GridBuildingSystem.current.AreaAndPos();
        GridBuildingSystem.current.ClearTempArea(area);
        if (!isPlacedEvenOnce)
        {
            Destroy(gameObject);
            return;
        }

        transform.position = GridBuildingSystem.current.gridLayout.CellToLocalInterpolated(orignalArea.position);
        GridBuildingSystem.current.AreaAndPos();

        GridBuildingSystem.current.TakeArea(orignalArea);
        SetDefaultSortingLayer();
        ChangeColorToWhite();
    }

    public void DeleteIt()
    {
        isPlaceOrNotActive = false;
        GridBuildingSystem.current.AreaAndPos();
        GridBuildingSystem.current.ClearTempArea(area);
        GridBuildingSystem.current.ClearMainArea(orignalArea);

        Destroy(gameObject);

    }

    #endregion



    #region Helpers

    private void OnDestroy()
    {
        foreach (var item in inventoryOp.InInventoryOpDict)
        {
            if (item.Value)
            {
                Inventory.current.addToInventory(item.Key, 1);
            }
        }
    }

    private void GetPlaceOrNot()
    {
        PlaceOrNot.current.prevBuilding = PlaceOrNot.current.building;
        PlaceOrNot.current.building = this;
        PlaceOrNot.current.building.isPlaceOrNotActive = true;
        if (PlaceOrNot.current.prevBuilding != PlaceOrNot.current.building)
        {
            if (PlaceOrNot.current.prevBuilding != null)
            {

                if (PlaceOrNot.current.prevBuilding.isPlaceOrNotActive)
                {
                    PlaceOrNot.current.prevBuilding.TakeItBack();
                }
            }
        }
        childRenderer.sortingLayerName = "Being Dragged";
        //PlaceOrNot.current.gameObject.transform.position = Camera.main.WorldToScreenPoint(GetLeastYPos() + new Vector3(0.5f, -.5f, 0));
        PlaceOrNot.current.gameObject.SetActive(true);
    }
    public void SetDefaultSortingLayer()
    {
        childRenderer.sortingLayerName = "Default";
        //Debug.Log(childRenderer.transform.position);
        //Debug.Log(GridBuildingSystem.current.gridLayout.CellToWorld(Vector3Int.FloorToInt(area.center)));
        childRenderer.sortingOrder = GetSortingOrder(GridBuildingSystem.current.gridLayout.CellToWorld(Vector3Int.FloorToInt(area.center)).y);

        //childRenderer.sortingOrder=(int)Camera.main.WorldToScreenPoint(childRenderer.transform.position).y*(-100);


        int GetSortingOrder(float y)
        {

            float beforeDecimal = (float)Math.Truncate(y);

            float afterDecimal = y - beforeDecimal;


            int a = (int)((beforeDecimal * -100) + (afterDecimal * -100));
            return a;

        }


    }


    public void ChangeColorToRed()
    {
        childRenderer.color = new Color(1, 0, 0, .5f);
    }
    public void ChangeColorToWhite()
    {
        childRenderer.color = new Color(1, 1, 1, 1);
    }

    public void ChangeColorToGreen()
    {
        childRenderer.color = new Color(.4f, .7f, .4f, .7f);
    }



    


    #endregion
}
