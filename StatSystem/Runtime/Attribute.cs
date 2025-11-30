using UnityEngine;

namespace StatSystem
{
    public class Attribute : Stat
    {
        protected int _currentValue;
        public int CurrentValue { get => _currentValue; private set => _currentValue = value; }
        public event System.Action CurrentValueChanged;
        public event System.Action<StatModifier> AppliedModifier;
        
        public Attribute(StatDefinition definition) : base(definition)
        {
            CurrentValue = Value;
        }

        public virtual void ApplyModifier(StatModifier modifier)
        {
            int nextValue = CurrentValue;

            switch (modifier.Type)
            {
                case ModifierOperationType.Additive:
                nextValue += modifier.Magnitude;
                break;

                case ModifierOperationType.Multiplicative:
                nextValue *= modifier.Magnitude;
                break;
                case ModifierOperationType.Override:
                nextValue = modifier.Magnitude;
                break;
            }
            nextValue = Mathf.Clamp(nextValue, 0, Value);

            if (CurrentValue != nextValue)
            {
                CurrentValue = nextValue;
                CurrentValueChanged?.Invoke();
                AppliedModifier?.Invoke(modifier);
            }
        }
    }
}