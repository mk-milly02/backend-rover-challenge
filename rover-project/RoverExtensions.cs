using Spectre.Console;

namespace rover_project;

public static class RoverExtensions
{
    public static bool ValidateCoordinates(string coordinates)
    {
        string[] coord = coordinates.Split(' ');
        if (coord.Length != 2) return false;
        return int.TryParse(coord[0], out _) && int.TryParse(coord[1], out _);
    }

    public static int CalculateArea(string coordinates)
    {
        string[] coord = coordinates.Split(' ');
        return int.Parse(coord[0]) * int.Parse(coord[1]);
    }

    private static readonly char[] CardinalPoints = new[] { 'N', 'E', 'S', 'W' };
    private static readonly char[] Instructions = new[] { 'L', 'M', 'R' };

    public static bool ValidateCurrentPosition(string position)
    {
        string[] pos = position.Split(' ');
        if (pos.Length != 3) return false;

        return int.TryParse(pos[0], out _) && int.TryParse(pos[1], out _) &&
            char.TryParse(pos[2], out char point) && CardinalPoints.Contains(point);
    }

    public static (int x, int y, char point) GeneratePosition(string position)
    {
        string[] pos = position.Split(' ');
        return (int.Parse(pos[0]), int.Parse(pos[1]), char.Parse(pos[2]));
    }

    public static bool ValidateInstructions(string instructions)
    {
        char[] instr = instructions.Distinct().ToArray();
        if (instr.Length < 1 || instr.Length > 3) return false;

        bool[] x = instr.Select(c => Instructions.Contains(c)).ToArray();
        return !x.Contains(false);
    }

    public static char[] GetInstructions(string instructions)
    {
        return instructions.ToCharArray();
    }

    private static void L(this Rover rover)
    {
        switch (rover.Position.Point)
        {
            case 'N':
                rover.Position = (rover.Position.X, rover.Position.Y, 'W');
                break;
            case 'E':
                rover.Position = (rover.Position.X, rover.Position.Y, 'N');
                break;
            case 'S':
                rover.Position = (rover.Position.X, rover.Position.Y, 'E');
                break;
            case 'W':
                rover.Position = (rover.Position.X, rover.Position.Y, 'S');
                break;
            default:
                break;
        }
    }

    private static void R(this Rover rover)
    {
        switch (rover.Position.Point)
        {
            case 'N':
                rover.Position = (rover.Position.X, rover.Position.Y, 'E');
                break;
            case 'E':
                rover.Position = (rover.Position.X, rover.Position.Y, 'S');
                break;
            case 'S':
                rover.Position = (rover.Position.X, rover.Position.Y, 'W');
                break;
            case 'W':
                rover.Position = (rover.Position.X, rover.Position.Y, 'N');
                break;
            default:
                break;
        }
    }

    private static void M(this Rover rover)
    {
        switch (rover.Position.Point)
        {
            case 'N':
                rover.Position = (rover.Position.X, rover.Position.Y + 1, 'N');
                break;
            case 'E':
                rover.Position = (rover.Position.X + 1, rover.Position.Y, 'E');
                break;
            case 'S':
                rover.Position = (rover.Position.X, rover.Position.Y - 1, 'S');
                break;
            case 'W':
                rover.Position = (rover.Position.X - 1, rover.Position.Y, 'W');
                break;
            default:
                break;
        }
    }

    public readonly static Action<Rover> ExecuteInstructions = (rover) =>
    {
        foreach (char instruction in rover.Instructions!)
        {
            switch (instruction)
            {
                case 'L':
                    rover.L();
                    break;
                case 'M':
                    rover.M();
                    break;
                case 'R':
                    rover.R();
                    break;
                default:
                    break;
            }
        }

        if (rover.Position.X * rover.Position.Y < 0 || rover.Position.X * rover.Position.Y > rover.Area)
        {
            AnsiConsole.MarkupLine($"{rover.Position.X} {rover.Position.Y} {rover.Position.Point}");
            AnsiConsole.MarkupLine("[red]! The rover is outside the plateau's perimeter[/]");
        }

        AnsiConsole.MarkupLine($"{rover.Position.X} {rover.Position.Y} {rover.Position.Point}");
    };
}