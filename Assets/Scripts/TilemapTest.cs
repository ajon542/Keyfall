using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using DG.Tweening;

public class TilemapTest : MonoBehaviour
{
    public Tilemap _tilemap;
    private PathFinder _pathFinder;
    private Sequence _currentSequence;

    private void Start()
    {
        var pathHeuristic = new ManhattanHeuristic();
        var weightedGraph = new TilemapWeightedGraph(_tilemap);
        _pathFinder = new PathFinder(weightedGraph, pathHeuristic);
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            var mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var cellPosition = _tilemap.WorldToCell(mouseWorldPosition);

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
            
            float pathTravelTime = currentPathWorldPositions.Count * 0.1f;
            
            _currentSequence?.Kill();
            _currentSequence = DOTween.Sequence();
            _currentSequence.Append(gameObject.transform.DOPath(currentPathWorldPositions.ToArray(), pathTravelTime)
                .SetEase(Ease.Linear));
        }
    }
}