using System;
using System.Collections.Generic;
using UnityEngine;


namespace StatSystem
{
    public class SecondaryStat : BaseStat, IDerivedStat
    {
        List<BaseStat> m_SourceStats = new();
        public List<BaseStat> sourceStats
        {
            get => m_SourceStats;
            set => m_SourceStats = value;
        }
        public SecondaryStat(StatDefinition definition) : base(definition) { }


        public override void Initialize()
        {
            base.Initialize();
        }

        protected override int CalculateBase()
        {
            int value = 0;
            // formula에 영향을 주는 외부스텟 가져오기
            for (int i = 0; i < sourceStats.Count; i++)
            {
                var finalValue = sourceStats[i].finalValue;
                value += Mathf.RoundToInt(finalValue * GetSource(i).weight);
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
    }
}