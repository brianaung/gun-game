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

        switch (relativePosStruct2)
        {
            case RelativePos.Up:
                processUpDown(this.struct1, this.struct2);
                break;
            case RelativePos.Down:
                processUpDown(this.struct2, this.struct1);
                break;
            case RelativePos.Right:
                processRightLeft(this.struct1, this.struct2);
                break;
            case RelativePos.Left:
                processRightLeft(this.struct2, this.struct1);
                break;
            default:
                break;
        };
    }

    private void processRightLeft(Node struct1, Node struct2)
    {
        Node leftStruct = null;
        List<Node> leftStructChildren = StructureHelper.TraverseGraphs(struct1);

        Node rightStruct = null;
        List<Node> rightStructChildren = StructureHelper.TraverseGraphs(struct2);

        // sort by most right aligned structs in left structs list
        var sortedLeft = leftStructChildren.OrderByDescending(child => child.TopRightAreaCorner.x).ToList();

        if (sortedLeft.Count == 1)
        { // no children
            leftStruct = sortedLeft[0];
        } else
        {
            int maxX = sortedLeft[0].TopRightAreaCorner.x;
            sortedLeft = 
                sortedLeft.Where(children => Math.Abs(maxX - children.TopRightAreaCorner.x) < 10).ToList();

            int index = Random.Range(0, sortedLeft.Count);
            leftStruct = sortedLeft[index];
        }

        var possibleRightList = rightStructChildren.Where(
           child => GetValidY(
               leftStruct.TopRightAreaCorner,
               leftStruct.BottomRightAreaCorner,
               child.TopLeftAreaCorner,
               child.BottomLeftAreaCorner
               ) != -1
           ).OrderBy(child => child.BottomRightAreaCorner.x).ToList();

        if (possibleRightList.Count <= 0)
        {
            rightStruct = struct2;
        }
        else
        {
            rightStruct = possibleRightList[0];
        }
        int y = GetValidY(leftStruct.TopLeftAreaCorner, leftStruct.BottomRightAreaCorner,
            rightStruct.TopLeftAreaCorner,
            rightStruct.BottomLeftAreaCorner);
        while (y == -1 && sortedLeft.Count > 1)
        {
            sortedLeft = sortedLeft.Where(
                child => child.TopLeftAreaCorner.y != leftStruct.TopLeftAreaCorner.y).ToList();
            leftStruct = sortedLeft[0];
            y = GetValidY(leftStruct.TopLeftAreaCorner, leftStruct.BottomRightAreaCorner,
            rightStruct.TopLeftAreaCorner,
            rightStruct.BottomLeftAreaCorner);
        }
        BottomLeftAreaCorner = new Vector2Int(leftStruct.BottomRightAreaCorner.x, y);
        TopRightAreaCorner = new Vector2Int(rightStruct.TopLeftAreaCorner.x, y + this.corridorWidth);
    }

    private int GetValidY(Vector2Int leftNodeUp, Vector2Int leftNodeDown, Vector2Int rightNodeUp, Vector2Int rightNodeDown)
    {
        if (rightNodeUp.y >= leftNodeUp.y && leftNodeDown.y >= rightNodeDown.y)
        {
        }
        if (rightNodeUp.y <= leftNodeUp.y && leftNodeDown.y <= rightNodeDown.y)
        {
        }
        if (leftNodeUp.y >= rightNodeDown.y && leftNodeUp.y <= rightNodeUp.y)
        {
        }
        if (leftNodeDown.y >= rightNodeDown.y && leftNodeDown.y <= rightNodeUp.y)
        {
        }
        return -1;
    }

    private void processUpDown(Node struct1, Node struct2)
    {
        throw new NotImplementedException();
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