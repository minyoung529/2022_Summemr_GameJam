(주의사항)
PoolManager를 사용할 경우 반드시 대상 스크립트에 PoolableObject를 상속받아 사용할 것
PoolManager는 MonoBehaviour로 작동됨(Hierachy창 어딘가에는 컴포넌트로 있어야됨)

(참고사항)
Pool이 뭔지는 이해 안 해도 됨(PoolManager가 알아서 해줄거임)

(PoolManger 함수설명)
CreatePool(PoolableObject prefab, int count) : count만큼 prefab을 복제하여 PoolManager가 저장
Pop(PoolableObject prefab) : prefab의 이름을 이용해 복제해둔 오브젝트를 찾아 
							 Active를 true로 변경하고 prefab의 Reset함수 실행 후 리턴
Push(PoolableObject obj) : obj의 Active를 false 변경 후 PoolManager에 다시 저장