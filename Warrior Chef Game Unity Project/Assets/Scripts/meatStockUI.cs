using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class meatStockUI : MonoBehaviour
{

    public Text meatStockText;
    //manages the current meat stock of the player
    
    public void SetMeatStock(int meatStock)
    {
        meatStockText.text = meatStock.ToString();
    }
}
