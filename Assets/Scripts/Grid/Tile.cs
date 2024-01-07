using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private bool m_IsBread;
    public bool IsBread { get => m_IsBread; }

    private int m_GridPosX, m_GridPosY;

    public int GridPosX { get => m_GridPosX; }
    public int GridPosY { get => m_GridPosY; }

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPos(int x, int y, int h)
    {
        m_GridPosX = x;
        m_GridPosY = y;
        SetGameobjectPosition(h);
    }

    private void SetGameobjectPosition(int h)
    {
        gameObject.transform.localPosition = new Vector3(GridPosX, h, GridPosY);
    }

    //public void Breadification()
    //{
    //    m_IsBread = true;

    //    Material mat = new Material(gameObject.GetComponent<Material>());
    //    if (mat != null)
    //    {
    //        //mat.color = Color.red * Color.yellow * Color.black;
    //        mat.color = new Color(117, 66, 0);

    //    }
    //    Material currentMat = gameObject.GetComponent<Material>();
    //    currentMat = mat;
    //}

    public void OnPointerDown(PointerEventData eventData)
    {
        GameManager.instance.EventManager.TriggerEvent(Constants.SET_FIRST_SWAP_TILE, this);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        GameManager.instance.EventManager.TriggerEvent(Constants.SET_SECOND_SWAP_TILE, this);
        GameManager.instance.EventManager.TriggerEvent(Constants.ADD_TILES_TO_GRID_POS);
    }

}
