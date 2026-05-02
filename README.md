# RPG Character Manager
## Overview and Reflection

### Ultimate Aspiration

My goal with this project is to create a character sheet manager with features specifically tailored to my best friend's RPG setting and ruleset. A user would be able to create new characters, modify them, and use class features with a focus on ease-of-use, no complex formulas to handle or wondering how a particular stat is calculated. Because the intention is to use a non-standard RPG ruleset, the project uses easily modified and replaced csv files to load abilities, classes, and skills, as well as other features. Changing the ruleset being used would be as simple as using a different set of core files, and modifying them for house rules would be as easy as adding or removing lines. After my presentation, my ultimate aspiration has broadened to include multiple users, instead of being a companion program, this could be a fully featured replacement for services that simulate the "table" experience. Perhaps eventually even providing an asynchronous, forum-style TTRPG experience!

The name used in the files is PolybiusTwo because I couldn't think of anything serious and I really liked that urban legend.

### Why this project?

I chose this project because, as someone who enjoys TTRPGs quite a bit, I'm very familiar with the struggles involved in the hobby. It's very difficult to get a group of people together to play, especially if they live across a country like my group does. Anything that helps make the game go smoother is very appreciated, something that would help a player that doesn't have the player's handbook memorized goes a long way to doing just that. Rolling characters, quickly accessing descriptions for abilities, skills, spells, how proficiency works, even how your own class features work, that's the niche I wanted to tackle with this project. It's something I would use myself, as one of those players that *doesn't* have the player's handbook memorized!

### Current Project Scope

For my class final, I tackled as much as I could given the time I had. I wanted fancier menus and displays but I prioritized function over flair. The project currently has most of the features I wanted from the original idea:
- Character Sheet functionality
	- Creation
		- Name and Description input
		- Class selection with level tracking
		- Ability rolling with a selection of multiple styles (every DM has their favorite!)
	- Saving to and Loading from simple .csv files
	- Displaying and inspecting all saved characters
- Names and Descriptions for Classes and Abilities
- Modularity for what classes and abilities are available
	- Classes and Abilities are loaded from .csv files that can be externally modified to change program behavior

Several features I had hoped to include were an automatic level up handler, actual character sheet editing and deletion, skills, and interactive dice rollers for combat handling and other gameplay. The features that were implemented were intended to use programming skills that I've acquired over the semester. The testing side of the project is quite thin, but the most important functionality that I wanted to make sure of was the dice roller delegates. The tests for those are functional and I believe test them well.

### Lessons Learned

I feel like this project has been great for developing my understanding of certain programming concepts. Delegates stand out as something I didn't quite understand before beginning, and I have to come to really enjoy them. I implemented them in the menu system and the ability rolling system. I've learned a bit more about the challenges of programming from an object-oriented design perspective, several places in the project still need de-duplication and could easily be split up into more granular methods. I also feel like my goal of modularity helped me approach that OOD perspective and, despite the extra work that it ended up creating with loading and parsing the files, I really enjoyed the challenge. The final lesson I learned is that I am really quite bad at writing tests and I am still not entirely sure how to properly test private members.

## Project Design

### Processes Used

The primary design strategy I used was CRC Cards but I did use a simple flowchart for the menu system on my whiteboard. Unfortunately, it has since been erased and it felt disingenuous to recreate it as an example of my design process.

### CRC Cards 
From the creation of these CRC Cards, the roles and implementations evolved quite a bit. The core concepts of these remain in the code, but the Attribute Roller idea ended up simply being a component of the Character Creator class (implemented as CharGen). The classes also ended up being less interconnected than I expected, instead leaning more on the loaded game files and object members.

image placeholder

## In Closing

Thank you for your time and consideration, as well as your guidance on my project and inspiration for programming in general. I hope to continue developing this project into something that will be truly useful, not only to myself or my table, but others in the TTRPG hobby!