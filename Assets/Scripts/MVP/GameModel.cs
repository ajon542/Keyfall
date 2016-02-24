using UnityEngine;
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

    private List<TownLayout>[,] townLayout;

    public int Width { get; private set; }
    public int Length { get; private set; }

    private Player player;

    /// <summary>
    /// Initialize the dungeon level.
    /// </summary>
    /// <param name="presenter">The game presenter.</param>
    public override void Initialize(Presenter presenter)
    {
        // Let the base class do its thing.
        base.Initialize(presenter);

        // TODO: Clean the variables up.
        int maxRoomSize = 10;
        int roomSpread = 3;
        int gridSize = maxRoomSize + roomSpread;
        Width = gridSize * 5;
        Length = gridSize * 5;

        ILevelGenerator townGenerator = new Dungeon(gridSize, 5, 5);
        townLayout = townGenerator.GenerateLevel(Width, Length);

        GenerateDungeon generateDungeon = new GenerateDungeon();
        generateDungeon.DungeonLayout = townLayout;
        generateDungeon.Width = Width;
        generateDungeon.Length = Length;
        presenter.PublishMsg(generateDungeon);

        //GenerateTown generateTown = new GenerateTown();
        //generateTown.TownLayout = townLayout;
        //generateTown.Width = Width;
        //generateTown.Length = Length;
        //presenter.PublishMsg(generateTown);

        player = new Player();
        player.Position = new Location(0, 0);
        PlayerPosition playerPosition = new PlayerPosition();
        playerPosition.Position = player.Position;
        presenter.PublishMsg(playerPosition);
    }

    /// <summary>
    /// Update the players position and send the result to the player view if valid.
    /// </summary>
    /// <param name="location">The new player location</param>
    private void UpdatePlayerPosition(Location location)
    {
        Location newPosition = new Location(
            player.Position.x + location.x,
            player.Position.y + location.y);

        // TODO:
        // Prevent the player from stepping off the dungeon floor.
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

    private float gameSpeed = 0;
    public override void UpdateModel()
    {
        // TODO: This is sufficient for now, but need to make the camera movement smoother.
        gameSpeed += Time.deltaTime;
        if(gameSpeed < 0.1f)
        {
            return;
        }
        gameSpeed = 0;

        if (Input.GetKey(KeyCode.W))
        {
            UpdatePlayerPosition(new Location(0, 1));
        }
        if (Input.GetKey(KeyCode.S))
        {
            UpdatePlayerPosition(new Location(0, -1));
        }
        if (Input.GetKey(KeyCode.A))
        {
            UpdatePlayerPosition(new Location(-1, 0));
        }
        if (Input.GetKey(KeyCode.D))
        {
            UpdatePlayerPosition(new Location(1, 0));
        }
    }
}