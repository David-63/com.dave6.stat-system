using UnityEngine;

namespace StatSystem
{
    [CreateAssetMenu(fileName = "StatDefinition", menuName = "DaveAssets/StatSystem/StatDefinition")]
    public class StatDefinition : ScriptableObject
    {
        [SerializeField] int baseValue;
        [SerializeField] int cap = -1;
        //[SerializeField] NodeGraph formula;
        public int BaseValue => baseValue;
        public int Cap => cap;
    }
}