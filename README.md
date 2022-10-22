

**The University of Melbourne**
# COMP30019 – Graphics and Interaction

## Teamwork plan/summary

<!-- [[StartTeamworkPlan]] PLEASE LEAVE THIS LINE UNTOUCHED -->

<!-- Fill this section by Milestone 1 (see specification for details) -->

Here you should be discussing how you will be delegating tasks among team members, as well
as protocols and processes you will use to keep the project organised. Keep it updated 
throughout the duration of the project. By the final submission it should contain 
a summary of who completed what. (You should of course remove and replace this paragraph!)

- Ian: camera view
  - third person, camera movement, camera collision
- Ian: player input
  - character movement 
  - jumping/double jump, dashing (mid-air/ground), strafing
- Ian: UI elements (health and ammo) + game controller ()
- Ian: jump particle effect
- Ian: Audio for guns and menu
- Ian: Ui elements/buttons for menu 
- Harry: weapon system: AK47/Firethrower
  - right click to shoot, press "R" to reload, press "1" or "2" to swich weapon, move mouse to trun character around
  - fire gun: it is implement with fire particle system. 
  - rifle
  - Decision making:
    - weapn only move in horizontal line
  - External resource:
    - https://www.youtube.com/watch?v=Cwe-GV1OM3k
    - https://www.youtube.com/watch?v=om-SS-CBZ8g&t=3s
  - weapon damage
- Harry: particle system
  - flame comming through fire gun, blood animation
- Jun: enemy system
  - hide in the corner of maze and pop up suddenly
  - projectile collision
  - enemy movement
  - enemy spawn rate
- Brian: environment
  - cel shading to have an cartoonish effect
  - Rooms, procedurally generated (room size, enemies spawn rate, objects within the room)

<!-- [[EndTeamworkPlan]] PLEASE LEAVE THIS LINE UNTOUCHED -->

## Final report

Read the specification for details on what needs to be covered in this report... 

