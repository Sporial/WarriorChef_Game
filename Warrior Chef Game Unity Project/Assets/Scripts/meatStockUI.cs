using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class meatStockUI : MonoBehaviour
{

    public Text meatStockText;

    
    public void SetMeatStock(int meatStock)
    {
        meatStockText.text = meatStock.ToString();
    }
}
