﻿using RAIN.Navigation.Graph;
using UnityEngine;

namespace Assets.Scripts.IAJ.Unity.Pathfinding.Heuristics
{
    public class EuclideanHeuristic : IHeuristic
    {
        public float H(NavigationGraphNode node, NavigationGraphNode goalNode)
        {
            return Mathf.Sqrt((goalNode.Position.x-node.Position.x)* (goalNode.Position.x - node.Position.x) + (goalNode.Position.y-node.Position.y)*(goalNode.Position.y - node.Position.y)) ;
        }
    }
}
