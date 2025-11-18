# My House Is Built For You

**My House Is Built For You** is a game created for the [Gamerella game jam](https://itch.io/jam/gamerella2025). This is a work-in-progress project developed by Yulia, aoof, Luna, and Adrien.

You can find the game on itch.io: [My House Is Built For You](https://frigidbody.itch.io/my-house-is-built-for-you)

## UI Components Implementation

### UIManager
The `UIManager` is a singleton that manages the display and hiding of all UI panels in the game. It handles the day sequence at the start of each day, showing the day number with a fade-in/fade-out effect, followed by the task menu. It controls mouse locking and input actions: when in UI, the mouse is unlocked and visible, player movement inputs are disabled, and the escape key is enabled to skip or exit UI. The `isInUI` flag indicates whether the player is currently interacting with UI elements.

Key methods:
- `ShowTaskMenu()`: Displays the task selection menu.
- `ShowDayText(int dayNumber)`: Shows the day number text with fade effects.
- `ShowJudgement()`: Displays the judgement screen.
- `ShowDialogue()`: Shows the interaction dialogue.
- `HideAll()`: Hides all UI panels and locks the mouse back for gameplay.

### TaskMenu
The `TaskMenu` displays the list of available tasks for the current day. It uses buttons to represent each task, allowing the player to select one. The menu can be in read-only mode (showing tasks without selection) or interactive mode. Tasks that have already been performed are disabled. Pressing escape toggles the task menu visibility.

Key features:
- Updates buttons based on `PointsSystem.availableTasks`.
- Highlights the selected task in green.
- Calls `onTaskSelected` when a task is confirmed.

### MainMenu
A simple script attached to the main menu scene. It provides buttons to start a new game (loads scene 1) or exit the application.

### DialogueManager
Handles the interaction dialogue when the player approaches an interactable object. It displays three options: Change (alters the object's variant), Destroy (removes the object), and Nothing (cancels interaction). The dialogue uses `DialogueOptions` from the interactable object to customize the button texts. After selection, it updates the object's state in the `PointsSystem` and disables further interaction with that object.

### JudgementManager
Manages the end-of-day judgement sequence. It displays the partner's feedback based on the number of correct actions performed, calculated by the `PointsSystem`. The judgement screen shows for a set duration (5 seconds) or can be skipped with double escape press. After judgement, it starts the next day.

### PlayerUI
A simple component that toggles the visibility of an interact indicator (e.g., a prompt to press E) based on the `showInteract` boolean. This is controlled by other scripts when the player is near interactable objects.

## PointsSystem
The `PointsSystem` is the core scoring and game state management system. It tracks player actions across days and evaluates performance against predefined correct actions.

### Key Structures
- `ObjectState`: Enum with values `Unchanged`, `Destroyed`, `Changed` – represents the state of an interactable object.
- `ActionStruct`: Records an action with `ObjectName`, `TaskName`, and `ObjectState`.
- `CorrectAction`: Defines the expected action for a task, including `taskName`, `objectName`, and `objectState`.
- `DayHistory`: A list of `ActionStruct` for each day.
- `DayTasks`: A ScriptableObject containing a list of task names and correct actions for a day.

### Game Flow
1. **Initialization**: On start, adds the first day to `stateHistory`, resets interactable objects, and shows the day sequence.
2. **Action Tracking**: Each interaction with an object (via `DialogueManager`) records an `ActionStruct` in the current day's history.
3. **Day Progression**: After performing all tasks (`currentActions >= dayTasks[currentDay].tasks.Count`), triggers judgement.
4. **Judgement Calculation**: Compares performed actions to `correctActions` for the day. Counts matches and generates feedback text based on score:
   - 0-1 correct: "You disappoint me, darling."
   - 2 correct: "Well… You can do better, right?"
   - 3 correct: "Thank you. I love you."
5. **Next Day**: Resets state, increments `currentDay`, and repeats.

The system ensures that each object can only be interacted with once per day, and tasks are evaluated based on exact matches of object, task, and state.