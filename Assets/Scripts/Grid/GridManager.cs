using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public List<Tile> Tiles = new List<Tile>();

    private List<Tile>[,] m_GridData = new List<Tile>[4, 4];

    private void Awake()
    {
        GameManager.instance.EventManager.Register(Constants.ADD_TILES_TO_GRID_POS, AddToTile);
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
                tile.SetPos(i, j);

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

    public void AddToTile(object[] param)
    {
        int x1 = (int)param[0];
        int y1 = (int)param[1];
        int x2 = (int)param[2];
        int y2 = (int)param[3];

        foreach (Tile tile in m_GridData[x1, y1])
        {
            int i = 0;
            m_GridData[x1, y1].Reverse();

            List<Tile> previous = m_GridData[x1, y1];

            m_GridData[x2, y2].Add(previous[i]);

            i++;
        }

        m_GridData[x1, y1].Clear();
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
