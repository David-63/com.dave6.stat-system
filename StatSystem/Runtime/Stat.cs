using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace StatSystem
{
    public class Stat
    {
        protected StatDefinition definition;
        protected int _value;
        protected List<StatModifier> modifiers = new();


        public int Value { get => _value; private set => _value = value; }
        public virtual int BaseValue { get => definition.BaseValue; protected set {} }
        public event System.Action ValueChange;                 // 별 일 없으면 UnityAction으로 변경해도 될듯

        public Stat(StatDefinition definition)
        {
            this.definition = definition;
            CalculateValue();
        }

        public void AddModifier(StatModifier modifier)
        {
            modifiers.Add(modifier);
            CalculateValue();
        }
        public void RemoveModifierFromSource(Object source)
        {
            modifiers = modifiers.Where(m => m.Source.GetInstanceID() != source.GetInstanceID()).ToList();
            CalculateValue();
        }

        protected void CalculateValue()
        {
            int nextValue = BaseValue;
            modifiers.Sort((x, y) => x.Type.CompareTo(y.Type));


            // base에 각 모디파이어 요소 적용
            foreach (var modifier in modifiers)
            {
                if (modifier.Type == ModifierOperationType.Additive)
                {
                    nextValue += modifier.Magnitude;
                }
                else if (modifier.Type == ModifierOperationType.Multiplicative)
                {
                    nextValue *= modifier.Magnitude;
                }
            }

            // 최대 값 제한
            if (definition.Cap >= 0)
            {
                nextValue = Mathf.Min(nextValue, definition.Cap);
            }

            // 계산 내용 반영
            if (Value != nextValue)
            {
                Value = nextValue;
                ValueChange?.Invoke();
            }
        }
    }
}