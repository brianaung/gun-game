

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
  - third. Not decide yet. wait to see which is better.
- Ian: player input
  - character movement 
  - jumping/double jump, dashing (mid-air/ground), strafing
- Ian: UI elements + game controller
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
_Exciting title_ is a first-person shooter (FPS) set in...

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
