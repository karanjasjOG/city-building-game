using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceOrNot : MonoBehaviour
{
    public static PlaceOrNot current;
    [HideInInspector]
    public Building prevBuilding;
    [HideInInspector]
    public Building building;
    private void Awake()
    {
        current = this;
        gameObject.SetActive(false);
    }
    public void ClickOk()
    {
        building.isPlaceOrNotActive = false;
        building.PlaceHere();
        gameObject.SetActive(false);
    }
    public void ClickNotOk()
    {
        building.isPlaceOrNotActive = false;

        building.TakeItBack();
        gameObject.SetActive(false);
    }

    public void ClickFlip()
    {
        building.gameObject.transform.localScale = new Vector3(building.gameObject.transform.localScale.x * -1, building.gameObject.transform.localScale.y, building.gameObject.transform.localScale.z);
    }

    public void ClickDelete()
    {
        building.DeleteIt();
        gameObject.SetActive(false);
    }

}
