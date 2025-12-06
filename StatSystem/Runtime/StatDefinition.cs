using System;
using System.Collections.Generic;
using UnityEngine;

namespace StatSystem
{
    [CreateAssetMenu(fileName = "StatDefinition", menuName = "DaveAssets/StatSystem/Stat/Definition")]
    public class StatDefinition : ScriptableObject
    {
        [SerializeField] int m_BaseValue;
        [SerializeField] int m_Cap = -1;
        [Serializable]
        public class Source
        {
            public StatDefinition stat;
            public float weight = 1f;
        }
        [SerializeField] List<Source> m_Sources;
        public List<Source> sources => m_Sources;
        public int baseValue => m_BaseValue;
        public int cap => m_Cap;


        public void AddBaseValueAmount(int amount)
        {
            int temp = m_BaseValue;

            // -1은 제한이 없는 상태, 그냥 다 더함
            if (m_Cap == -1)
            {
                temp += amount;
            }
            else
            {
                // 제한수치까지 그냥 더함
                temp = Mathf.Min(temp + amount, m_Cap);
            }

            m_BaseValue = temp;
        }


        //[SerializeField] NodeGraph m_Fomula;
        //public NodeGraph formula => m_Fomula;
    }
}