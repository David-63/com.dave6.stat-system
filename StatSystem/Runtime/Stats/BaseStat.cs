using System.Collections.Generic;
using UnityEngine;

namespace StatSystem
{
    public interface IDerivedStat
    {
        List<BaseStat> sourceStats { get; set; }
        void SetupSorces(List<BaseStat> sources);
    }
    public interface IEffectApplicable
    {
        float currentValue { get; }
        void ApplyEffect(StatEffect effect);
    }
    public abstract class BaseStat
    {
        public StatDefinition definition { get; private set; }
        public int baseValue => definition.baseValue;
        protected int m_FinalValue;                             // Modifier 적용후
        public int finalValue
        {
            get
            {
                if (m_IsDirty)
                {
                    int tempValue = CalculateBase();
                    tempValue = ApplyModifiers(tempValue);

                    if (m_FinalValue != tempValue)
                    {
                        m_FinalValue = tempValue;
                        onValueChanged?.Invoke();
                    }

                    m_IsDirty = false;
                }
                return m_FinalValue;
            }
        }

        bool m_IsDirty = true;
        protected List<StatModifier> m_Modifiers = new();

        public event System.Action onValueChanged;           // <- 이건 CalculateValue 에서 호출해야함

        /// <summary>
        /// 상속받은 객체는 CalculateBase 를 통해서 FinalValue 값을 계산해야함
        /// </summary>
        /// <param name="definition"></param>
        public BaseStat(StatDefinition definition)
        {
            this.definition = definition;
            m_FinalValue = definition.baseValue;
        }

        public virtual void Initialize()
        {
            CalculateValue();
        }

        /**/
        /// <summary>
        /// Modifier 추가 및 삭제시 호출
        /// </summary>
        public virtual void CalculateValue()
        {
            // 여기서 베이스 계산됨 (캡은 여기서 적용)
            m_FinalValue = CalculateBase();     // CalculateBase는 상속받아서 구현

            // float multipliers 적용하기
            m_FinalValue = ApplyModifiers(m_FinalValue);

            // currentValue 갱신
            //m_CurrentValue = m_FinalValue;
        }

        /// <summary>
        /// Attribute, Derived 의 베이스 값 계산
        /// Derived의 경우 다른 스텟에 영향을 받아서 baseValue가 결정됨
        /// </summary>
        /// <returns></returns>
        protected abstract int CalculateBase();

        // 버프 디버프 적용 ?
        protected virtual int ApplyModifiers(int value)
        {
            int flat = 0;
            float percent = 0f;             // 합연산
            float finalMultiplier = 1f;     // 곱연산
            foreach (var m in m_Modifiers)
            {
                switch (m.type)
                {
                    case ModifierOperationType.Flat:
                        flat += m.magnitude;
                        break;
                    case ModifierOperationType.Percent:
                        percent += m.magnitude * 0.01f;
                        break;
                    case ModifierOperationType.finalMultiplier:
                        finalMultiplier *= 1f + m.magnitude * 0.01f;
                        break;
                }
            }

            // 플렛 적용
            int result = value + flat;

            // 퍼센트 적용
            result = Mathf.FloorToInt(result * (1f + percent));

            // 최종 배율 적용
            result = Mathf.FloorToInt(result * finalMultiplier);

            return result;
        }

        public virtual void AddBaseValueAmount(int amount)
        {
            definition.AddBaseValueAmount(amount);
            MarkDirty();
            onValueChanged?.Invoke();
        }

        public void AddModifier(StatModifier modifier)
        {
            m_Modifiers.Add(modifier);
            MarkDirty();
            onValueChanged?.Invoke();
        }
        public void RemoveModifierFromSource(Object source)
        {
            if (source == null) return;

            int prevCount = m_Modifiers.Count;

            m_Modifiers.RemoveAll(m => m.source == source);
            
            if (m_Modifiers.Count != prevCount)
            {
                MarkDirty();
                onValueChanged?.Invoke();
            }
        }
        public void RemoveAllModifiers()
        {
            if (m_Modifiers.Count > 0)
            {
                m_Modifiers.Clear();
                MarkDirty();
                onValueChanged?.Invoke();
            }
        }

        public void MarkDirty() => m_IsDirty = true;

        protected StatDefinition.Source GetSource(int idx) => definition.sources[idx];
    }
}