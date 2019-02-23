using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    public Tilemap _tilemap;
    public Tilemap _obstacles;
    public float _movementTime;
    private PathFinder _pathFinder;
    private Sequence _currentSequence;

    private void Start()
    {
        var pathHeuristic = new ManhattanHeuristic();
        var graph = new TilemapGraph(_tilemap);
        var graphNodeCost = new NodeCostChainOfResponsibilities(new List<IGraphNodeCost>
        {
            new ObstacleGraphNodeCost(_obstacles),
            new FloorGraphNodeCost(_tilemap)
        });
        _pathFinder = new PathFinder(graph, graphNodeCost, pathHeuristic);
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {            
            var mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var cellPosition = _tilemap.WorldToCell(mouseWorldPosition);
            
            Debug.Log($"floorPos={_tilemap.WorldToCell(mouseWorldPosition)}, " +
                      $"obstaclePos={_obstacles.WorldToCell(mouseWorldPosition)}");

            // TODO: You can still click on non-passable tiles and can walk into them
            if (_tilemap.HasTile(cellPosition) == false)
                return;

            var targetCellPosition = cellPosition;
            var currentCellPosition = _tilemap.WorldToCell(gameObject.transform.position);

            var currentPath = _pathFinder.GetPath(currentCellPosition, targetCellPosition);
            
            var currentPathWorldPositions = new List<Vector3>();
            foreach (var pos in currentPath)
            {
                var cellWorldPosition = _tilemap.CellToWorld(pos);
                var cellCenterWorldPosition = _tilemap.GetCellCenterWorld(pos);          
                currentPathWorldPositions.Add(new Vector3(cellCenterWorldPosition.x, cellWorldPosition.y, 0));
            }

            // Always start from the current world position, this will prevent snapping back to middle of cell.
            if (currentPathWorldPositions.Count > 0)
                currentPathWorldPositions[0] = gameObject.transform.position;
            
            float pathTravelTime = currentPathWorldPositions.Count * _movementTime;
            
            _currentSequence?.Kill();
            _currentSequence = DOTween.Sequence();
            _currentSequence.Append(gameObject.transform.DOPath(currentPathWorldPositions.ToArray(), pathTravelTime)
                .SetEase(Ease.Linear));
        }
    }
}