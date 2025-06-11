# Sparta ATM Practice

Unity 기반 ATM 시뮬레이션 프로젝트입니다. 사용자 로그인, 잔액 조회, 송금 기능 등을 포함하여 실습 중심으로 구현하였습니다.

## 📁 프로젝트 구조

Sparta_ATM_Pratic-main/

├── Assets/

│ ├── Font/

│ ├── Scenes/

│ ├── Scripts/

│ │ ├── GameManager.cs

│ │ ├── LoginManager.cs

│ │ ├── PopupBank.cs

│ │ └── PopupRemit.cs

│ └── TextMesh Pro/

├── Packages/

├── ProjectSettings/

├── .gitignore

├── .gitattributes


## 🛠 주요 기능

- **사용자 로그인**: ID/비밀번호 기반 로그인 시스템
- **잔액 조회**: 현재 사용자의 잔액 확인
- **송금 기능**: 다른 사용자에게 금액 송금
- **UI 팝업 관리**: 다양한 팝업 UI 지원
- **데이터 저장**: JSON 기반 유저 데이터 저장/불러오기

## 🚀 시작 방법

1. Unity Hub에서 프로젝트 폴더 열기
2. `MainScene`을 실행
3. 사용자 ID와 PW를 입력하여 로그인 → 송금 등 기능 사용 가능

## 💾 빌드 파일 다운로드

> 💻 [Windows용 실행 파일 다운로드]
> https://drive.google.com/file/d/1469Vo46vSVphqO3PU1mulFPGUft7WU7b/view?usp=drive_link

## 🧱 개발 환경

- Unity 2021 이상
- TextMesh Pro

## 📌 주의 사항

- 사용자 정보는 로컬 JSON으로 저장되므로 보안에 유의
- 예: `/userdata_<이름>.json`

## 🙋‍♀️ 만든 사람

- 김기찬
