using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;


namespace Core.Nodes
{
    public class PowerNode : IntermediateNode
    {
        [HideInInspector] public CodeFunctionNode @base;
        [HideInInspector] public CodeFunctionNode exponent;

        public override float value => (float)Math.Pow(@base.value, exponent.value);

        public override ReadOnlyCollection<CodeFunctionNode> children
        {
            get
            {
                List<CodeFunctionNode> nodes = new();
                if (exponent != null)
                {
                    nodes.Add(exponent);
                }
                if (@base != null)
                {
                    nodes.Add(@base);
                }

                return nodes.AsReadOnly();
            }
        }

        public override void AddChild(CodeFunctionNode child, string portName)
        {
            if (portName.Equals("A"))
            {
                @base = child;
            }
            else
            {
                exponent = child;
            }
        }

        public override void RemoveChild(CodeFunctionNode child, string portName)
        {
            if (portName.Equals("A"))
            {
                @base = null;
            }
            else
            {
                exponent = null;
            }
        }
    }
}