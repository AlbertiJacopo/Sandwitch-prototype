using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public List<Ingredient> IngredientsList = new List<Ingredient>();

    private List<Ingredient>[,] m_GridData = new List<Ingredient>[4, 4];

    private Ingredient m_FirstTile, m_SecondTile;

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
        IngredientsList.Shuffle(); //shuffle the elemets of the list

        //assigne for every grid position an ingredient
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                m_GridData[i, j] = new List<Ingredient>();

                Ingredient tile = IngredientsList[j];

                //set the position of the ingredient's gameobject
                tile.SetPos(i, j, 0);

                m_GridData[i, j].Add(tile);

                j++;
            }

            i++;
        }
    }

    /// <summary>
    /// set the first ingredient to be swapped
    /// </summary>
    /// <param name="param"></param>
    public void SetFirstSwapTile(object[] param)
    {
        m_FirstTile = (Ingredient)param[0];
    }

    /// <summary>
    /// set the second ingredient to be swapped
    /// </summary>
    /// <param name="param"></param>
    public void SetSecondSwapTile(object[] param)
    {
        m_SecondTile = (Ingredient)param[0];
    }

    /// <summary>
    /// add all the ingrediens on a list the selected grid position to another one
    /// </summary>
    /// <param name="param"></param>
    public void AddToTile(object[] param)
    {
        if (m_FirstTile != null && m_SecondTile != null)
        {
            //get x and y pos of the first list
            int x1 = m_FirstTile.GridPosX;
            int y1 = m_FirstTile.GridPosY;

            //get x and y pos of the second list
            int x2 = m_SecondTile.GridPosX;
            int y2 = m_SecondTile.GridPosY;

            int n = 0;

            //reverse the list
            m_GridData[x1, y1].Reverse();
        
            //add ingredients and set their gameobjects' position
            foreach (Ingredient tile in m_GridData[x1, y1])
            {
                int i = 0;

                List<Ingredient> previous = m_GridData[x1, y1];

                m_GridData[x2, y2].Add(previous[i]);

                m_GridData[x2, y2][i].SetPos(x2, y2, n);
                n++;

                i++;
            }

            //clear the first list
            m_GridData[x1, y1].Clear();
        
            //check win condition
            CheckWin(m_GridData[x2, y2]);

            //resets the first and second tile to swap
            m_FirstTile = null;
            m_SecondTile = null;
        }
    }

    /// <summary>
    /// check win and if it check true it makes the win screen appear
    /// </summary>
    /// <param name="list"></param>
    private void CheckWin(List<Ingredient> list)
    {
        if (list[0].IsBread && list[list.Count].IsBread)
        {
            GameManager.instance.EventManager.TriggerEvent(Constants.UI_SCREEN_SET_ACTIVE);
        }
    }
    
}


//static class to permit to shuffle every list that need to
public static class ListsCommands
{
    private static System.Random rng = new System.Random();

    /// <summary>
    /// Shuffle a list
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
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
