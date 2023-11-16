# Game Engine/ Code Connoisseurs
​
> _Note:_ This document is intended to be relatively short. Be concise and precise. Assume the reader has no prior knowledge of your application and is non-technical. 
​
## Partner Intro
 * Include the names, emails, titles, primary or secondary point of contact at the partner organization
 * Provide a short description about the partner organization. (2-4 lines)
 
 James Rhule, jamesrhule@projecthumancity.com, Project: Human City organization leader, primary contact
 
 P.S. will introduce one developer and one project manager later October due to scheduling chanllenge.

 Project: Human City is a non-profit organization who try to design and create an open ecosystem. They initiate ideas for global impact, focusing on addressing human inequality, social injustice, and access to basic needs. 

## Description about the project
Keep this section very brief.
 * Provide a high-level description of your application and it's value from an end-user's perspective
 * What is the problem you're trying to solve? Is there any context required to understand **why** the application solves this problem?

City building game is a web-based creative city-building game that aims to connect users closer to the real world. Users obtain inventory resources to build their city by scanning objects (ex: cars, buildings, infrastructures, etc.) in the real world, using the AR camera in the Spotstich mobile app (Spotstich mobile app is a multi-purpose social app developed by Project: Human City). 

Our product could potentially revolutionize how users engage their physical environment with virtual games. It will encourage people to explore the local environment and surrounding objects, capture and record them with a vivid 3D model instead of a 2D picture. For real life urban planners, this product might improve their efficiency of initializing city planning and inspire them with creative and effective design. 

With this interactive game developed, we hope to enhance users’ real world exploration and social interaction which could yield significant positive social impact. This also aligns with the mission of our partner, Project: Human City. As a non-profit organization, Project: Human City aims to enhance the lives of city residents on and off the app with various initiatives and projects. We together want to create a difference in people’s lives.

​
## Key Features
 * Describe the key features in the application that the user can access.
 * Provide a breakdown or detail for each feature.
 * This section will be used to assess the value of the features built

City building game (Web game):
 * Add objects from inventory to selected location on the game map
 * Delete selected object from the game map
 * View friends' city design

Spotstich Mobile App:
 * Trade for different "skin" of objects in inventory of the Spotstich Mobile App to enhance the appearance of objects
 * Scan objects using the AR camera API and store scanned object into the user's inventory
​
## Instructions
 * Clear instructions for how to use the application from the end-user's perspective
 * How do you access it? For example: Are accounts pre-created or does a user register? Where do you start? etc. 
 * Provide clear steps for using each feature described in the previous section.
 * This section is critical to testing your application and must be done carefully and thoughtfully.

Todo: we lack the url for the client side

For new users:
Upon reaching the Login & Register screen, input the required information—username, email, and password—then click the register button. Subsequently, the Login & Register scene will vanish, unveiling a newly generated map in the game scene.

For existing users:
Upon arriving at the Login & Register screen, existing users only need to input their email and password, then click the login button. The Login & Register scene will then disappear, revealing the user's previously saved map from the last session within the game scene.

Once in the game scene, users can interact by selecting items from the inventory box located at the top left. They can place the chosen item onto the map by dragging it to the desired position and clicking once more. To remove an item from the map, users can click the item already placed on the map and press the "esc" button.

Upon deciding to log out, the map will automatically save any changes made by the user. By clicking the log-out button, the map will exit, and the user will be successfully logged out.


 
 ## Development requirements
 * What are the technical requirements for a developer to set up on their machine or server (e.g. OS, libraries, etc.)?
 * Briefly describe instructions for setting up and running the application. You should address this part like how one would expect a README doc of real-world deployed application would be.
 * You can see this [example](https://github.com/alichtman/shallow-backup#readme) to get started.

(Will update after we reach the implementation stage of the project)
 
 ## Deployment and Github Workflow
​
Describe your Git/GitHub workflow. Essentially, we want to understand how your team members share codebase, avoid conflicts and deploys the application.
​
 * Be concise, yet precise. For example, "we use pull-requests" is not a precise statement since it leaves too many open questions - Pull-requests from where to where? Who reviews the pull-requests? Who is responsible for merging them? etc.  
 * If applicable, specify any naming conventions or standards you decide to adopt.
 * Describe your overall deployment process from writing code to viewing a live application
 * What deployment tool(s) are you using? And how?
 * Don't forget to **briefly justify why** you chose this workflow or particular aspects of it!

**Pull request:**  
We separated ourselves into two subteams (web game and mobile app), three people per team. When a team member starts a pull request, at least one person from their subteam needs to review the pull request in order to merge the code. The pull request needs to have detailed descriptions of what is done/changed in each file. Ideally one should create a pull request after each task they finish and name the pull request as the task they worked on.   

**Naming conventions:**  
We will follow the standard naming conventions for each language/framework (ie. Camel case for C#)  

**Deployment:**  
(Will update after we reach the implementation stage of the project)  

 ## Coding Standards and Guidelines
 Keep this section brief, a maximum of 2-3 lines. You would want to read through this [article](https://www.geeksforgeeks.org/coding-standards-and-guidelines/) to get more context about what this section is for before attempting to answer.
  * These are 2 optional resources that you might want to go through: [article with High level explanation](https://blog.codacy.com/coding-standards-what-are-they-and-why-do-you-need-them/) and [this article with Detailed Explanation](https://google.github.io/styleguide/)

The web game (mainly done in Unity) will follow [Google C# Style Guide](https://google.github.io/styleguide/csharp-style.html). The mobile app will follow ​[Google JavaScript Style Guide](https://google.github.io/styleguide/jsguide.html).

 ## Licenses 
​
 Keep this section as brief as possible. You may read this [Github article](https://help.github.com/en/github/creating-cloning-and-archiving-repositories/licensing-a-repository) for a start.
​
 * What type of license will you apply to your codebase? And why?
 * What affect does it have on the development and use of your codebase?

We are using [Creative Commons Attribution-NonCommercial-ShareAlike 4.0 International Public License](https://creativecommons.org/licenses/by-nc-sa/4.0/legalcode.en) by the requirement of our partner. Anyone is free to share and adapt materials of this code base given that they do the following:  
 * give appropriate credit, provide a link to the license, and indicate if changes were made
 * do not use the codebase for commercial purposes
 * use the same license as this codebase, when you share and adapt the material
 * do not restrict others from doing anything this license permits
