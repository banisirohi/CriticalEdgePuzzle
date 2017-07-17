using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;

namespace CritiCalEdgePuzzle
{
	class Process
	{

		List<Node> nodeList = new List<Node>();
		List<Edge> edges = new List<Edge>();
		List<string> criticalBridgeNodes = new List<string>();
		string jumpNode;
		string initialNode;

		/*
		 * iterates on 1 node in each edge and call tree traverse if all nodes visited
		 * if all nodes not visited, its a critical bridge
		*/
		public List<string> findCriticalBridges(string input1)
		{
			List<string> criticalEdgesResult = new List<string>();
			nodeList = new List<Node>();
			edges = new List<Edge>();
			criticalBridgeNodes = new List<string>();
			jumpNode = string.Empty;
			initialNode = string.Empty;
			input1.parseInput(nodeList, edges);

			foreach (Edge edge in edges)
			{
				var degreeOfVertex1 = nodeList.FirstOrDefault(c => c.NodeName == edge.vertex1).degree;
				var degreeOfVertex2 = nodeList.FirstOrDefault(c => c.NodeName == edge.vertex2).degree;

				--degreeOfVertex1;
				--degreeOfVertex2;

				if (degreeOfVertex1 > 0)
					startTraversingNodesForSelectedEdge(edge.vertex1, edge.vertex2);
				if (nodeList.Any(x => !x.isVisited))
					criticalEdgesResult.Add(edge.vertex1 + "," + edge.vertex2);
				nodeList.ForEach(a => a.isVisited = false);
				initialNode = string.Empty;
			}

			return criticalEdgesResult;

		}

		public void startTraversingNodesForSelectedEdge(string selectedNode, string excludedNode)
		{
			initialNode = selectedNode;
			jumpNode = excludedNode;
			traverseTreeForEachNode(nodeList.Where(z => z.NodeName.Equals(initialNode)).ToList());
		}

		public void traverseTreeForEachNode(List<Node> Nodes)
		{
			foreach (var node in Nodes)
			{
				TraverseTree(node);
			}
		}

		/*
		 * recursive function to traverse all nodes stating from input Node
		 * set isVisited=true for node which is reachable
		*/
		public void TraverseTree(Node node)
		{
			nodeList.FirstOrDefault(c => c.NodeName == node.NodeName).isVisited = true;
			List<string> edgesFromNode = new List<string>();

			criticalBridgeNodes = nodeList.Where(g => g.isVisited).Select(i => i.NodeName).ToList();

			if (criticalBridgeNodes != null || criticalBridgeNodes.Count() > 0)
			{
				edgesFromNode = edges.Where(x => x.vertex1 == node.NodeName && !criticalBridgeNodes.Contains(x.vertex2) && jumpNode != x.vertex2).Select(y => y.vertex2).ToList();
				edgesFromNode.AddRange(edges.Where(x => x.vertex2 == node.NodeName && !criticalBridgeNodes.Contains(x.vertex1) && jumpNode != x.vertex1).Select(y => y.vertex1).ToList());
			}
			else
			{
				edgesFromNode = edges.Where(x => x.vertex1 == node.NodeName && jumpNode != x.vertex2).Select(y => y.vertex2).ToList();
				edgesFromNode.AddRange(edges.Where(x => x.vertex2 == node.NodeName && jumpNode != x.vertex1).Select(y => y.vertex1).ToList());
			}
			if (initialNode == node.NodeName) edgesFromNode.Remove(jumpNode);

			List<Node> nodesToTraverse = (nodeList != null || nodeList.Count() > 0) ? nodeList.Where(z => !z.isVisited && edgesFromNode.Contains(z.NodeName)).ToList() : null;
			if (nodesToTraverse == null || nodesToTraverse.Count() == 0)
				return;
			nodeList.Where(e => edgesFromNode.Contains(e.NodeName)).ToList().ForEach(b => b.isVisited = true);
			jumpNode = string.Empty;

			traverseTreeForEachNode(nodesToTraverse);
		}

	}
}

