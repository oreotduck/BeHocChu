# BeHocChu – Vietnamese Alphabet Learning Game

## Overview

**BeHocChu** is an educational game designed to help children learn the Vietnamese alphabet through simple and interactive gameplay.

The game presents alphabet letters hidden inside eggs. When players click on an egg, it breaks and reveals a letter. The game then shows an illustration related to that letter to help children associate letters with familiar objects.

This project was developed using **Unity** as part of a learning project.

## ✨ Features

- Interactive egg-breaking mechanic
- Vietnamese alphabet learning
- Visual illustration for each letter
- Simple and child-friendly interface
- Educational gameplay for early learners

## 🛠 Technologies Used
![Unity](https://img.shields.io/badge/Unity-100000?style=for-the-badge&logo=unity&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white)
![SQLite](https://img.shields.io/badge/Sqlite-003B57?style=for-the-badge&logo=sqlite&logoColor=white)

### 🎨 Design Tools

![Clip Studio Paint](https://img.shields.io/badge/ClipStudioPaint-%23CFD3D3.svg?style=for-the-badge&logo=ClipStudioPaint&logoColor=white) 
![Aseprite](https://img.shields.io/badge/Aseprite-FFFFFF?style=for-the-badge&logo=Aseprite&logoColor=#7D929E)

## How to Run the Project

1. Clone the repository
`git clone https://github.com/oreotduck/BeHocChu.git`
2. Open the project using Unity Hub
3. Open the main scene and press Play in the Unity Editor.

## 🎮 Gameplay

1. Click on an egg.
2. The egg breaks and reveals a Vietnamese alphabet letter.
3. The game displays an illustration corresponding to the letter.
Example:
- **A → C`á` sấu**
- **B → Con `b`ò**
(Illustrations are used to help children recognize words related to each letter.)

### Screenshots
![Gameplay](https://i.postimg.cc/Jnd3LXMC/Picture1.png)

1. Click at “Bắt Đầu” button, the game will display the first scene where 2 eggs is included.

![Gameplay](https://i.postimg.cc/sfvJXcnG/Picture2.png)

2. Click the left egg for the first time, it will crack a little.

![Gameplay](https://i.postimg.cc/5Nzq5tdT/Picture4.png)

3. Click for the second time, the crack become bigger.

![Gameplay](https://i.postimg.cc/BncTywv2/Picture5.png)

4. Click for the fourth time, the egg will disappear and a capital character will be displayed.

![Gameplay](https://i.postimg.cc/k5JQdF6R/Picture6.png)

5. Click for the fifth time, the capital character will disappear and a normal character will be displayed.

![Gameplay](https://i.postimg.cc/vBjf9JP8/Picture7.png)

6. Click for the sixth time, the word which contains the previous character will be displayed with an image of the word.

![Gameplay](https://i.postimg.cc/NG91Y0MX/Picture8.png)

## 📚 Testing
Basic functional testing was performed to ensure the core gameplay mechanics work as expected.

| Test Case ID | Description | Expected Result |
|--------------|-------------|----------------|
| TC01 | Test menu display | Display main menu |
| TC02 | Test menu click | The game will display the first scene where 2 eggs is included. |
| TC03 | Click on egg | Display word and image of that word |
| TC04 | Progress saved between pages | Show saved progress |
| TC05 | Progress saved when comeback from menu | Show saved progress |

## ⚠ Limitations
- The application does not currently save the revealed letters or images when the player navigates to another page or returns to the main menu.
- Some features are still basic due to the scope of the course project.

## 🚀 Future Improvements
Possible improvements for future versions include:
  - Adding voice pronunciation for letters with support for different Vietnamese regional accents.
  - Adding English alphabet learning mode.
  - Improving UI/UX for children.

Implementing data persistence so the game can remember progress

## Team Members
This project was developed by a team of five members.  
The main contributors to the implementation are:
- Nguyen Vo Ha Giang
- Trinh Thi Trang
- Trieu Gia Khiem

## Course Information

  - Course: Software Project Management.
  - Class: A01E.
  - Lecturer: Lê Viết Linh.
