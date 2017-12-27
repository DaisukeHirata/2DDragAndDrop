using UnityEngine;
using System.Collections;

// location logic
// https://docs.google.com/spreadsheets/d/1DD3tWENh-vMkO_At3EHAI9AGxQjMvkM6ey6Nfdki3-Q/edit#gid=1784329131
public class PositionConnector : Position
{
    private int connectorOffset = 75;

    public override int VIndex
    {
        get { return vIndex; }
        set
        {
            vIndex = value;
            transform.localPosition = new Vector2(transform.localPosition.x + connectorOffset, vIndex * yDelta);
        }
    }

    public override int HIndex
    {
        get { return hIndex; }
        set
        {
            hIndex = value;
            transform.localPosition = new Vector2(hIndex * xDelta + connectorOffset, transform.localPosition.y);
        }
    }

    public override void SetIndex(int hIndex, int yIndex)
    {
        transform.localPosition = new Vector2(hIndex * xDelta + connectorOffset, yIndex * yDelta);
    }
}
