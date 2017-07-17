using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;

namespace CritiCalEdgePuzzle
{
	public static class Utility
	{
		/*
		 * parses input and identifies nodes and edges.
		 * puts node in NODE list
		 * puts edges in edges list
		*/
		public static void parseInput(this string input1, List<Node> nodeList, List<Edge> edges)
		{
			string pattern = @"^[a-zA-Z]+$";
			Regex regex = new Regex(pattern);
			int counter = 0;
			Char[] inputArray = input1.ToCharArray();
			for (int j = 2; j < inputArray.Length; j++)
			{
				if (regex.IsMatch(inputArray[j].ToString()))
				{
					if (counter == 0)
					{
						nodeList.Add(
							new Node
							{
								NodeName = inputArray[j].ToString()
							});
					}
					else
					{
						var ver1 = inputArray[j].ToString();
						var ver2 = inputArray[j + 2].ToString();
						edges.Add(
							new Edge
							{
								vertex1 = ver1,
								vertex2 = ver2,
								IsActive = true
							});
						nodeList.FirstOrDefault(z => z.NodeName == ver1).degree = ++nodeList.FirstOrDefault(z => z.NodeName == ver1).degree;
						nodeList.FirstOrDefault(z => z.NodeName == ver2).degree = ++nodeList.FirstOrDefault(z => z.NodeName == ver2).degree;
						j += 2;
					}

				}
				if (inputArray[j].ToString() == "}")
					counter++;
			}

		}

	}
}
