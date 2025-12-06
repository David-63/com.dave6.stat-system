using System.Collections.Generic;
using UnityEngine;

namespace StatSystem
{
    [CreateAssetMenu(fileName = "StatDataBase", menuName = "DaveAssets/StatSystem/Stat/Database")]
    public class StatDatabase : ScriptableObject
    {
        public List<StatDefinition> attributes = new List<StatDefinition>();
        public List<StatDefinition> secondaryStats = new List<StatDefinition>();
        public List<StatDefinition> resourceStats = new List<StatDefinition>();
    }
}