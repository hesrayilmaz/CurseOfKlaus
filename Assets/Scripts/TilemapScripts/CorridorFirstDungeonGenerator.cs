using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CorridorFirstDungeonGenerator : SimpleRandomWalkDungeonGenerator
{
    [SerializeField]
    private int corridorLength = 14, corridorCount = 5;
    [SerializeField]
    [Range(0.1f,1)]
    private float roomPercent = 0.8f;


    public ItemPlacementManager itemPlacement;
    private GameObject character;
    public GameObject santa;
    private GameObject santaClone;

    private void Start()
    {
        character = GameObject.FindWithTag("Player");
        tilemapVisualizer.Clear();
        RunProceduralGeneration();
    }

    protected override void RunProceduralGeneration()
    {
        CorridorFirstGeneration();
    }

    private void CorridorFirstGeneration()
    {
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        HashSet<Vector2Int> potentialRoomPositions = new HashSet<Vector2Int>();

        CreateCorridors(floorPositions, potentialRoomPositions);

        HashSet<Vector2Int> roomPositions = CreateRooms(potentialRoomPositions);

        List<Vector2Int> deadEnds = FindAllDeadEnds(floorPositions);

        CreateRoomsAtDeadEnd(deadEnds, roomPositions);

        floorPositions.UnionWith(roomPositions);

        tilemapVisualizer.PaintFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions, tilemapVisualizer);

    }

    private void CreateRoomsAtDeadEnd(List<Vector2Int> deadEnds, HashSet<Vector2Int> roomFloors)
    {
        foreach (var position in deadEnds)
        {
            if(roomFloors.Contains(position) == false)
            {
                var room = RunRandomWalk(randomWalkParameters, position);
                roomFloors.UnionWith(room);
            }
        }
    }

    private List<Vector2Int> FindAllDeadEnds(HashSet<Vector2Int> floorPositions)
    {
        List<Vector2Int> deadEnds = new List<Vector2Int>();
        foreach (var position in floorPositions)
        {
            int neighboursCount = 0;
            foreach (var direction in Direction2D.cardinalDirectionsList)
            {
                if (floorPositions.Contains(position + direction))
                    neighboursCount++;
                
            }
            if (neighboursCount == 1)
                deadEnds.Add(position);
        }
        return deadEnds;
    }

    private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> potentialRoomPositions)
    {
        HashSet<Vector2Int> roomPositions = new HashSet<Vector2Int>();
        int roomToCreateCount = Mathf.RoundToInt(potentialRoomPositions.Count * roomPercent);

        List<Vector2Int> roomsToCreate = potentialRoomPositions.OrderBy(x => Guid.NewGuid()).Take(roomToCreateCount).ToList();
        
        Debug.Log(roomToCreateCount);

        if (santaClone)
            Destroy(santaClone);
        itemPlacement.Reset();

        Vector2Int[] startEndPoints = FindMostDistantPoints(roomsToCreate);
        Vector2Int startPoint = startEndPoints[0];
        Vector2Int endPoint = startEndPoints[1];


        foreach (var roomPosition in roomsToCreate)
        {
            var roomFloor = RunRandomWalk(randomWalkParameters, roomPosition);
            roomPositions.UnionWith(roomFloor);

            if (roomPosition==startPoint)
            {
                character.transform.position = (Vector3Int)roomPosition;
                itemPlacement.PlaceItems(roomFloor, roomsToCreate);
            }
            else if (roomPosition == endPoint)
            {
                santaClone = Instantiate(santa, (Vector3Int)roomPosition, Quaternion.identity);
            }
            else
            {
                itemPlacement.PlaceItems(roomFloor, roomsToCreate);
                itemPlacement.PlaceEnemies(roomFloor);
            }

        }
        return roomPositions;
    }

    private Vector2Int[] FindMostDistantPoints(List<Vector2Int> roomsToCreate)
    {
        float maxDistance = 0;
        Vector2Int[] startEndPoints = new Vector2Int[2];

        for (int i = 0; i < roomsToCreate.Count; i++)
        {
            for(int j = i + 1; j < roomsToCreate.Count; j++)
            {
                if(Vector2.Distance(roomsToCreate.ElementAt(i), roomsToCreate.ElementAt(j))> maxDistance)
                {
                    maxDistance = Vector2.Distance(roomsToCreate.ElementAt(i), roomsToCreate.ElementAt(j));
                    Debug.Log("maxDistance: " + maxDistance);
                    startEndPoints[0] = roomsToCreate.ElementAt(i);
                    startEndPoints[1] = roomsToCreate.ElementAt(j);
                }
            }
        }
        Debug.Log("final distance: " + maxDistance);
        return startEndPoints;
    }

    private void CreateCorridors(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> potentialRoomPositions)
    {
        var currentPosition = startPosition;
        potentialRoomPositions.Add(currentPosition);

        for (int i = 0; i < corridorCount; i++)
        {
            var corridor = ProceduralGenerationAlgorithms.RandomWalkCorridor(currentPosition, corridorLength);
            currentPosition = corridor[corridor.Count - 1];
            potentialRoomPositions.Add(currentPosition);
            floorPositions.UnionWith(corridor);
        }
    }
}
