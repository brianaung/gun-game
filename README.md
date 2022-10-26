

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

For the level design, we wanted to create a dungeon that will be different each time the user plays the game (something like minecraft where the world will be different each time you start a new game). Therefore we decided to use procedural generation to randomly generate rooms. The main algorithm used is called Binary Space Partitioning.

The high level overview of the algorithm is: 
1. first we define the space where we want to generate the entire dungeon.
2. after that, the parent room is divided into smaller rooms if the is larger than the minimum room size that we defined (we set some offsets so the rooms created are of different sizes)
3. repeat step 2 until all rooms are small enough and can not be divided further.
4. to build the corridors that connect the rooms (and ensure that all rooms can be reached by the player), we connect the parent node with its children nodes in the tree construct when splitting the room.

After applying this procedural generation algorithm, we simply place the prefab assets onto the scene at random positions on the map. Since we are just using premade assets of the natural environment (trees, rocks, etc.), spawing them at random positions make sense.

This algorithm can be used to easily generate dungeons as large as you want and as complicated by adjusting the parameters. However, we decided to generate a smaller and less complicated dungeon consisting of around 5 or 6 rooms since we do not want to make the player spend a long time defeating smaller enemies before reaching the final boss scene.

### Shader pipeline

## Cel Shader

We used cel shading technique to create fun and cartoonish aesthetic to our game.

Some features of our cel shader:
- object glossiness
- rim effect using Fresnel around the objects
- different number of shadow steps and sizes
- customizable outlines (some objects have thicker outline with distinct colors to make it pop more. e.g. Powerups)

Why Cel shading?
- Since our team lack member who have experience with creating our own assets (in blender for example), we decided to mostly download free assets from different sources. Using the cel shading technique helped us make the aesthetic of all the assets more consistent and do not make them look out of place. For the assets, we mostly use low poly assets (objects made with low polygon count with flat surfaces) since we want to make the game look cartoonish and simple. Therefore, the cel shading technique matches more with these low poly assets, making the game looks simple yet looks good.

Path: Assets/Code/Shaders/CelShader.shader

## Pixelated Shader

We used pixeleated shader to provide sqecial visual effect when player is attacked by enemies.

- This shader is assigned to a material called "pixeleted" and controled by a script called pixelationShaderHandeller. It will enable picelated material when collision was detected and disabled after 2 seconds. 

- The aim of this pixelated shader is to blur the whole screen when player was attacked. 

- The team trying to assign this material to all the objects in the scene. However, we find it is very inefficiency and almost impossable to do so. Therefore, pixelated material was assigned to camera, which works as a filter. It turned out very efficient and easy to do so. 

How this be done
- the screen was divided to specific number of pixels with fixed width and height. Each pixcel was assigned to a texture which is extracted from the main texture. 

Path: Assets/Code/Shaders/PixelationShader.shader


### Particle System
Flamethrower effect in Assets/Level/Prefabs/Effects
This flame particle system is made into cartoonish style on purpose in order to match our game aesthetic. 

Attributes were changed based on the apperence of the flame. 

Attributes varied
-  Start lifetime was randomed between 1 and 2. This attribute is worked with start speed to make sure the flame has reasonable length. The randomness of lifetime makes sure the flame particle die out naturally.
-  Max particles was set to 3000 to make flame look good.
-  Shape of particle system is set to cone shape, with radius 0.1 and angle 1. This is can enable all the particles bounds together.
-  Color over lifetime was from yellow to red in order to mimic flame. 
-  Size over lifetime is from the small to larger. 
-  Texture Sheet animation is assgined to a sprite in order to make flame looks more realistic. 

Another particle system "FireEmbers" was add under the hariachy of this flame particle system to make it looks nice. 

### Evaluation


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

#### For Procedural generation
https://www.youtube.com/watch?v=JSRBdUhRBu4
https://www.jamesbaum.co.uk/blether/procedural-generation-with-binary-space-partitions-and-rust/
https://medium.com/@guribemontero/dungeon-generation-using-binary-space-trees-47d4a668e2d0

#### For Cel shading
https://www.youtube.com/watch?v=kV4IG811DUU&t=250s
https://danielilett.com/2019-05-29-tut2-intro/
https://roystan.net/articles/toon-shader/
https://www.ronja-tutorials.com/post/032-improved-toon/#specular-highlights
