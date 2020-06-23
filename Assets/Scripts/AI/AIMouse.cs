using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMouse
{
    private static Vector2 BigMapDimension = new Vector2(192f, 108f);
    private static Vector2Int BigMapDivision = new Vector2Int(8, 8);
    private Vector2Int BigMapPosition;
    private Vector2 MapDimension = new Vector2(BigMapDimension.x / BigMapDivision.x, BigMapDimension.y / BigMapDivision.y);
    private Vector2 mouseDisplacement;

    private Vector2 mousePos;
    private bool[] mouseClick = new bool[2];

    public Vector2 position
    {
        get
        {
            return mousePos;
        }
    }

    public AIMouse(Vector2Int BigMapPosition)
    {
        this.BigMapPosition = BigMapPosition;
        mouseDisplacement = new Vector2(BigMapPosition.x * BigMapDimension.x / BigMapDivision.x, BigMapPosition.y * BigMapDimension.y / BigMapDivision.y);
        CenterMouse();
    }

    public Vector2 MoveMouse(Vector2 moveDelta)
    {
        float newX = Mathf.Clamp(mousePos.x + moveDelta.x, 0f, MapDimension.x) + mouseDisplacement.x;
        float newY = Mathf.Clamp(mousePos.y + moveDelta.y, 0f, MapDimension.y) + mouseDisplacement.y;
        mousePos = new Vector2(newX, newY);
        return position;
    }

    public void CenterMouse() {
        MoveMouse(MapDimension / 2);
    }

    public Vector2 GetRandomPos()
    {
        float x = Random.Range(0.5f, MapDimension.x - 0.5f) + mouseDisplacement.x;
        float y = Random.Range(0.5f, MapDimension.y - 0.5f) + mouseDisplacement.y;
        return new Vector2(x, y);
    }

    public bool GetMouseButton(int mouse)
    {
        return mouseClick[Mathf.Clamp(mouse, 0, 1)];
    }

    public void ClickMouseButton(int mouse)
    {
        mouseClick[Mathf.Clamp(mouse, 0, 1)] = true;
    }

    public void LiftMouseButton(int mouse)
    {
        mouseClick[Mathf.Clamp(mouse, 0, 1)] = false;
    }
}
