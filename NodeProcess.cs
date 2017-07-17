using System;
using System.Collections.Generic;
using System.Linq;

namespace CritiCalEdgePuzzle
{
	public class NodeProcess
	{
		List<Node> nodeList = new List<Node>();
		List<Edge> edges = new List<Edge>();
		List<string> criticalBridgeNodes = new List<string>();
		List<string> edgesFromNode;
		List<string> criticalEdgesResult = new List<string>();

		public List<string> findCriticalBridges(string input1)
		{
			nodeList = new List<Node>();
			edges = new List<Edge>();
			criticalBridgeNodes = new List<string>();
			criticalEdgesResult = new List<string>();

			input1.parseInput(nodeList, edges);
			edges.ForEach( x => 
								{ 
									x.IsActive = false; 
									TraverseTree(nodeList.FirstOrDefault()); 
									if (nodeList.Any( y => !y.isVisited))
										criticalEdgesResult.Add(x.vertex1 + "," + x.vertex2);
									nodeList.ForEach(a => a.isVisited = false);
									x.IsActive = true;
								});

			return criticalEdgesResult;
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
			edgesFromNode = new List<string>();

			criticalBridgeNodes = nodeList.Where(g => g.isVisited ).Select(i => i.NodeName).ToList();

			if (criticalBridgeNodes != null || criticalBridgeNodes.Count() > 0)
			{
				edgesFromNode = edges.Where(x => x.vertex1 == node.NodeName && !criticalBridgeNodes.Contains(x.vertex2) && x.IsActive).Select(y => y.vertex2).ToList();
				edgesFromNode.AddRange(edges.Where(x => x.vertex2 == node.NodeName && !criticalBridgeNodes.Contains(x.vertex1) && x.IsActive).Select(y => y.vertex1).ToList());
			}
			else
			{
				edgesFromNode = edges.Where(x => x.vertex1 == node.NodeName && x.IsActive).Select(y => y.vertex2).ToList();
				edgesFromNode.AddRange(edges.Where(x => x.vertex2 == node.NodeName && x.IsActive).Select(y => y.vertex1).ToList());
			}

			List<Node> nodesToTraverse = (nodeList != null || nodeList.Count() > 0) ? nodeList.Where(z => !z.isVisited && edgesFromNode.Contains(z.NodeName)).ToList() : null;
			if (nodesToTraverse == null || nodesToTraverse.Count() == 0)
				return;
			nodeList.Where(e => edgesFromNode.Contains(e.NodeName)).ToList().ForEach(b => b.isVisited = true);
			traverseTreeForEachNode(nodesToTraverse);
		}

	}
}
