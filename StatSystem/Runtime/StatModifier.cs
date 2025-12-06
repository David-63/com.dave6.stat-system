
using UnityEngine;

namespace StatSystem
{
    public enum ModifierOperationType
    {
        Flat,               // Base 에 합하는 고정치 (+15 +25)
        Percent,            // 합연산 요소  (+15% +25%)
        finalMultiplier,    // 곱연산 요소  (x1.15 x1.25)
        //Override,           // 값 덮어쓰기
    }
    public class StatModifier
    {
        public Object source { get; set; }
        public int magnitude { get; set; }
        public ModifierOperationType type { get; set; }

        public override string ToString() => $"{type} {magnitude} frome {source}.";
    }
}