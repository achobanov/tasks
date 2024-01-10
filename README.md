## Overview
The tesk is designed to familiarize you with Blazor framework, MudBlazor components suite that will is used in Not and Domain Driven Design (or Clean Architecture).

It is separated in the following parts:

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
Domain Model is the innermost and most important layer of a Domain Driven Design (DDD) application.

[2]: https://www.youtube.com/watch?v=xr56fmgLl74&list=PL4WEkbdagHIR0RBe_P4bai64UDqZEbQap&index=1
[1]: https://mudblazor.com/getting-started/installation#manual-install-add-script-reference