using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public class AStarSearch<TNode> where TNode : class
{
    public TNode Success { get; private set; }
    private Func<TNode, IEnumerable<TNode>> _getNeighbors;
    private Func<TNode, float> _getCost;
    private Func<TNode, float> _getHeuristic;
    private Func<TNode, bool> _getSuccess; 
    private IEnumerable<TNode> _openNodes;
    private TNode _currentNode;
    private int _maxIter, _currentIter; 

    public AStarSearch(TNode start, Func<TNode, IEnumerable<TNode>> getNeighbors,
        Func<TNode, float> getCost, Func<TNode, float> getHeuristic, 
        Func<TNode, bool> getSuccess, int maxIter)
    {
        _getNeighbors = getNeighbors;
        _getHeuristic = getHeuristic;
        _getSuccess = getSuccess;
        _getCost = getCost;
        _openNodes = new List<TNode>{start};
        _maxIter = maxIter;
        _currentIter = 0;
    }
    public IEnumerable<TNode> CycleAndGetOpenNodes()
    {
        if(_openNodes.Count() > 0) Cycle();
        return _openNodes.ToList();
    }
    public TNode DoSearch()
    {
        while (_openNodes.Count() > 0 && _currentIter < _maxIter)
        {
            var validToContinue = Cycle();
            if (Success != null) return Success;
            if (validToContinue == false) break;
        }

        return null;
    }

    private bool Cycle()
    {
        _currentIter++;
        _currentNode = _openNodes.First();
        _openNodes = _openNodes.Skip(1);
        if (_getSuccess(_currentNode))
        {
            Success = _currentNode;
            return true; 
        }
        var neighbors = _getNeighbors(_currentNode);
        var success = neighbors.Where(n => _getSuccess(n)).FirstOrDefault();
        if (success != null)
        {
            Success = success;
            return true; 
        }
        
        _openNodes = _openNodes.Union(neighbors);
        if (_openNodes.Count() == 0) return false;
        _openNodes = _openNodes.OrderBy(n => _getCost(n) + _getHeuristic(n));
        return true; 
    }
}
