// See https://aka.ms/new-console-template for more information

using rover_project;
using Spectre.Console;

Rover rover = new();

string coordinates = AnsiConsole.Prompt(
    new TextPrompt<string>("? Input the upper-right coordinates of the plateau: ")
    .PromptStyle(new Style(decoration: Decoration.Bold))
    .Validate(input => RoverExtensions.ValidateCoordinates(input)
    ? ValidationResult.Success()
    : ValidationResult.Error("[red]! The coordinates entered are invalid. [/]")));

rover.Area = RoverExtensions.CalculateArea(coordinates);

string position = AnsiConsole.Prompt(
    new TextPrompt<string>("? Input the rover's current position: ")
    .PromptStyle(new Style(decoration: Decoration.Bold))
    .Validate(input => RoverExtensions.ValidateCurrentPosition(input)
    ? ValidationResult.Success()
    : ValidationResult.Error("[red]! The position provided is invalid[/]")));

rover.Position = RoverExtensions.GeneratePosition(position);

string instructions = AnsiConsole.Prompt(
    new TextPrompt<string>("? Enter a series of instructions: ")
    .PromptStyle(new Style(decoration: Decoration.Bold))
    .Validate(input => RoverExtensions.ValidateInstructions(input)
    ? ValidationResult.Success()
    : ValidationResult.Error("[red]! The instructions provided are invalid[/]")));

rover.Instructions = RoverExtensions.GetInstructions(instructions);

RoverExtensions.ExecuteInstructions(rover);