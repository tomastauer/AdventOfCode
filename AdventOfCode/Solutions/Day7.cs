using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions
{
    using System.Text.RegularExpressions;

    internal class Day7 : Solution
    {
        protected override string RunInternalPart1(string input)
        {
            var tree = this.BuildTree(input);
            return tree.Single(c => c.Value.Parent == null).Value.ToString();
        }

        protected override string RunInternalPart2(string input)
        {
            var tree = this.BuildTree(input);
            var root = tree.Single(c => c.Value.Parent == null).Value;
            while (GetNextUnbalanced(root) != null)
            {
                root = GetNextUnbalanced(root);
            }
            root = root.Parent;

            return (GetUnbalancedNode(root).Weight - GetDifference(root)).ToString();
        }

        private Node GetUnbalancedNode(Node root)
        {
            return root.Children.Single(c => c.TotalWeight != GetMeanNode(root).TotalWeight);
        }

        private int GetDifference(Node root)
        {
            return GetUnbalancedNode(root).TotalWeight - GetMeanNode(root).TotalWeight;
        }

        private Node GetMeanNode(Node root)
        {
            return root.Children.OrderBy(c => c.TotalWeight).ElementAt(1);
        }
 
        private Node GetNextUnbalanced(Node root)
        {
            return root.Children.SingleOrDefault(c =>
                c.TotalWeight != root.Children.OrderBy(d => d.TotalWeight).ElementAt(1).TotalWeight);
        }

        private Dictionary<string, Node> BuildTree(string input)
        {
            var tree = new Dictionary<string, Node>();

            var matches = input.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).Select(line => Regex.Match(line, @"(?<name>[a-z]+)\s\((?<weight>[\d]+)\)(\s->\s)?(?<children>[a-z,\s]*)?"));
            foreach (var match in matches)
            {
                Node node;
                var name = match.Groups["name"].Value;
                var weight = int.Parse(match.Groups["weight"].Value);
                var children = match.Groups["children"].Value;

                if (tree.ContainsKey(name))
                {
                    node = tree[name];
                    node.Weight = weight;
                }
                else
                {
                    node = new Node(match.Groups["name"].Value)
                    {
                        Weight = weight
                    };
                    tree.Add(name, node);
                }

                foreach (var child in children.Split(',').Select(c => c.Trim()))
                {
                    if (tree.ContainsKey(child))
                    {
                        node.Children.Add(tree[child]);
                        tree[child].Parent = node;
                    }
                    else
                    {
                        var childNode = new Node(child)
                        {
                            Parent = node
                        };
                        tree.Add(child, childNode);
                        node.Children.Add(childNode);
                    }
                }
            }

            return tree;
        }

        private class Node
        {
            private IList<Node> children;

            public Node(string name)
            {
                this.Name = name;
            }

            private string Name { get; }
            public int Weight { get; set; }

            public IList<Node> Children
            {
                get => this.children ?? (this.children = new List<Node>());
                set => this.children = value;
            }

            public Node Parent { get; set; }

            public override int GetHashCode()
            {
                return this.Name.GetHashCode();
            }

            private int totalWeight;

            public int TotalWeight
            {
                get
                {
                    if (totalWeight == 0)
                    {
                        totalWeight = Children.Sum(c => c.TotalWeight) + Weight;
                    }
                    return totalWeight;
                }
            }
           
            public override string ToString()
            {
                return this.Name;
            }
        }
    }
}
