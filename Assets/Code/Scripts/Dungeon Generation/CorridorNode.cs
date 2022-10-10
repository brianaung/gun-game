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
    private int disFromWallMod = 1;

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
                sortedLeft.Where(children => Mathf.Abs(maxX - children.TopRightAreaCorner.x) < 10).ToList();

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
        { // if no right node
            rightStruct = struct2;
        }
        else
        { // get possible right node
            rightStruct = possibleRightList[0];
        }

        // check if the 2 rooms can be matched
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

        // get corridor corner coords for horizontal orientation
        BottomLeftAreaCorner = new Vector2Int(leftStruct.BottomRightAreaCorner.x, y);
        TopRightAreaCorner = new Vector2Int(rightStruct.TopLeftAreaCorner.x, y + this.corridorWidth);
    }

    private int GetValidY(Vector2Int leftNodeUp, Vector2Int leftNodeDown, Vector2Int rightNodeUp, Vector2Int rightNodeDown)
    {
        // right node bigger than left
        if (rightNodeUp.y >= leftNodeUp.y && leftNodeDown.y >= rightNodeDown.y)
        {
            return StructureHelper.GetMidPoint(
                leftNodeDown + new Vector2Int(0, disFromWallMod),
                leftNodeUp - new Vector2Int(0, disFromWallMod + this.corridorWidth)
            ).y;
        }
        // left node bigger than right
        if (rightNodeUp.y <= leftNodeUp.y && leftNodeDown.y <= rightNodeDown.y)
        {
            return StructureHelper.GetMidPoint(
                rightNodeDown + new Vector2Int(0, disFromWallMod),
                rightNodeUp - new Vector2Int(0, disFromWallMod + this.corridorWidth)
            ).y;
        }
        // left node slightly below right node
        if (leftNodeUp.y >= rightNodeDown.y && leftNodeUp.y <= rightNodeUp.y)
        {
            return StructureHelper.GetMidPoint(
                rightNodeDown + new Vector2Int(0, disFromWallMod),
                leftNodeUp - new Vector2Int(0, disFromWallMod)
            ).y;
        }
        // left node slightly above right node
        if (leftNodeDown.y >= rightNodeDown.y && leftNodeDown.y <= rightNodeUp.y)
        {
            return StructureHelper.GetMidPoint(
                leftNodeDown + new Vector2Int(0, disFromWallMod),
                rightNodeUp - new Vector2Int(0, disFromWallMod + this.corridorWidth)
            ).y;
        }
        return -1;
    }

    // same thing as processRightLeft but for Up and down connections
    private void processUpDown(Node struct1, Node struct2)
    {
        Node botStruct = null;
        List<Node> botStructChildren = StructureHelper.TraverseGraphs(struct1);

        Node topStruct = null;
        List<Node> topStructChildren = StructureHelper.TraverseGraphs(struct2);

        // sort by most right aligned structs in left structs list
        var sortedBot = botStructChildren.OrderByDescending(child => child.TopRightAreaCorner.x).ToList();

        if (sortedBot.Count == 1)
        { // no children
            botStruct = sortedBot[0];
        }
        else
        {
            int maxY = sortedBot[0].TopLeftAreaCorner.y;
            sortedBot =
                sortedBot.Where(children => Mathf.Abs(maxY - children.TopLeftAreaCorner.y) < 10).ToList();

            int index = Random.Range(0, sortedBot.Count);
            botStruct = sortedBot[index];
        }
        var possibleTopList = topStructChildren.Where(
            child => GetValidX(
                botStruct.TopLeftAreaCorner,
                botStruct.TopRightAreaCorner,
                child.BottomLeftAreaCorner,
                child.BottomRightAreaCorner)
            != -1).OrderBy(child => child.BottomRightAreaCorner.y).ToList();
        if (possibleTopList.Count == 0)
        {
            topStruct = struct2;
        }
        else
        {
            topStruct = possibleTopList[0];
        }
        int x = GetValidX(
                botStruct.TopLeftAreaCorner,
                botStruct.TopRightAreaCorner,
                topStruct.BottomLeftAreaCorner,
                topStruct.BottomRightAreaCorner);
        while (x == -1 && sortedBot.Count > 1)
        {
            sortedBot = sortedBot.Where(child => child.TopLeftAreaCorner.x != topStruct.TopLeftAreaCorner.x).ToList();
            botStruct = sortedBot[0];
            x = GetValidX(
                botStruct.TopLeftAreaCorner,
                botStruct.TopRightAreaCorner,
                topStruct.BottomLeftAreaCorner,
                topStruct.BottomRightAreaCorner);
        }
        BottomLeftAreaCorner = new Vector2Int(x, botStruct.TopLeftAreaCorner.y);
        TopRightAreaCorner = new Vector2Int(x + this.corridorWidth, topStruct.BottomLeftAreaCorner.y);
    }

    private int GetValidX(Vector2Int bottomNodeLeft,
        Vector2Int bottomNodeRight, Vector2Int topNodeLeft, Vector2Int topNodeRight)
    {
        if (topNodeLeft.x < bottomNodeLeft.x && bottomNodeRight.x < topNodeRight.x)
        {
            return StructureHelper.GetMidPoint(
                bottomNodeLeft + new Vector2Int(disFromWallMod, 0),
                bottomNodeRight - new Vector2Int(this.corridorWidth + disFromWallMod, 0)
                ).x;
        }
        if (topNodeLeft.x >= bottomNodeLeft.x && bottomNodeRight.x >= topNodeRight.x)
        {
            return StructureHelper.GetMidPoint(
                topNodeLeft + new Vector2Int(disFromWallMod, 0),
                topNodeRight - new Vector2Int(this.corridorWidth + disFromWallMod, 0)
                ).x;
        }
        if (bottomNodeLeft.x >= (topNodeLeft.x) && bottomNodeLeft.x <= topNodeRight.x)
        {
            return StructureHelper.GetMidPoint(
                bottomNodeLeft + new Vector2Int(disFromWallMod, 0),
                topNodeRight - new Vector2Int(this.corridorWidth + disFromWallMod, 0)

                ).x;
        }
        if (bottomNodeRight.x <= topNodeRight.x && bottomNodeRight.x >= topNodeLeft.x)
        {
            return StructureHelper.GetMidPoint(
                topNodeLeft + new Vector2Int(disFromWallMod, 0),
                bottomNodeRight - new Vector2Int(this.corridorWidth + disFromWallMod, 0)

                ).x;
        }
        return -1;
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