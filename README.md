# MannersMatter 🌟

**MannersMatter** is a 2D educational game designed for children aged 6-12 to teach them the importance of good manners, social etiquette, and kindness. Through interactive storytelling and decision-making, players navigate everyday scenarios at home, school, and the park, learning how their actions affect others.

![MannersMatter Banner](https://via.placeholder.com/800x200?text=MannersMatter+Game)

## 🎮 About the Game

In **MannersMatter**, players take on the role of a young character going through a typical day. From waking up and interacting with parents to attending school and playing in the park, every interaction presents a choice. 

Should you help Mom set the table? How do you greet your teacher? What do you do when a friend is sad?

The game provides immediate feedback through a **Manners Meter** and audio cues, reinforcing positive behavior and explaining why certain choices are better than others.

## ✨ Key Features

-   **Interactive Dialogue System:** Engage in conversations with a variety of characters including Mom, Dad, Teachers, and friends (Alex, Bob, Emma, Liam, Mia).
-   **Decision Making:** Make choices in real-time (e.g., Yes/No responses) that impact the story and your score.
-   **Manners Meter:** A dynamic UI bar that fills up with good deeds and drops with bad manners. Keep it high to win!
-   **Candy Collection:** Earn candies as rewards for exceptional kindness (collect up to 12!).
-   **Audio & Visual Feedback:** Positive sounds and visuals reward good choices, while gentle corrections guide players away from bad ones.
-   **Three Main Environments:**
    -   🏠 **Home:** Learn about chores, respect for parents, and morning routines.
    -   🏫 **School:** Practice classroom etiquette, greeting teachers, and interacting with classmates.
    -   🌳 **Park:** Learn about sharing, empathy, and social play (even with a puppy!).

## 🕹️ How to Play

1.  **Start the Game:** Begin your journey in the "Home" level.
2.  **Interact:** Walk up to characters to start conversations.
3.  **Choose Wisely:** When presented with a choice (like "Help Mom?" or "Ignore her"), click the button that represents the polite or kind action.
4.  **Watch Your Meter:**
    -   ✅ **Good Choice:** Manners Meter goes up, you might get a candy, and you hear a happy sound!
    -   ❌ **Bad Choice:** Manners Meter goes down, and you'll get feedback on why that wasn't the best choice.
5.  **Complete the Day:** Finish all scenarios to see your final score!

## 🛠️ Technical Details

-   **Engine:** Unity 2022.3 (or compatible version)
-   **Language:** C#
-   **Packages:** TextMeshPro, Unity UI
-   **Architecture:**
    -   `PointsManager`: Singleton class managing the global score and candy count.
    -   `MannersMeter`: Handles the UI updates and visual/audio feedback.
    -   `DialogueManager`: Custom scripts for each character (e.g., `DialogueManagerMom`, `DialogueManagerTeach`) handling specific scenario logic.

## 🚀 Installation & Setup

To play or modify the game:

1.  **Clone the Repository:**
    ```bash
    git clone https://github.com/Shubhojit-17/MannersMatter.git
    ```
2.  **Open in Unity:**
    -   Launch Unity Hub.
    -   Click "Add" and select the `MannersMatter` folder.
    -   Open the project (ensure you have a compatible Unity version installed).
3.  **Play:**
    -   Open the `Assets/Scenes/HomePage.unity` scene.
    -   Press the ▶️ **Play** button in the editor.

## 📂 Project Structure

-   `Assets/Scenes`: Contains all game levels (Home, School, Park, GameOver).
-   `Assets/Scripts`: C# scripts for game logic, dialogue, and UI.
-   `Assets/Animation`: Animation controllers for characters.
-   `Assets/Background`: Sprites and background images.
-   `Assets/Songs`: Audio files for background music and sound effects.

## 👨‍💻 Credits

Developed by **[Shubhojit-17](https://github.com/Shubhojit-17)**.

---
*Teaching the world one "Please" and "Thank You" at a time!*
