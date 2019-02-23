using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    public Tilemap _floor;
    public Tilemap _obstacles;
    public Tilemap _collectables;
    
    public float _movementTime;
    private PathFinder _pathFinder;
    private Sequence _currentSequence;
    private NodeCostChainOfResponsibilities _graphNode;
    private FloorGraphNodeCost _floorNodeCost;
    private ObstacleGraphNodeCost _obstacleNodeCost;

    private void Start()
    {
        var pathHeuristic = new ManhattanHeuristic();
        var graph = new TilemapGraph(_floor);
        _obstacleNodeCost = new ObstacleGraphNodeCost(_obstacles);
        _floorNodeCost = new FloorGraphNodeCost(_floor);
        _graphNode = new NodeCostChainOfResponsibilities(new List<IGraphNodeCost>
        {
            _obstacleNodeCost,
            _floorNodeCost
        });
        _pathFinder = new PathFinder(graph, _graphNode, pathHeuristic);
    }

    private void Update()
    {
        // Example of collecting something
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetMouseButtonUp(0))
        {
            var mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var mouseCellPosition = _floor.WorldToCell(mouseWorldPosition);
            var playerCellPosition = _floor.WorldToCell(transform.position);

            if (playerCellPosition != mouseCellPosition)
                return;
            
            if (_collectables.HasTile(mouseCellPosition))
            {
                _collectables.SetTile(mouseCellPosition, null);
            }
        }
        
        // Example of pathfinding
        if (Input.GetMouseButtonUp(0))
        {
            var mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var cellPosition = _floor.WorldToCell(mouseWorldPosition);

            var targetCellPosition = cellPosition;
            var currentCellPosition = _floor.WorldToCell(gameObject.transform.position);

            var currentPath = _pathFinder.GetPath(currentCellPosition, targetCellPosition);
            if (currentPath == null)
                return;
            
            // Process the given path to ensure there are no blocking obstacles
            foreach (var node in currentPath)
            {
                if (_graphNode.Cost(node) == _obstacleNodeCost.Cost(node))
                    return;
            }
            
            var currentPathWorldPositions = new List<Vector3>();
            foreach (var pos in currentPath)
            {
                var cellWorldPosition = _floor.CellToWorld(pos);
                var cellCenterWorldPosition = _floor.GetCellCenterWorld(pos);          
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