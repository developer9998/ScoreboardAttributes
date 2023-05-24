<div align="center">
 <h1>ScoreboardAttributes</h1>
 <a href="https://github.com/developer9998/ScoreboardAttributes/blob/main/LICENSE/">   
 <img src="https://img.shields.io/github/license/developer9998/ScoreboardAttributes?label=License&style=flat-square"</img></a>
 <a href="https://github.com/developer9998/ScoreboardAttributes/releases/latest">
 <img src="https://img.shields.io/github/downloads/developer9998/ScoreboardAttributes/total?label=Total%20Downloads&style=flat-square"<img></a>
 <a href="https://discord.gg/dev9998">
 <img src="https://img.shields.io/discord/989239017511989258?label=Dev%27s%20Discord&style=flat-square"</img></a>
</div>

## What is ScoreboardAttributes?
ScoreboardAttributes is a library for Gorilla Tag that gives players that show up on the Scoreboards their own attributes. 

## For Developers
Before you plan on adding or removing any attributes, you must link the library to your Visual Studio project.

**1. Right click "Dependencies" and then click "Add Project Reference..."**<br>
<img src="https://github.com/developer9998/ScoreboardAttributes/blob/main/Marketing/Ref1.png" width=35% height=35%>

**2. Go the the "Browse" tab and then click "Browse..."**<br>
<img src="https://github.com/developer9998/ScoreboardAttributes/blob/main/Marketing/Ref2.png" width=62% height=62%>

**3. Locate the library's DLL file, and then click on "OK"**
<img src="https://github.com/developer9998/ScoreboardAttributes/blob/main/Marketing/Ref3.png" width=62% height=62%>

### Adding/Removing attributes
Both adding and removing your own attributes is really simple.

To add an attribute, you will need the target player and the attribute name.<br>
```cs
PlayerTexts.RegisterAttribute("Hello world", Photon.Pun.PhotonNetwork.LocalPlayer);
// This gives our local player an attribute. Our attribute is "Hello world".
```

To remove an attribute, you will only need the target player.<br>
**NOTE: This only removes attributes created from the same mod.**<br>
```cs
PlayerTexts.UnregisterAttribute(Photon.Pun.PhotonNetwork.LocalPlayer);
// This removes our local player's attribute added in this mod.
```

#### *This product is not affiliated with Gorilla Tag or Another Axiom LLC and is not endorsed or otherwise sponsored by Another Axiom LLC. Portions of the materials contained herein are property of Another Axiom LLC. © 2021 Another Axiom LLC.*
