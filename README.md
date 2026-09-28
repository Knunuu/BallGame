Game for GDW class.

Keanu Wong-Nguyen 100977720

Fuck It We Ball

This game is a sports game where each team's goal is to kick or carry the ball to the opposing goal. Each game is consisted of 5 arenas in a line that transition to each side upon scoring a goal. If a team reaches to the final end of the opposing side, they win the game. The players can kick, grab, and throw the ball and other items, along with unique skills per character to specialize their play style to win the game.

For GDW Tutorial Lab Assignment 1, I created a factory for creating items. There are only a few items currently in the game: The main ball (managed by the Game Manager), an extra ball, and a bomb. They all inherit from the Item class, and the factory manages these items and adds additional affects to them, both for managing and gameplay purposes. The item factory can receive the desired item location, spawn delay, and transform parent, but the main thing is that i can also change the gravity setting of the object. By default, the items have no gravity, but can be altered in the factory, so for example I can create a bomb or ball with or without gravity.

I can iterate more on this factory to also change things such as the scale or bounciness of each item, if I want to create more random items . This allows things such as different game modes where all gravity is enabled or size is randomized, without having to create new prefabs.

The game manager uses this factory to create the starting ball that it tracks throughout the game.

GameManager --> Ask Factory for ball item --> returns ball with no parent (to avoid do not destroy issues) --> GameManager saves ball and moves it when transitioning between arenas.

Each arena has its own item spawner that makes items randomly when it is the active stage.

Arena isActive? --> Yes, start asking factory for items. Sends location and ask for specifically random item. --> Item factory generates random stats and spawns item.
