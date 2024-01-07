using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Ingredient : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private bool m_IsBread;
    public bool IsBread { get => m_IsBread; }

    private int m_GridPosX, m_GridPosY;

    public int GridPosX { get => m_GridPosX; }
    public int GridPosY { get => m_GridPosY; }

    /// <summary>
    /// set the position on the grid and the gameobjects (through SetGameobjectPosition())
    /// </summary>
    /// <param name="x">x pos on the grid [x pos in scene]</param>
    /// <param name="y">y pos on the grid [z pos in scene]</param>
    /// <param name="h">y pos in the scene</param>
    public void SetPos(int x, int y, int h)
    {
        m_GridPosX = x;
        m_GridPosY = y;
        SetGameobjectPosition(h);
    }

    /// <summary>
    /// set the gameobject position
    /// </summary>
    /// <param name="h">height</param>
    private void SetGameobjectPosition(int h)
    {
        gameObject.transform.localPosition = new Vector3(GridPosX, h, GridPosY);
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        //set the first tile to swap in the gridmanager
        GameManager.instance.EventManager.TriggerEvent(Constants.SET_FIRST_SWAP_TILE, this);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //set the second tile to swap in the gridmanager
        GameManager.instance.EventManager.TriggerEvent(Constants.SET_SECOND_SWAP_TILE, this);

        //add all the ingredients on the first list to the second one
        GameManager.instance.EventManager.TriggerEvent(Constants.ADD_TILES_TO_GRID_POS);
    }

    
    /// <summary>
    /// set an ingredient to be bread
    /// </summary>    
    public void Breadification()
    {
        m_IsBread = true;

        //change the material
        Material mat = new Material(gameObject.GetComponent<Material>());
        if (mat != null)
        {
            //mat.color = Color.red * Color.yellow * Color.black;
            mat.color = new Color(117, 66, 0);

        }
        Material currentMat = gameObject.GetComponent<Material>();
        currentMat = mat;
    }
}
