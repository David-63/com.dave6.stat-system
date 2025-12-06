namespace StatSystem
{
    public class Attribute : BaseStat
    {
        public Attribute(StatDefinition definition) : base(definition) { }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override int CalculateBase()
        {
            return baseValue;
        }

    }
}