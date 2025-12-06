using System;
using System.Collections.Generic;
using UnityEngine;

namespace StatSystem
{
    public class StatHandler : MonoBehaviour
    {
        [SerializeField] StatDatabase m_StatDatabase;
        [SerializeField] StatEffect m_StatEffects;
        public StatEffect statEffects => m_StatEffects;
        protected Dictionary<string, BaseStat> m_Stats = new(StringComparer.OrdinalIgnoreCase);
        public Dictionary<string, BaseStat> stats => m_Stats;

        bool m_IsInitialized = false;
        public bool isInitialized => m_IsInitialized;
        public event Action Initialized;
        public event Action WillUninitialize;

        protected virtual void Awake()
        {
            if (!m_IsInitialized)
            {
                Initialize();
            }
        }

        void OnDestroy()
        {
            WillUninitialize?.Invoke();
        }

        protected void Initialize()
        {
            foreach (StatDefinition definition in m_StatDatabase.attributes)
            {
                m_Stats.Add(definition.name, new Attribute(definition));
            }
            foreach (StatDefinition definition in m_StatDatabase.secondaryStats)
            {
                m_Stats.Add(definition.name, new SecondaryStat(definition));
            }
            foreach (StatDefinition definition in m_StatDatabase.resourceStats)
            {
                m_Stats.Add(definition.name, new ResourceStat(definition));
            }

            foreach (var baseStat in m_Stats.Values)
            {
                if (baseStat is SecondaryStat derived)
                {
                    var targetDefinition = derived.definition;

                    List<BaseStat> sources = new();
                    foreach (var sourceDefinition in targetDefinition.sources)
                    {
                        if (m_Stats.TryGetValue(sourceDefinition.stat.name, out var srcInstance))
                        {
                            sources.Add(srcInstance);
                        }
                    }
                    derived.SetupSorces(sources);
                }
            }


            foreach (var stat in m_Stats.Values)
            {
                stat.Initialize();
            }
            
            m_IsInitialized = true;
            Initialized?.Invoke();
        }

    }
}