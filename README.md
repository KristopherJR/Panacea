# Panacea
Panacea is a 2D, Top down game produced for COMP2451 Games Design &amp; Engineering. It incorporates the previously developed Pong Game Engine from the 3rd Milestone, and uses the game concept developed in the 1st milestone as it's basis for development. This is the 4th and Final, Post-Production Milestone for 2nd Year Computing in this module. It makes use of Dijkstra's algorithm to give the NPCs automated Pathfinding, in which it will calculate the shortest path between a random Tile and follow it.

![image](https://user-images.githubusercontent.com/47984645/115913767-efb36e80-a468-11eb-9d06-a04d36bec90e.png)

<h2>Synopsis</h2>
In the cold, autumn months of 1894 – Sam, a trainee doctor joins the ranks of the Royal Worcester Infirmary. Starting as a mere appealing work placement, Sam will soon realise there is much more to this institution than meets the eye, and that not everyone’s intentions may be as they seem. On arrival you’re briefed, introduced to the team and shown the wards which are soon to become your second home. As you progress, you hone your skills and become a true expert in your field, presenting unrivalled skills that even the most senior members of the team cannot match, but is it enough? Unbeknownst to you, matters are about to get much more complicated and demanding as people are mysteriously falling ill, and inpatients numbers drastically rising each day. Distracted by the unprecedented workload that falls upon the workforce in this pre-war, Victorian society – you fail to notice that those that proclaim to be your ally, are far from it. Who is really your friend, and who can you really trust? You are the intern after all, surely no one is out to get you… It’s your job to sift the fact from fiction, defend your name and prove your worth as a member of the team but most importantly, keep everyone alive in the process!

<h3>Version 0.7</h3>
Version 0.7 sees the final version that will be used for COMP2451 Final Milestone submission. The game will still be worked on past this point, but this is what was submitted. The game in it's current state features the following mechanics/features:

<br>1 -	Walking with W, A, S, D
<br>2	- Animated Sprites
<br>3	- Spawning a world with a TileMap, with separate collidable and non-collidable layers
<br>4 -	Player Token colliding with Walls and other Entities
<br>5 -	A Camera that can track the player, with zoom functionality. (Mouse input)
<br>6 -	Sprint Mechanic (With a changing animation)
<br>7 -	Roaming NPCs using a Pathfinding (Dijkstra's) algorithm

<br>The software features 5 A.I Managers in the Engine Code that the game runs off. A SceneManager, EntityManager, CollisionManager, InputManager and NavigationManager.
<br>
<br>For more details about each aspect of the programs development and mechanics, review the design documentation included in the source code.
