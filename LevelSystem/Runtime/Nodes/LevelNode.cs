using Core.Nodes;
using StatSystem;

namespace LevelSystem.Nodes
{
    public class LevelNode : CodeFunctionNode
    {
        public ILevelable levelable;
        public override float value => levelable.level;
    }
}