Remember that _"this document"_ should be `well written` and formatted **appropriately**. 
Below are examples of markdown features available on GitHub that might be useful in your report. 
For more details you can find a guide [here](https://docs.github.com/en/github/writing-on-github).

### Table of contents
* [Game Summary](#game-summary)
* [Technologies](#technologies)
* [Using Images](#using-images)
* [Code Snipets](#code-snippets)

### Game Summary
Gun Game is a game where you play as a capsule man going through a dungeon and killing other capsule men and then eventually ending up fighting a giant amogus after 20 kills.

### How to play the game
The game uses fairly stand mouse and keyboard inputs, WASD to move the player around, left mouse button to shoot, moving the mouse moves the camera around the player space to jump, r to reload and the numbers 1 and 2 allow you to switch to the corresponding two weapons. When the game first loads you are taken to a menu screen where you can press start to play or press controls to look over the controls or the credits to look at the credits.

### Level design
The biggest design decision we made is how the level is going to be like. We knew we wanted a shooter game in the third person and so an arena first came to mind. However, an area would be a bit too basic and we wanted to include some procedural generation for the level, and so we thought of doing a maze of some kind. Whilst this would work, mazes tend to be quite small as it's just a bunch of corridors. We eventually came up with the idea of procedurally generating rooms and connecting them with corridors. This gives us much more space to decorate the rooms with objects and larger rooms give the player more freedom of movement.

### Player and Camera controls
The next gameplay decision was how the player is going to be controlled. We opted for the standard mouse and keyboard controls, WASD, space, left mouse button, etc., as that is what most shooter games use. We could have added a controller option however we thought mouse and keyboard controls were best due to the accuracy of a mouse would fit the game much better than controller thumbsticks.

Another thing we had to consider is making the game with a first person perspective or third person perspective. Whilst the game can work with a first person perspective and easier as dealing with camera collision makes the camera movement a bit more complicated, making the game third person, gives the player a larger field of view allowing the player to see enemies on the sides more easily, making for a better gameplay experience.

Another design related decision that needs to be made in regards to the camera is how it is going to collide with the objects around it. An early implementation of camera collision was to check slightly behind the camera for objects, however, when using the position of the camera, as a start point of a linecast. Something like this,
``` C#
if(Physics.Linecast(camTransform.position + offset, camTransform.position - offset, out var hit))
{
  transform.position = hit.point;
}
 
```
will make the camera stick to the wall. This can be patched with checking if the player's distance from the camera gets to larger than the camera will move to its normal position, however, that is only a janky patch and not actually a fix as the gameplay experience isn't that great as the camera will suddenly move forwards.

Eventually we ended up going for a more simple route, which is just checking for objects between the player and camera. However, since the player is also an object we must include a slight offset so the linecast avoids the player. Something like this
``` C#
  if(Physics.Linecast(playerTransform.position - offset, camTransform.position, out var hit))
  {
      camtransform.position = hit.point;
  }
```
Whilst this isn't a perfect solution, enemies or objects when close enough can mess with the camera position, it is a solution that works well enough and was easily implemented.

### Boss level
A gameplay decision we had to make was how do we end the game? The game wasn't endless and so we decided to add in a boss enemy at the end. There are many ways of adding a boss enemy to a game, however, with how we did the level design, much more thought was needed to determine the best way to add the boss. For example, what room do we add the boss? At what point in the game do we add the boss? These were the big questions we had to think about. We first thought about adding the boss in the biggest room that was generated. However, due to the nature of how the level was generated we couldn't guarantee a large room since we wanted to make the boss large in comparison to the player. We could've figured out a way to generate a predetermined size room specifically for the boss but adding that into the dungeon generator made it too complicated. And so, rather than making the boss a part of the dungeon we decided to make the boss its own scene. With the boss having its own scene, we could design the level specifically for the boss and not have to worry about the limitations of the dungeon generator as we can design the level to the needs of the boss. 

The next decision we had to make was how to transition from the dungeon level to the boss level. There was a couple of ways to do this, the first is a time limit, which can be implemented quite easily, however, we wanted the player to be able to explore the dungeon at their own leisure, and with different dungeon sizes, the time limit needs to be altered which adds to unnecessary coupling. We ended up deciding on using a kill counter to determine when the player will transition to the boss level. We also thought about using the number of rooms cleared but that would still be the equivalent to a kill counter, and if we decided to make the game more maze like the player wouldn't need to clearevery room completely to transition to the boss level.

### Procedural Generation Technique 
# TODO: Add more details
We wanted to use procedural generation is regards to the making the dungeon and the simplest way of doing that was generating rooms and then connecting them with corridors. For the room generation, we used an algorithm called Binary Space Partitioning which subdivides a space into smaller spaces. And so, repeatedly doing that will split a large space into smaller spaces which will act as rooms. Adding straight line paths from each room to another room and walls around the paths and rooms will create a complete dungeon. 

### Shader pipeline

## Cel Shader

## Pixelated Shader


### Particle System
## TODO: Which one do we want marked?
Enemy explosion/Flamethrower in Assets/Level/Prefabs/Effects

### Evaluation
## TODO: Evaluation stuff

### Technologies
Project is created with:
* Unity 2022.1.9f1 
* Ipsum version: 2.33
* Ament library version: 999

### Using Images

You can include images/gifs by adding them to a folder in your repo, currently `Gifs/*`:

<p align="center">
  <img src="Gifs/sample.gif" width="300">
</p>

To create a gif from a video you can follow this [link](https://ezgif.com/video-to-gif/ezgif-6-55f4b3b086d4.mov).

### Code Snippets 

You may wish to include code snippets, but be sure to explain them properly, and don't go overboard copying
every line of code in your project!

```c#
public class CameraController : MonoBehaviour
{
    void Start ()
    {
        // Do something...
    }
}
```

### External Resources
https://www.youtube.com/watch?v=zVX9-c_aZVg
https://www.youtube.com/watch?v=CSuvGGiC2wI
https://docs.unity3d.com/ScriptReference/CharacterController.Move.html
