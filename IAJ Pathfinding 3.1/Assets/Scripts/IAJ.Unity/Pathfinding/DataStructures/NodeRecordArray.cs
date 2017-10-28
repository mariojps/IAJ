using System;
using System.Collections.Generic;
using RAIN.Navigation.Graph;

namespace Assets.Scripts.IAJ.Unity.Pathfinding.DataStructures
{
    public class NodeRecordArray : IOpenSet, IClosedSet
    {
        private NodeRecord[] NodeRecords { get; set; }
        private List<NodeRecord> SpecialCaseNodes { get; set; }
        private NodePriorityHeap Open { get; set; }

        public NodeRecordArray(List<NavigationGraphNode> nodes)
        {
            //this method creates and initializes the NodeRecordArray for all nodes in the Navigation Graph
            this.NodeRecords = new NodeRecord[nodes.Count];

            for (int i = 0; i < nodes.Count; i++)
            {
                var node = nodes[i];
                node.NodeIndex = i; //we're setting the node Index because RAIN does not do this automatically
                this.NodeRecords[i] = new NodeRecord { node = node, status = NodeStatus.Unvisited };
            }

            this.SpecialCaseNodes = new List<NodeRecord>();

            this.Open = new NodePriorityHeap();
        }

        public NodeRecord GetNodeRecord(NavigationGraphNode node)
        {
            //do not change this method
            //here we have the "special case" node handling
            if (node.NodeIndex == -1)
            {
                for (int i = 0; i < this.SpecialCaseNodes.Count; i++)
                {
                    if (node == this.SpecialCaseNodes[i].node)
                    {
                        return this.SpecialCaseNodes[i];
                    }
                }
                return null;
            }
            else
            {
                return this.NodeRecords[node.NodeIndex];
            }
        }

        public void AddSpecialCaseNode(NodeRecord node)
        {
            this.SpecialCaseNodes.Add(node);
        }

        void IOpenSet.Initialize()
        {
            this.Open.Initialize();
            //we want this to be very efficient (that's why we use for)
            for (int i = 0; i < this.NodeRecords.Length; i++)
            {
                this.NodeRecords[i].status = NodeStatus.Unvisited;
            }

            this.SpecialCaseNodes.Clear();
        }

        void IClosedSet.Initialize()
        {
            //Does nothing. Cause we don't need it.
            //TODO implement
        }

        public void AddToOpen(NodeRecord nodeRecord)
        {
            nodeRecord.status = NodeStatus.Open;
            Open.AddToOpen(nodeRecord);
            //TODO implement
        }

        public void AddToClosed(NodeRecord nodeRecord)
        {
            if (nodeRecord.status == NodeStatus.Open)
                Open.RemoveFromOpen(nodeRecord);
            nodeRecord.status = NodeStatus.Closed;
            //TODO implement
        }

        public NodeRecord SearchInOpen(NodeRecord nodeRecord)
        {
            return Open.SearchInOpen(nodeRecord);
            //TODO implement
        }

        public NodeRecord SearchInClosed(NodeRecord nodeRecord)
        {
            foreach (NodeRecord nR in NodeRecords)
            {
                if (nR.Equals(nodeRecord) && nodeRecord.status == NodeStatus.Closed)
                {
                    return nR;
                }
            }
            return null;
            //TODO implement
        }

        public NodeRecord GetBestAndRemove()
        {
            return Open.GetBestAndRemove();
            //TODO implement
        }

        public NodeRecord PeekBest()
        {
            return Open.PeekBest();
            //TODO implement
        }

        public void Replace(NodeRecord nodeToBeReplaced, NodeRecord nodeToReplace)
        {
            Open.Replace(nodeToBeReplaced, nodeToReplace);
            //TODO implement
        }

        public void RemoveFromOpen(NodeRecord nodeRecord)
        {
            Open.RemoveFromOpen(nodeRecord);
            //TODO implement
        }

        public void RemoveFromClosed(NodeRecord nodeRecord)
        {
            //There is no closed... What is the point?
            //TODO implement
        }

        ICollection<NodeRecord> IOpenSet.All()
        {
            return Open.All();
            //TODO implement
        }

        ICollection<NodeRecord> IClosedSet.All()
        {
            NodePriorityHeap Closed = new NodePriorityHeap();
            foreach (NodeRecord nR in NodeRecords)
            {
                if (nR.status == NodeStatus.Closed)
                    Closed.AddToOpen(nR);
            }
            return Closed.All();
            //TODO implement
        }

        public int CountOpen()
        {
           return Open.CountOpen();
            //TODO implement
        }
    }
}
