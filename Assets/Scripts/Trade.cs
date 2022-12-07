using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trade : MonoBehaviour
{
    public static Trade current;

    public GameObject obj;

    public InventoryOp whatYouSelling;

    private void Awake()
    {
        if (current != null)

            Destroy(this);

        else
            current = this;
    }

}
