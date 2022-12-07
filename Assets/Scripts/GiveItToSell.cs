using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveItToSell : MonoBehaviour
{
    public bool hemp = false;

    void GiveItToTradeManager()
    {
        Trade.current.whatYouSelling = new InventoryOp(hemp: hemp);
    }

    void ActivateSellPanel2()
    {

    }
}
