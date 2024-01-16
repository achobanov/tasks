## Overview
The tesk is designed to familiarize you with Blazor framework, MudBlazor components suite that will is used in Not and Domain Driven Design (or Clean Architecture).
To start, fork this repo (challenge/toni branch) and start working through the following stages

## Part 1: Blazor
[Blazor Crash Course][2] - first 9 chapters, rest is optional for now

Goes through core Blazor concept such as Pages, Components, Binding, Rendering, Navigation, Parameters and so on.

## Part 2: MudBlazor
[MudBlazor][1] is a free open-source suite of components that are frankly necessary, as MS provides very little out of the box in Blazor. 

For this part, familiarize yourself with the `vl-challenge` solution:
- `VL.Challenge.Blazor.Client` - a Blazor client app. You can run it using VisualStudio 2022 edition
- `VL.Challenge.Domain` - contains the Domain Model for the application according to DDD standards. You don't need to bother about it at this stage as we'll dive later on.

and then convert change the implementation to use MudBlazor components instead of the plain HTML/BLazor you see currently.

## Part 3: Domain Model
Domain Model is the innermost and most important layer of a Domain Driven Design (DDD) application. See [basics of DDD][3] and [hexagonal architecture][4] for a high-level overview. I expect some stuff will not be clear so I think this is a good time to talk.

Afte that I want you to create a new Domain entity `Calendar`. It has to:
- concern itself only with the current week - no need to know about months and years. It will always display 7 days
- it has to indicate which day is today
- it has to show all tasks the user has for every day

The core Domain logic should be contained in the Domain entity `Calendar`, which should be used to render the calendar view in a separate Page on the Blazor UI. The `Calendar` entity does not need to be persisted, as it holds no additional data to `User` and `VLTask`

[2]: https://www.youtube.com/watch?v=xr56fmgLl74&list=PL4WEkbdagHIR0RBe_P4bai64UDqZEbQap&index=1
[1]: https://mudblazor.com/getting-started/installation#manual-install-add-script-reference
[3]: https://www.youtube.com/watch?v=4rhzdZIDX_k
[4]: https://www.youtube.com/watch?v=bDWApqAUjEI&t=1s
