using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int meatStock;
    public int upgradeToken;
    public int maxHealth;
    public int xPos;
    public int yPos;
    public int mapLevel;

    public PlayerData(playerController player)
    {
        meatStock = player.meatStock;
        upgradeToken = player.upgradeToken;
        maxHealth = player.maxHealth;
        xPos = (int)player.transform.position.x;
        yPos = (int)player.transform.position.y;
    }
}
