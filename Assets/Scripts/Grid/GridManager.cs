using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public List<Tile> Tiles = new List<Tile>();

    private List<Tile>[,] m_GridData = new List<Tile>[4, 4];

    private Tile m_FirstTile, m_SecondTile;

    private void Awake()
    {
        GameManager.instance.EventManager.Register(Constants.ADD_TILES_TO_GRID_POS, AddToTile);
        GameManager.instance.EventManager.Register(Constants.SET_FIRST_SWAP_TILE, SetFirstSwapTile);
        GameManager.instance.EventManager.Register(Constants.SET_SECOND_SWAP_TILE, SetSecondSwapTile);
    }

    // Start is called before the first frame update
    void Start()
    {
        InitGrid();
    }

    private void InitGrid()
    {
        Tiles.Shuffle();

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                m_GridData[i, j] = new List<Tile>();

                Tile tile = Tiles[j];

                //if (i == 0 && j == 0 || i == 4 && j == 4) tile.Breadification();
                tile.SetPos(i, j, 0);

                m_GridData[i, j].Add(tile);

                j++;
            }

            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetFirstSwapTile(object[] param)
    {
        m_FirstTile = (Tile)param[0];
    }
    
    public void SetSecondSwapTile(object[] param)
    {
        m_SecondTile = (Tile)param[0];
    }

    public void AddToTile(object[] param)
    {
        int x1 = m_FirstTile.GridPosX;
        int y1 = m_FirstTile.GridPosY;
        int x2 = m_SecondTile.GridPosX;
        int y2 = m_SecondTile.GridPosY;

        int n = 0;

        foreach (Tile tile in m_GridData[x1, y1])
        {
            int i = 0;
            m_GridData[x1, y1].Reverse();

            List<Tile> previous = m_GridData[x1, y1];

            m_GridData[x2, y2].Add(previous[i]);

            m_GridData[x2, y2][i].SetPos(x2, y2, n);
            n++;

            i++;
        }

        m_GridData[x1, y1].Clear();
        CheckWin(m_GridData[x2, y2]);
    }

    private void CheckWin(List<Tile> list)
    {
        if (list[0].IsBread && list[list.Count].IsBread)
        {
            GameManager.instance.EventManager.TriggerEvent(Constants.UI_SCREEN_SET_ACTIVE);
        }
    }
    
}


public static class ListsCommands
{
    private static System.Random rng = new System.Random();

    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
