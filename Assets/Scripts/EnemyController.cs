using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using DG.Tweening;

public class EnemyController : MonoBehaviour
{
    // How doe we stop enemies walking on each other?
    
    public Tilemap _tilemap;
    public PlayerController _player; // TODO: Only temporary
    public float _movementTime;
    private float _reevaluatePathInterval;
    
    private PathFinder _pathFinder;
    private Sequence _currentSequence;

    private void Start()
    {
        var pathHeuristic = new EuclideanHeuristic();
        var weightedGraph = new TilemapWeightedGraph(_tilemap);
        _pathFinder = new PathFinder(weightedGraph, pathHeuristic);
        _reevaluatePathInterval = 3;
    }

    private void Update()
    {
        _reevaluatePathInterval -= Time.deltaTime;
        if (_reevaluatePathInterval < 0)
        {
            _reevaluatePathInterval = 3;
            
            var playerWorldPosition = _player.transform.position;
            var cellPosition = _tilemap.WorldToCell(playerWorldPosition);

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