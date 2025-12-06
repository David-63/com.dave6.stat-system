using System;
using System.Collections.Generic;
using UnityEngine;

namespace StatSystem
{
    public class ResourceStat : BaseStat, IDerivedStat, IEffectApplicable
    {
        // max(final) / current
        float m_CurrentValue;
        public float currentValue => m_CurrentValue;

        List<BaseStat> m_SourceStats = new();
        public Action onCurrentValueChanged;

        public List<BaseStat> sourceStats
        {
            get => m_SourceStats;
            set => m_SourceStats = value;
        }
        public ResourceStat(StatDefinition definition) : base(definition) { }


        // 이건 DerivedStat과 동일하게 해도 될듯?
        protected override int CalculateBase()
        {
            int value = 0;

            for (int i = 0; i < sourceStats.Count; i++)
            {
                var finalValue = sourceStats[i].finalValue;
                value += Mathf.RoundToInt(finalValue * definition.sources[i].weight);
            }

            return baseValue + value;
        }

        public void SetupSorces(List<BaseStat> sources)
        {
            sourceStats = sources;

            foreach (var stat in sourceStats)
            {
                stat.onValueChanged += MarkDirty;
            }
        }

        public override void CalculateValue()
        {
            // 여기서 베이스 계산됨 (캡은 여기서 적용)
            m_FinalValue = CalculateBase();     // CalculateBase는 상속받아서 구현
            Debug.Log(m_FinalValue);

            // float multipliers 적용하기
            m_FinalValue = ApplyModifiers(m_FinalValue);

            // currentValue 갱신
            m_CurrentValue = m_FinalValue;
            Debug.Log(m_CurrentValue);
        }

        public void ApplyEffect(StatEffect effect)
        {
            float amount = effect.operationType switch
            {
                EffectOperationType.Addition => effect.value,
                EffectOperationType.Subtraction => -effect.value,
                EffectOperationType.PercentCurrentIncrease => currentValue * effect.value,
                EffectOperationType.PercentCurrentDecrease => -currentValue * effect.value,
                EffectOperationType.PercentMaxIncrease => finalValue * effect.value,
                EffectOperationType.PercentMaxDecrease => -finalValue * effect.value,
                _ => 0,
            };

            if (amount > 0)
            {
                Restore(amount);
            }
            else if (amount < 0)
            {
                Consume(amount);
            }

        }

        public void Restore(float amount)
        {
            float prev = m_CurrentValue;
            m_CurrentValue = Mathf.Min(m_CurrentValue + amount, finalValue);

            if (!Mathf.Approximately(prev, m_CurrentValue))
                onCurrentValueChanged?.Invoke();
        }

        public void Consume(float amount)
        {
            float prev = m_CurrentValue;
            m_CurrentValue = Mathf.Max(currentValue + amount, 0f);

            if (!Mathf.Approximately(prev, currentValue))
                onCurrentValueChanged?.Invoke();
        }
    }
}