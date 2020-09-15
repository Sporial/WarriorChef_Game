using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Tilemaps;

public class LevelGenTest : MonoBehaviour
{
    //Tile holders
    public GameObject startingPrefab;
    public GameObject[] tilePrefabs;
    public GameObject cookingPrefab;
    SpriteRenderer startPos;
    Vector3 startcollSize;
    

    //The number of tiles used per level. Change this based on how long you want it
    GameObject[] tileList = new GameObject[4];
    void Start()
    {
        //0,0,0 instantiate I think
        tileList[0] = Instantiate(startingPrefab);
        startPos = startingPrefab.GetComponent<SpriteRenderer>();
        startcollSize = startPos.bounds.size;

        int randInst;
        int prevRandInst = -1;

        for(int i=1;i <tileList.Length;i++)
        {
            //get Random tile. Change this so it doesn't repeat
            
            randInst = GetRandom();
            while(prevRandInst == randInst)
            {
                randInst = GetRandom();
            }
            //Pain
            tileList[i] = Instantiate(tilePrefabs[randInst], new Vector2(GetTile(i - 1).bounds.center.x + GetTile(i - 1).bounds.size.x / 2 + tilePrefabs[randInst].GetComponent<SpriteRenderer>().bounds.size.x / 2, GetTile(i - 1).transform.position.y),Quaternion.identity);
            prevRandInst = randInst;
        }
        Instantiate(cookingPrefab, new Vector2(GetTile(tileList.Length-1).bounds.center.x + GetTile(tileList.Length-1).bounds.size.x / 2 + cookingPrefab.GetComponent<SpriteRenderer>().bounds.size.x / 2, GetTile(tileList.Length-1).transform.position.y), Quaternion.identity);
        
    }

    SpriteRenderer GetTile(int randCall)
    {
        return tileList[randCall].GetComponent<SpriteRenderer>();
    }

    int GetRandom()
    {
        return UnityEngine.Random.Range(0, tilePrefabs.Count());
    }
}
