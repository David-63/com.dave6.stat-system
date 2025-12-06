## [0.0.1] - 2025.11.30

### Remove
- Editor, Level, Save 기능 제거


### Change
- StatSystem 구조 변경
  - Modifier 기능을 Add, Multiplicative, Override 구조에서
  Flat, Percent, finalMultiplier로 변경. 계산식은 (Base + Flat) * (+ Percent) * (x FinalMultiplier)
  - Stat 구조를 abtract BaseStat을 상속받도록 변경
  IDerivedStat, IEffectApplicable 인터페이스 추가
  IDerivedStat는 formula 계산을 노드그래프 대신에 다른 statDefinition을 참조하는 구조
  IEffectApplicable는 currentValue와 그것에 영향을 주는 ApplyEffect함수를 제공
