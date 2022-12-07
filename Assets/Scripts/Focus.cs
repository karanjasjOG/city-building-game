using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Focus : MonoBehaviour
{
    public static Focus current;

    private void Awake()
    {
        if (current != null)
            Destroy(this);
        else current = this;
    }

    public void ShopInFocus()
    {
        GameObject.Find("MyHomeCanvas").transform.Find("GetBuildingsPanel").gameObject.SetActive(false);
        GameObject.Find("TradeCanvas").transform.Find("SellPanel").gameObject.SetActive(false);
    }
    public void StoreInFocus()
    {
        GameObject.Find("ShopCanvas").transform.Find("Shop").gameObject.SetActive(false);
        GameObject.Find("TradeCanvas").transform.Find("SellPanel").gameObject.SetActive(false);
    }
    public void TradeInFocus()
    {
        GameObject.Find("MyHomeCanvas").transform.Find("GetBuildingsPanel").gameObject.SetActive(false);
        GameObject.Find("ShopCanvas").transform.Find("Shop").gameObject.SetActive(false);
    }
}
