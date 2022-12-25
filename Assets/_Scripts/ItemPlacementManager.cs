using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemPlacementManager : MonoBehaviour
{
    [SerializeField] private GameObject[] itemsToPlace;
    [SerializeField] private GameObject[] itemsToHang;
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject water;

    HashSet<Vector3Int> nearWallTilesUp;
    HashSet<Vector3Int> nearWallTilesDown;
    HashSet<Vector3Int> nearWallTilesRight;
    HashSet<Vector3Int> nearWallTilesLeft;
    HashSet<Vector3Int> nearWallTiles;
    HashSet<Vector3Int> innerTiles;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void PlaceItems(HashSet<Vector2Int> floorPositions)
    {
        //HashSet<Vector2Int> itemPositions = new HashSet<Vector2Int>();
        //itemPositions.UnionWith(floorPositions);

        nearWallTilesUp=new HashSet<Vector3Int>();
        nearWallTilesDown=new HashSet<Vector3Int>();
        nearWallTilesRight = new HashSet<Vector3Int>();
        nearWallTilesLeft = new HashSet<Vector3Int>();
        nearWallTiles = new HashSet<Vector3Int>();
        innerTiles = new HashSet<Vector3Int>();

        foreach (Vector2Int pos in floorPositions)
        {
            if (!floorPositions.Contains(pos + Vector2Int.up))
            {
                if(floorPositions.Contains(pos + Vector2Int.down)&& floorPositions.Contains(pos + Vector2Int.right)&&
                    floorPositions.Contains(pos + Vector2Int.left))
                {
                    //Debug.Log("11111111111111111");
                    nearWallTilesUp.Add((Vector3Int)pos);
                    nearWallTiles.Add((Vector3Int)pos);
                }
            }
            else if(!floorPositions.Contains(pos + Vector2Int.down))
            {
                if (floorPositions.Contains(pos + Vector2Int.up) && floorPositions.Contains(pos + Vector2Int.right) &&
                    floorPositions.Contains(pos + Vector2Int.left))
                {
                    //Debug.Log("222222222222");
                    nearWallTilesDown.Add((Vector3Int)pos);
                }
            }
            else if (!floorPositions.Contains(pos + Vector2Int.right))
            {
                if (floorPositions.Contains(pos + Vector2Int.up) && floorPositions.Contains(pos + Vector2Int.down) &&
                    floorPositions.Contains(pos + Vector2Int.left))
                {
                    //Debug.Log("3333333333");
                    nearWallTilesRight.Add((Vector3Int)pos);
                    nearWallTiles.Add((Vector3Int)pos);
                }
            }
            else if (!floorPositions.Contains(pos + Vector2Int.left))
            {
                if (floorPositions.Contains(pos + Vector2Int.up) && floorPositions.Contains(pos + Vector2Int.down) &&
                    floorPositions.Contains(pos + Vector2Int.right))
                {
                    //Debug.Log("444444444444");
                    nearWallTilesLeft.Add((Vector3Int)pos);
                    nearWallTiles.Add((Vector3Int)pos);
                }
            }
            else
            {
                //Debug.Log("55555555555");
                innerTiles.Add((Vector3Int)pos);
            }
        }
        Debug.Log("nearWallTilesUp " + nearWallTilesUp.Count);
        Debug.Log("nearWallTilesDown " + nearWallTilesDown.Count);
        Debug.Log("nearWallTilesRight " + nearWallTilesRight.Count);
        Debug.Log("nearWallTilesLeft " + nearWallTilesLeft.Count);
        Debug.Log("nearWallTiles " + nearWallTiles.Count);
        Debug.Log("innerTiles " + innerTiles.Count);
        PlaceItems();
    }

    private void PlaceItems()
    {
        int itemCount = Random.Range(7, 10);
        int itemHangCount = Random.Range(4, 6);
        int enemyCount = Random.Range(4, 7);
        int waterCount = Random.Range(1, 4);

        for(int i = 0; i < itemCount; i++)
        {
            var position = innerTiles.ElementAt(Random.Range(0, innerTiles.Count));
            Instantiate(itemsToPlace[Random.Range(0,itemsToPlace.Length)], position, Quaternion.identity);
            innerTiles.Remove(position);
        }
        for(int i = 0; i < itemHangCount; i++)
        {
            var position = nearWallTilesUp.ElementAt(Random.Range(0, nearWallTilesUp.Count));
            Instantiate(itemsToHang[Random.Range(0, itemsToHang.Length)], position+Vector3Int.up, Quaternion.identity);
            nearWallTilesUp.Remove(position);
        }
        for (int i = 0; i < waterCount; i++)
        {
            var position = nearWallTiles.ElementAt(Random.Range(0, nearWallTiles.Count));
            Instantiate(water, position, Quaternion.identity);
            nearWallTiles.Remove(position);
        }
        for (int i = 0; i < enemyCount; i++)
        {
            var position = innerTiles.ElementAt(Random.Range(0, innerTiles.Count));
            Instantiate(enemy, position, Quaternion.identity);
            innerTiles.Remove(position);
        }

    }
}
