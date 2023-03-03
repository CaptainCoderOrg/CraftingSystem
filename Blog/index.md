---
title: RPG Crafting System
layout: home
nav_order: 0
---

# Let's Make an RPG Crafting System
{: .no_toc }

![Crafting System](imgs/crafting-system.png)

Hello coders! Captain Coder here with another learning series. On the Captain
Coder's Academy discord, it was proposed that I implement a crafting system live
on stream. This site will (hopefully) serve to chronicle the streams for anyone
who missed them / wants to catch up.

* Archived Streams Playlist: [Playlist]
* Catch the Captain Live on Twitch: [Twitch]
* Source Code: [Repository]

## Day 1 - Design Document and Project Scope

The crew began work on the initial version of the Crafting System. We defined
our learning goals, created a design document, specified the scope of the
project, set up a new Unity project, and wrote a simple implementation of a
[ShapelessRecipe].

* [Read More]({% link pages/01-day-1.md %})
* [Watch On YouTube](https://youtube.com/live/_S4JNwdGPEo?feature=share)

## Day 2 - Using DLLs and Defining a Crafting Container

The crew continued work on the Crafting System. Today, we explored how to link
an external **DLL** within a Unity project, began the `CaptainCoder.Core`
library, defined the API for the [CraftingContainer] class, and began writing a
unit tests for [CraftingContainer].

* [Read More]({% link pages/02-day-2.md %})
* [Watch On YouTube](https://youtube.com/live/IA66jZh51h8)


## Day 3: Scriptable Objects and Recipe Database

Today, we finished an implementation of the [CraftingContainer].
Additionally, we took our first dive into `ScriptableObjects` by defining
scriptable object types for items, recipes, and recipe categories! Lastly, we
started work on an implementation of the [RecipeDatabase] class.

* [Read More]({% link pages/03-day-3.md %})
* [Watch On YouTube](https://youtube.com/live/6p3TJ3fbHe4)

## Day 4: Interfaces and Exploring UI Toolkit"

Today, we finished a implementing the `RecipeDatabase` class, refactored
the CraftingSystem to expose additional interfaces, implemented a
`CraftingContainerData` ScriptableObject, and began exploring using 
the UI Toolkit in play mode.

* [Read More]({% link pages/04-day-4.md %})
* [Watch On YouTube](https://youtube.com/live/N8lzDTX7_GM)


{% include Links.md %}