# StatSystem

캐릭터가 사용할 변수들을 스텟 단위로 묶어서 대신 관리해주는 패키지

## 뭘 할수있지?

- StatDefinition으로 스텟 정의
- StatDatabase에 Attribute, Secondary, Resource로 스텟 등록
- Attribute를 다른 타입의 스텟에 Source로 등록하여 계산식에 추가 가능

## 사용방법

1. 우클릭 → Create → DaveAssets → Stat Database 만들기
2. StatDefinition으로 각 필요한 스텟을 정의하고 DB에 등록
3. 캐릭터에 StatController 컴포넌트 붙이고 → 만든 데이터베이스 연결
