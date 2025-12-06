using System;
using UnityEngine;

namespace StatSystem
{
    [Serializable]
    public enum EffectOperationType
    {
        Addition,
        Subtraction,
        PercentCurrentIncrease,
        PercentCurrentDecrease,
        PercentMaxIncrease,
        PercentMaxDecrease,
    }

    [CreateAssetMenu(fileName = "StatDefinition", menuName = "DaveAssets/StatSystem/Effect")]
    public class StatEffect : ScriptableObject
    {
        [SerializeField] EffectOperationType m_OperationType;
        [SerializeField] float m_Value;

        public EffectOperationType operationType => m_OperationType;
        public float value => m_Value;
    }
}