# [Memory Game](http://inventwithpython.com/images/simulate_screenshot.png)

##### Description

> Four coloured buttons light up in a specific pattern. After displaying the pattern, the player must repeat the pattern by clicking the buttons in proper order. The pattern gets longer each time the player completes the pattern. If the player presses a wrong button, the game ends.
The game will also present the score to the user, as well as save it to an SQL DB so that it remembers it the next time the game is started

- OOP
- Will store patterns in an array
- Will compare Arrays against each other
- Will update the screen with new score
- Will show highest score
- Will store data on a DB so the score are persistent

`Classes`
`Arrays`
`Methods`
`SQL`

### Updates

- Created All Windows Forms
- Created All essential Classes
- Colour Boxes get dynamically created
- Colour Boxes will trigger a universal event which will eventaully act as the Brain of the game

### What Needs To Be Implemented

- DB Syncing
- Fix Position of colour boxes
- Add a score and wave hud for the user to see
- Implement settings page

#### Summary
Memory Magic is a simple in that when you start the game, a random pattern of colours are generated. This is then used to put the user to the test on how many of those colours, in a order he / she can remember. 

The game has a variety of settings that can be changed before the start of the game. These include: 

Name: This is a text box where the user can type there name, or an alias. This is used for identifying a user within the database. 

Difficulty: within this setting, there are four different options to choose from "Easy", "Medium", "Hard", "Impossible ". Each of these will determine the amount of time the user has to remember the colours, as well as the score calculations. The more difficult, the higher chance you will have to get a higher score. 

GridSize: since the colour grid gets dynamically created when the game starts the user has the ability to change this setting which will enable the user to change how big the grid will be. There are four different settings, they go as follows: 4*4, 8*8, 12*12, 16*16. Obviously, as the grid gets bigger, the hard it will become, however saying that, when the grid is bigger, the game will reward the user will more points. 

Multi-Colour: this is a settings which can be enabled or disabled. When enabled, the board will be generated with a variety of colours this making it a lit harder for the user. Having this option enabled will also allow the user to gain more points. 

Leaderboard: This setting doesnt affect how the gameplay works. It just determines whether or not the user saves his or hers score to the leaderboard for everyone to see. 


What Did I Use And Why

When I first started designing my game, I had absolutely no idea how to implement a grid into the window. I began to add them manually. I soon realised this was a bad idea because I wanted the size of the grid to change depending on user input. This is where I started to programatically and dynamically create the grids. This allowed for far better control which is exactly what I wanted. 

Since I wanted the user to click each part of 
