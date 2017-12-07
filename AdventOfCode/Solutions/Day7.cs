﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


        }

        private bool TraverseTree(Node input)
        {
            foreach (var child in input.Children)
            {
                if (child.Children.Count > 0)
                {
                    
                }
            }
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

            public override string ToString()
            {
                return this.Name;
            }
        }
    }
}