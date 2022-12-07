using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCloseShopStore : MonoBehaviour

{

    GameObject shop;

    GameObject store;
    GameObject sellShop;
    GameObject sellShop2;

    private void Start()
    {
        shop = GameObject.Find("ShopCanvas").transform.Find("Shop").gameObject;
        store = GameObject.Find("MyHomeCanvas").transform.Find("GetBuildingsPanel").gameObject;
        sellShop=GameObject.Find("TradeCanvas").transform.Find("SellPanel").gameObject;
        sellShop2= GameObject.Find("TradeCanvas").transform.Find("SellPanel2").gameObject;
    }

    public void DoShop()
    {
        if (shop.activeSelf)
            shop.SetActive(false);
        else
        {
            Focus.current.ShopInFocus();
            shop.SetActive(true);
        }
    }
    public void DoEditStore()
    {
        if (store.activeSelf)
            store.SetActive(false);
        else
        {
            Focus.current.StoreInFocus();
            store.SetActive(true);
        }
    }

    public void CloseShop()
    {
        shop.SetActive(false);
    }
    public void CloseStore()
    {
        store.SetActive(false);
    }
    public void CloseSellShop()
    {
        sellShop.SetActive(false);
    }
    public void CloseSellShop2()
    {
        sellShop2.SetActive(false);
    }
}
