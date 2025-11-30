using System;
using System.Collections.Generic;
using UnityEngine;

namespace StatSystem
{
    public class StatController : MonoBehaviour
    {
        [SerializeField] StatDatabase statDatabase;
        protected Dictionary<string, Stat> stats = new(StringComparer.OrdinalIgnoreCase);
        public Dictionary<string, Stat> Stats => stats;

        bool isInitialized;
        public bool IsInitialized => isInitialized;
        public event Action Initialized;
        public event Action WillUninitialize;

        protected void Awake()
        {
            if (!IsInitialized)
            {
                Initialize();
                isInitialized = true;
                Initialized?.Invoke();
            }
        }

        void OnDestroy()
        {
            WillUninitialize?.Invoke();
        }

        void Initialize()
        {
            foreach (var definition in statDatabase.stats)
            {
                Stats.Add(definition.name, new Stat(definition));
            }
            foreach (var definition in statDatabase.attributes)
            {
                Stats.Add(definition.name, new Attribute(definition));
            }
            foreach (var definition in statDatabase.primaryStats)
            {
                Stats.Add(definition.name, new PrimaryStat(definition));
            }
        }
    }
}