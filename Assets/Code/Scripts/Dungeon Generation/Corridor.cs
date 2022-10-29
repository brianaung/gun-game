using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Corridor : Node
{
    private Node node1;
    private Node node2;
    private int width;
    private int disFromWallMod = 1;

    public Corridor(Node node1, Node node2, int width) : base(null)
    {
        this.node1 = node1;
        this.node2 = node2;
        this.width = width;

        GenerateCorridor();
    }

    private void GenerateCorridor()
    {
        var relativePos = CheckPos();

        switch (relativePos)
        {
            case RelativePos.Up:
                processUpDown(this.node1, this.node2);
                break;
            case RelativePos.Down:
                processUpDown(this.node2, this.node1);
                break;
            case RelativePos.Right:
                processRightLeft(this.node1, this.node2);
                break;
            case RelativePos.Left:
                processRightLeft(this.node2, this.node1);
                break;
        }
        ;
    }

    private void processRightLeft(Node node1, Node node2)
    {
        Node leftNode = null;
        List<Node> leftChildren = Helper.TraverseGraphs(node1);

        Node rightNode = null;
        List<Node> rightChildren = Helper.TraverseGraphs(node2);

        // sort by most right aligned structs in left structs list
        var sortedLeft = leftChildren.OrderByDescending(child => child.TopRight.x).ToList();

        if (sortedLeft.Count == 1)
        { // no children
            leftNode = sortedLeft[0];
        }
        else
        {
            int maxX = sortedLeft[0].TopRight.x;
            sortedLeft = sortedLeft
                .Where(children => Mathf.Abs(maxX - children.TopRight.x) < 10)
                .ToList();

            int index = Random.Range(0, sortedLeft.Count);
            leftNode = sortedLeft[index];
        }

        var possibleRightList = rightChildren
            .Where(
                child =>
                    GetValidY(leftNode.TopRight, leftNode.BotRight, child.TopLeft, child.BotLeft)
                    != -1
            )
            .OrderBy(child => child.BotRight.x)
            .ToList();

        if (possibleRightList.Count <= 0)
        { // if no right node
            rightNode = node2;
        }
        else
        { // get possible right node
            rightNode = possibleRightList[0];
        }

        // check if the 2 rooms can be matched
        int y = GetValidY(
            leftNode.TopLeft,
            leftNode.BotRight,
            rightNode.TopLeft,
            rightNode.BotLeft
        );
        while (y == -1 && sortedLeft.Count > 1)
        {
            sortedLeft = sortedLeft.Where(child => child.TopLeft.y != leftNode.TopLeft.y).ToList();
            leftNode = sortedLeft[0];
            y = GetValidY(
                leftNode.TopLeft,
                leftNode.BotRight,
                rightNode.TopLeft,
                rightNode.BotLeft
            );
        }

        // get corridor corner coords for horizontal orientation
        BotLeft = new Vector2Int(leftNode.BotRight.x, y);
        TopRight = new Vector2Int(rightNode.TopLeft.x, y + this.width);
    }

    private int GetValidY(
        Vector2Int leftNodeUp,
        Vector2Int leftNodeDown,
        Vector2Int rightNodeUp,
        Vector2Int rightNodeDown
    )
    {
        // right node bigger than left
        if (rightNodeUp.y >= leftNodeUp.y && leftNodeDown.y >= rightNodeDown.y)
        {
            return Helper
                .GetMidPoint(
                    leftNodeDown + new Vector2Int(0, disFromWallMod),
                    leftNodeUp - new Vector2Int(0, disFromWallMod + this.width)
                )
                .y;
        }
        // left node bigger than right
        if (rightNodeUp.y <= leftNodeUp.y && leftNodeDown.y <= rightNodeDown.y)
        {
            return Helper
                .GetMidPoint(
                    rightNodeDown + new Vector2Int(0, disFromWallMod),
                    rightNodeUp - new Vector2Int(0, disFromWallMod + this.width)
                )
                .y;
        }
        // left node slightly below right node
        if (leftNodeUp.y >= rightNodeDown.y && leftNodeUp.y <= rightNodeUp.y)
        {
            return Helper
                .GetMidPoint(
                    rightNodeDown + new Vector2Int(0, disFromWallMod),
                    leftNodeUp - new Vector2Int(0, disFromWallMod)
                )
                .y;
        }
        // left node slightly above right node
        if (leftNodeDown.y >= rightNodeDown.y && leftNodeDown.y <= rightNodeUp.y)
        {
            return Helper
                .GetMidPoint(
                    leftNodeDown + new Vector2Int(0, disFromWallMod),
                    rightNodeUp - new Vector2Int(0, disFromWallMod + this.width)
                )
                .y;
        }
        return -1;
    }

    // same thing as processRightLeft but for Up and down connections
    private void processUpDown(Node node1, Node node2)
    {
        Node botNode = null;
        List<Node> botChildren = Helper.TraverseGraphs(node1);

        Node topNode = null;
        List<Node> topChildren = Helper.TraverseGraphs(node2);

        // sort by most right aligned structs in left structs list
        var sortedBot = botChildren.OrderByDescending(child => child.TopRight.x).ToList();

        if (sortedBot.Count == 1)
        { // no children
            botNode = sortedBot[0];
        }
        else
        {
            int maxY = sortedBot[0].TopLeft.y;
            sortedBot = sortedBot
                .Where(children => Mathf.Abs(maxY - children.TopLeft.y) < 10)
                .ToList();

            int index = Random.Range(0, sortedBot.Count);
            botNode = sortedBot[index];
        }
        var possibleTopList = topChildren
            .Where(
                child =>
                    GetValidX(botNode.TopLeft, botNode.TopRight, child.BotLeft, child.BotRight)
                    != -1
            )
            .OrderBy(child => child.BotRight.y)
            .ToList();
        if (possibleTopList.Count == 0)
        {
            topNode = node2;
        }
        else
        {
            topNode = possibleTopList[0];
        }
        int x = GetValidX(botNode.TopLeft, botNode.TopRight, topNode.BotLeft, topNode.BotRight);
        while (x == -1 && sortedBot.Count > 1)
        {
            sortedBot = sortedBot.Where(child => child.TopLeft.x != topNode.TopLeft.x).ToList();
            botNode = sortedBot[0];
            x = GetValidX(botNode.TopLeft, botNode.TopRight, topNode.BotLeft, topNode.BotRight);
        }
        BotLeft = new Vector2Int(x, botNode.TopLeft.y);
        TopRight = new Vector2Int(x + this.width, topNode.BotLeft.y);
    }

    private int GetValidX(
        Vector2Int bottomNodeLeft,
        Vector2Int bottomNodeRight,
        Vector2Int topNodeLeft,
        Vector2Int topNodeRight
    )
    {
        if (topNodeLeft.x < bottomNodeLeft.x && bottomNodeRight.x < topNodeRight.x)
        {
            return Helper
                .GetMidPoint(
                    bottomNodeLeft + new Vector2Int(disFromWallMod, 0),
                    bottomNodeRight - new Vector2Int(this.width + disFromWallMod, 0)
                )
                .x;
        }
        if (topNodeLeft.x >= bottomNodeLeft.x && bottomNodeRight.x >= topNodeRight.x)
        {
            return Helper
                .GetMidPoint(
                    topNodeLeft + new Vector2Int(disFromWallMod, 0),
                    topNodeRight - new Vector2Int(this.width + disFromWallMod, 0)
                )
                .x;
        }
        if (bottomNodeLeft.x >= (topNodeLeft.x) && bottomNodeLeft.x <= topNodeRight.x)
        {
            return Helper
                .GetMidPoint(
                    bottomNodeLeft + new Vector2Int(disFromWallMod, 0),
                    topNodeRight - new Vector2Int(this.width + disFromWallMod, 0)
                )
                .x;
        }
        if (bottomNodeRight.x <= topNodeRight.x && bottomNodeRight.x >= topNodeLeft.x)
        {
            return Helper
                .GetMidPoint(
                    topNodeLeft + new Vector2Int(disFromWallMod, 0),
                    bottomNodeRight - new Vector2Int(this.width + disFromWallMod, 0)
                )
                .x;
        }
        return -1;
    }

    private RelativePos CheckPos()
    {
        Vector2 node1Mid = ((Vector2)node1.TopRight + node1.BotLeft) / 2;
        Vector2 node2Mid = ((Vector2)node2.TopRight + node2.BotLeft) / 2;

        // to check the direction where the corridors should connect
        float angle = CalculateAngle(node1Mid, node2Mid);

        if ((angle < 45 && angle >= 0) || (angle > -45 && angle < 0))
        {
            return RelativePos.Right;
        }
        else if (angle > 45 && angle < 135)
        {
            return RelativePos.Up;
        }
        else if (angle > -135 && angle < -45)
        {
            return RelativePos.Down;
        }
        else
        {
            return RelativePos.Left;
        }
    }

    private float CalculateAngle(Vector2 node1Mid, Vector2 node2Mid)
    {
        return Mathf.Atan2(node2Mid.y - node1Mid.y, node2Mid.x - node1Mid.x) * Mathf.Rad2Deg;
    }
}

public enum RelativePos
{
    Up,
    Down,
    Right,
    Left
}
