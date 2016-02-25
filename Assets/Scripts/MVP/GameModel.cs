﻿using UnityEngine;
using System;
using System.Collections.Generic;


/// <summary>
/// Concrete implementation of the IGameModel abstract base class.
/// </summary>
public class GameModel : IGameModel
{
    /// <summary>
    /// Represents the relationship between each of the rooms.
    /// </summary>
    //private IGraph<Room> roomGraph;

    private List<string>[,] townLayout;

    public int Width { get; private set; }
    public int Length { get; private set; }

    private Player player;

    IWeightedGraph<Location> grid;
    PathFinder finder;
    List<Location> path;

    /// <summary>
    /// Initialize the dungeon level.
    /// </summary>
    /// <param name="presenter">The game presenter.</param>
    public override void Initialize(Presenter presenter)
    {
        // Let the base class do its thing.
        base.Initialize(presenter);

        ILevelGenerator townGenerator = new Town();
        townLayout = townGenerator.GenerateLevel(60, 40);

        //GenerateDungeon generateDungeon = new GenerateDungeon();
        //generateDungeon.DungeonLayout = townLayout;
        //presenter.PublishMsg(generateDungeon);

        GenerateTown generateTown = new GenerateTown();
        generateTown.TownLayout = townLayout;
        presenter.PublishMsg(generateTown);

        player = new Player();
        player.Position = new Location(0, 0);
        PlayerPosition playerPosition = new PlayerPosition();
        playerPosition.Position = player.Position;
        presenter.PublishMsg(playerPosition);

        grid = new DungeonGrid(60, 40);
        finder = new PathFinder(grid);

        path = finder.GetPath(new Location(0, 0), new Location(50, 30));
    }

    /// <summary>
    /// Update the players position and send the result to the player view if valid.
    /// </summary>
    /// <param name="location">The new player location</param>
    private void UpdatePlayerPosition(Location location)
    {
        Location newPosition = new Location(location.x, location.y);

        // TODO: Error! Prevent the player from stepping off the dungeon floor.
        //if (dungeonLayout[newPosition.x, newPosition.y] != DungeonLayout.Floor)
        //if(townLayout.Is)
        //{
        //    return;
        //}

        // Update the player position.
        player.Position = newPosition;

        // Send the updated position to the view.
        PlayerPosition playerPosition = new PlayerPosition();
        playerPosition.Position = player.Position;
        presenter.PublishMsg(playerPosition);
    }

    private int count = 0;
    private float gameSpeed = 0;
    public override void UpdateModel()
    {
        // TODO: This is sufficient for now, but need to make the camera movement smoother.
        gameSpeed += Time.deltaTime;
        if(gameSpeed < 0.2f)
        {
            return;
        }
        
        gameSpeed = 0;

        if(count >= path.Count)
        {
            return;
        }
        Location next = path[count++];
        UpdatePlayerPosition(next);
    }
}