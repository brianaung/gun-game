using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class CorridorNode : Node
{
    private Node struct1;
    private Node struct2;
    private int corridorWidth;

    public CorridorNode(Node node1, Node node2, int corridorWidth) : base(null)
    {
        this.struct1 = node1;
        this.struct2 = node2;
        this.corridorWidth = corridorWidth;

        GenerateCorridor();
    }

    private void GenerateCorridor()
    {
        var relativePosStruct2 = CheckPos();

    }

    private RelativePos CheckPos()
    {
        Vector2 struct1Mid = ((Vector2)struct1.TopRightAreaCorner + struct1.BottomLeftAreaCorner) / 2;
        Vector2 struct2Mid = ((Vector2)struct2.TopRightAreaCorner + struct2.BottomLeftAreaCorner) / 2;

        // to check the direction where the corridors should connect
        float angle = CalculateAngle(struct1Mid, struct2Mid);

        if ((angle < 45 && angle >= 0) || (angle > -45 && angle < 0))
        {
            return RelativePos.Right;
        } else if (angle > 45 && angle < 135)
        {
            return RelativePos.Up;
        } else if (angle > -135 && angle < -45)
        {
            return RelativePos.Down;
        } else
        {
            return RelativePos.Left;
        }
    }

    private float CalculateAngle(Vector2 struct1Mid, Vector2 struct2Mid)
    {
        return Mathf.Atan2(
            struct2Mid.y - struct1Mid.y, 
            struct2Mid.x - struct1Mid.x
            ) * Mathf.Rad2Deg;
    }

}
public enum RelativePos
{
    Up,
    Down,
    Right,
    Left
}