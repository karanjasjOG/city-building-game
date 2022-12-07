using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SellPanel2 : MonoBehaviour
{
    public TMP_Text text;

    public Slider slider;

    float minValue = 0;
    float maxValue = 0;

    

     void SetItUp()
    {

        maxValue = Inventory.current.getFromInventory(Inventory.InventoryEnum.Hemp);
        slider.value = 1;
        text.text = maxValue.ToString();
    }
}
