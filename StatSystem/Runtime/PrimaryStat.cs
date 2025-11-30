using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("StatSystem.Tests")]
namespace StatSystem
{
    public class PrimaryStat : Stat
    {
        int baseValue;
        public override int BaseValue { get => baseValue; protected set => baseValue = value; }

        public PrimaryStat(StatDefinition definition) : base(definition)
        {
            baseValue = definition.BaseValue;
            CalculateValue();
        }

        internal void Add(int amount)
        {
            BaseValue += amount;
            CalculateValue();
        }
        internal void Substract(int amount)
        {
            BaseValue -= amount;
            CalculateValue();
        }
    }
}