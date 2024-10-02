# Cheese-hunt
A Unity project of an Android mobile game for a programming exam at HEAJ in 2023-2024. This is a game where you control a mouse, you have to collect points by taking food on the map while avoiding cats and other traps, remembering to go back and hide in the hole before the end of timer.

# Luca Rougefort / GeekLuc
## Etudiant B3JV programmation de jeux vidéo HEAJ

https://geekluc.itch.io/cheese-hunt

I use a statemachine for the different states of my game
In this project, my enemies, whether it is the cat or the 2 traps, are behaviorTrees

Here is an explanation of some interesting script

# CatBT.cs
The CatBT.cs script implements a behavior tree for a cat enemy in Unity, using a NavMeshAgent for navigation. It defines hunting and attacking behaviors based on distance from the player, using sequences and selectors to organize actions. The cat patrols, pursues the player if he is in his field of vision (fovRange), and attacks if he is within range (attackRadius). Gizmos are drawn to visualize these staves in the editor. The script also adjusts the speed of the agent with each update.

# TrapSpeed.cs
The TrapSpeed.cs script implements a behavior tree for a speed trap in Unity, which interacts with the player. It uses sequences and selectors to define the trap's actions: activate the trap if the player is in range (checkRange) and deactivate the trap otherwise. The trap can be activated or deactivated via the ActivateTrap and DeactivateTrap methods, and a gizmo is drawn to visualize the detection range in the editor. Behavior is managed by nodes in the behavior tree, such as CheckRange, TrapActived, IsTrapActived, and TrapDisabled.

# UpgradeSystem.cs
The UpgradeSystem.cs script manages an upgrade system for a Unity game, allowing players to purchase and apply upgrades using in-game currency. Each upgrade has several specific levels, costs, and effects, such as increased speed, pickup points, pepper duration, and playing time. The BuyUpgrade, SetSpeed, SetPickupScore, SetPepperDuration, and SetGameTime methods apply the improvements and update the player's preferences. The script also updates UI texts to reflect costs and descriptions of enhancements, and draws gizmos to visualize scopes in the editor.

# Timer.cs
The Timer.cs script manages a countdown timer in a Unity game, displaying the remaining time via a user interface and triggering events based on the elapsed time. It sets the maximum time from the player's preferences, updates the timer text and image, and changes the color of the image based on the remaining time. Visual alerts and vibrations are activated when the weather is critical. The script also adjusts the behavior of the pickup spawner (pickUpSpawner) based on the time remaining and triggers a camera shake when the time is very low. When the time is up, it changes the game state to defeat state.

# Spawner.cs
The Spawner.cs script handles the generation of pickups and obstacles in a Unity game. It initializes and maintains instance counters for each pickup type and obstacle, and generates them randomly in defined areas while respecting minimum distances between them. The SpawnPickUps and SpawnObstacles methods ensure that objects do not overlap and are not too close to players or enemies. The script dynamically adjusts the maximum instance quantities and destroys excesses if necessary. It also allows you to retrieve instance scores and counters, and change the maximum instance limits for pickups.


