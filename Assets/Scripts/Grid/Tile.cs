using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private bool m_IsBread;
    public bool IsBread { get => m_IsBread; }

    private int GridPosX, GridPosY;

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

    public void SetPos(int x, int y)
    {
        GridPosX = x;
        GridPosY = y;
        SetGameobjectPosition();
    }

    private void SetGameobjectPosition()
    {
        gameObject.transform.localPosition = new Vector3(GridPosX, 0, GridPosY);
    }

    public void Breadification()
    {
        m_IsBread = true;

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
