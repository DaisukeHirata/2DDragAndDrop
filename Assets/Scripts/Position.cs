using UnityEngine;
using System.Collections;

// location logic
// https://docs.google.com/spreadsheets/d/1DD3tWENh-vMkO_At3EHAI9AGxQjMvkM6ey6Nfdki3-Q/edit#gid=1784329131
public class Position : MonoBehaviour
{
    private int vIndex; // vertical index
    private int hIndex; // horizontal index
    private const int xDelta = 150;
    private const int yDelta = 100;

    public int VIndex
    {
        get { return vIndex; }
        set
        {
            vIndex = value;
            transform.localPosition = new Vector2(transform.localPosition.x, vIndex * yDelta);
        }
    }

    public int HIndex
    {
        get { return hIndex; }
        set
        {
            hIndex = value;
            transform.localPosition = new Vector2(hIndex * xDelta, transform.localPosition.y);
        }
    }

    public virtual int XDelta
    {
        get { return xDelta; }
    }

    public virtual int YDelta
    {
        get { return yDelta; }
    }

    public void SetIndex(int hIndex, int yIndex)
    {
        transform.localPosition = new Vector2(hIndex * xDelta, yIndex * yDelta);
    }

    public Vector2 GetPosition()
    {
        return transform.localPosition;
    }
}
