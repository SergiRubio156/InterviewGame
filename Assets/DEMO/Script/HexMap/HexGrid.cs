using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;


[CustomEditor(typeof(HexGrid))]

public class HexGrid : MonoBehaviour
{
    [Header("Grid Settings")]
    public Vector2Int gridSize;
    public float radius = 1f;
    public float outerSize = 1f;


    int waterLow, waterUp, waterLandLow, waterLandUp;

    //public GameObject Prefab
    public bool isFlatTopped;

    public HexTileGenerationSettings settings;

    public void Clear()
    {
        List<GameObject> children = new List<GameObject>();

        for(int i = 0; i< transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            children.Add(child);
        }
        
        foreach(GameObject child in children)
        {
            DestroyImmediate(child, true);
        }
    }

    void reformGridSize()
    {
        int x = gridSize.x;
        waterLow = Mathf.RoundToInt(x * 0.15f);
        waterLandLow = Mathf.RoundToInt(x * 0.2f);
        waterUp = gridSize.x - waterLow;
        waterLandUp = gridSize.x - waterLandLow;
    }

    public void LayoutGrid()
    {
        Clear();
        reformGridSize();
        for (int y = 0; y < gridSize.y; y++)
        {
            for (int x = 0; x < gridSize.x; x++)
            {
                GameObject tile = new GameObject($"Hex C{x},R{y}");
                tile.transform.position = GetPositionForHexFromCoordinate(new Vector2Int(x, y));


                HexTile hextile = tile.AddComponent<HexTile>();
                hextile.settings = settings;
                if(y < waterLow || x < waterLow || y >= waterUp || x >= waterUp)
                    hextile.RollTileType(0,1);
                else if(y == waterLandLow || x == waterLandLow || y == waterLandUp || x == waterLandUp)
                    hextile.RollTileType(0,3);
                else if(y >= waterLandLow+1 || x >= waterLandLow+1)
                    hextile.RollTileType(1, 2);
                hextile.AddTile();

                tile.transform.SetParent(transform,true);
                //hextile.offsetCordinates = new Vector2Int(x, y);

                //hextile.cubeCoordinates = Utilities.OffsetToCube(hextile.offsetCordinates);
            }
        }
    }
    public Vector3 GetPositionForHexFromCoordinate(Vector2Int coordinate)
    {
        int column = coordinate.x;
        int row = coordinate.y;
        float width;
        float height;
        float xPosition;
        float yPosition;
        bool shouldOffset;
        float horizontalDistance;
        float verticalDistance;
        float offset;
        float size = outerSize;

        if (!isFlatTopped)
        {
            shouldOffset = (row % 2) == 0;
            width = Mathf.Sqrt(3) * size;
            height = 2f * size;

            horizontalDistance = width;
            verticalDistance = height * (3f / 4f);

            offset = (shouldOffset) ? width / 2 : 0;

            xPosition = (column * (horizontalDistance)) + offset;
            yPosition = (row * (verticalDistance));
        }
        else
        {
            shouldOffset = (column % 2) == 0;
            width = 2f * size;
            height = Mathf.Sqrt(3f) * size;

            horizontalDistance = width * (3f / 4f);
            verticalDistance = height;


            offset = (shouldOffset) ? height / 2 : 0;

            xPosition = (column * (horizontalDistance));
            yPosition = (row * (verticalDistance)) - offset;
        }
        return new Vector3(xPosition, 0, -yPosition);
    }

}


