# Forest of Patience - 인내의 숲
**2024 게임공학 캡스톤디자인** 에서 진행한 1인 게임 개발 프로젝트입니다.  

## 📝 프로젝트 소개
* '메이플스토리' 게임 내에 존재하는 '인내의 숲' 게임과 비슷한 2D 점프 게임입니다.
* 개인 기록을 측정하는 '싱글 플레이' 모드와 여러 사용자와 경쟁할 수 있는 '멀티 플레이' 모드가 있습니다.

## ⏳ 프로젝트 기간
2024.05.15 ~ 2024.06.18  

## 📍 주요 기능
**초기 접속 화면**  
![인내의 숲](https://github.com/user-attachments/assets/b5892071-ac1b-4a4a-8838-9a78ee1a384c)
* 게임을 실행하면 보이는 초기 화면입니다.
* 싱글 플레이, 멀티 플레이를 골라 게임을 플레이할 수 있습니다.
* 설정에서 싱글 플레이의 플레이 기록을 삭제할 수 있습니다.

<br>

**싱글 플레이**  
![맵 선택](https://github.com/user-attachments/assets/75b57b3f-4b7d-4441-941d-07f8919195ff)
* '기본맵', '장애물맵', '발판 지옥맵' 중 한 가지를 선택하여 게임을 할 수 있습니다.

<br>

**맵 종류**
|기본맵|장애물맵|발판 지옥맵|
|----|------|--------|
|![map1](https://github.com/user-attachments/assets/78192991-54b5-4a8a-8f1b-9ab3d165cd5b)|![map2](https://github.com/user-attachments/assets/7d407840-5eda-46bc-ba53-2effaf9fd62c)|![map3](https://github.com/user-attachments/assets/cb9eb45e-caec-4945-9c33-aef87f237214)
|배치된 발판을 밟고 골인 지점까지 점프하면 되는 기본맵입니다.|일정 주기로 반복되는 장애물을 피해서 골인 지점으로 올라가야 하는 맵입니다.|일정 주기로 발판이 나타났다 사라지는 것이 반복되는 맵입니다.|

<br>

**장애물 종류**
|스윙 장애물|톱니 장애물|
|--------|--------|
|![swing](https://github.com/user-attachments/assets/31d04cf2-3f79-429f-bae0-308df1206ba3)|![saw](https://github.com/user-attachments/assets/a7ac09a8-b0be-416f-acb2-cebe0889ed65)
|좌우로 흔들리는 장애물로 충돌하게 되면 힘, 방향을 고려하여 캐릭터가 밀려납니다.|상하로 움직이는 장애물로 위로 올라올 때만 충돌이 적용됩니다. 마찬가지로 충돌하면 캐릭터가 밀려납니다.|

<br>

**멀티 플레이**
|닉네임 입력|방 번호 입력|
|--------|---------|
|<img width="1384" alt="nickname" src="https://github.com/user-attachments/assets/03590834-b5ca-4533-98ba-c3d28249d834">|<img width="1384" alt="room_number" src="https://github.com/user-attachments/assets/39b7358f-4e1d-45cb-8fb0-d76d3fb1e897">|
|최대 8자의 닉네임을 입력할 수 있고 한글, 영문, 숫자, 특수기호를 사용할 수 있습니다.|0에서 999사이의 방 번호를 입력해야 입장 버튼이 활성화 되고, 같은 방에 입장한 유저끼리 게임을 할 수 있습니다.|

<br>

**로비**
<img width="1536" alt="lobby" src="https://github.com/user-attachments/assets/201ee814-c741-41b6-8c52-bd0085a212f8">
* 제일 먼저 방에 들어온 유저가 호스트(Host)가 됩니다.
* 호스트의 화면에만 **Menu** UI가 보이고, 호스트만이 맵을 선택하여 게임을 시작할 수 있습니다.
* 맵을 고르지 않은 상태에서는 게임을 시작할 수 업습니다.

<br>

**멀티 플레이(인게임)**  
![inGame_Multi](https://github.com/user-attachments/assets/e4d7fa90-7152-4150-a891-f7b98c03152a)
* 모든 유저가 동일하게 게임을 시작하기 위해 3초 뒤 게임이 시작됩니다.
* 특정 유저가 골인 지점에 도착하면 게임은 종료되고 결과 화면이 표시됩니다.
* 결과 화면에는 1등 유저의 닉네임이 보이고 메인 화면으로 이동할 수 있는 버튼이 있습니다.
