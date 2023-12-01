using System;

using System.Collections.Generic;

using System.Linq;

using System.Threading;



class Program

{

    static void Main()
    {
        Console.WindowHeight = 16;
        Console.WindowWidth = 32;

        int screenwidth = Console.WindowWidth;
        int screenheight = Console.WindowHeight;

        Random randomnummer = new Random();

        Pixel player1 = new Pixel();
        player1.xPos = screenwidth / 4;
        player1.yPos = screenheight / 2;
        player1.schermKleur = ConsoleColor.Red;

        Pixel player2 = new Pixel();
        player2.xPos = 3 * screenwidth / 4;
        player2.yPos = screenheight / 2;
        player2.schermKleur = ConsoleColor.Blue;

        string movementPlayer1 = "RIGHT";
        string movementPlayer2 = "LEFT";

        List<Pixel> bodyPlayer1 = new List<Pixel>();
        List<Pixel> bodyPlayer2 = new List<Pixel>();

        int scorePlayer1 = 0;
        int scorePlayer2 = 0;

        Pixel obstacle = new Pixel();
        obstacle.xPos = randomnummer.Next(1, screenwidth - 1);
        obstacle.yPos = randomnummer.Next(1, screenheight - 1);
        obstacle.schermKleur = ConsoleColor.Cyan;

        while (true)
        {
            Console.Clear();

            Console.ForegroundColor = obstacle.schermKleur;
            Console.SetCursorPosition(obstacle.xPos, obstacle.yPos);
            Console.Write("■");

            Console.ForegroundColor = player1.schermKleur;
            Console.SetCursorPosition(player1.xPos, player1.yPos);
            Console.Write("■");

            Console.ForegroundColor = player2.schermKleur;
            Console.SetCursorPosition(player2.xPos, player2.yPos);
            Console.Write("■");

            Console.ForegroundColor = ConsoleColor.White;
            for (int i = 0; i < screenwidth; i++)
            {
                Console.SetCursorPosition(i, 0);
                Console.Write("■");
                Console.SetCursorPosition(i, screenheight - 1);
                Console.Write("■");
            }

            for (int i = 0; i < screenheight; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("■");
                Console.SetCursorPosition(screenwidth - 1, i);
                Console.Write("■");
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(screenwidth / 2 - 5, 0);
            Console.Write($"Player 1 Score: {scorePlayer1} | Player 2 Score: {scorePlayer2}");

            MoveSnake(ref player1, ref bodyPlayer1, movementPlayer1, screenwidth, screenheight);
            MoveSnake(ref player2, ref bodyPlayer2, movementPlayer2, screenwidth, screenheight);

            if (player1.xPos == obstacle.xPos && player1.yPos == obstacle.yPos)
            {
                scorePlayer1++;
                obstacle.xPos = randomnummer.Next(1, screenwidth - 1);
                obstacle.yPos = randomnummer.Next(1, screenheight - 1);
            }

            if (player2.xPos == obstacle.xPos && player2.yPos == obstacle.yPos)
            {
                scorePlayer2++;
                obstacle.xPos = randomnummer.Next(1, screenwidth - 1);
                obstacle.yPos = randomnummer.Next(1, screenheight - 1);
            }

            if (CheckCollision(player1, bodyPlayer1, screenwidth, screenheight) || CheckCollision(player2, bodyPlayer2, screenwidth, screenheight))
            {
                GameOver(scorePlayer1, scorePlayer2);
            }

            DrawSnakeBody(bodyPlayer1, player1.schermKleur);
            DrawSnakeBody(bodyPlayer2, player2.schermKleur);

            Thread.Sleep(100);
        }
    }

    static void MoveSnake(ref Pixel player, ref List<Pixel> body, string direction, int screenwidth, int screenheight)
    {
        Pixel previousPosition = new Pixel { xPos = player.xPos, yPos = player.yPos };

        switch (direction)
        {
            case "UP":
                player.yPos = (player.yPos - 1 + screenheight) % screenheight;
                break;
            case "DOWN":
                player.yPos = (player.yPos + 1) % screenheight;
                break;
            case "LEFT":
                player.xPos = (player.xPos - 1 + screenwidth) % screenwidth;
                break;
            case "RIGHT":
                player.xPos = (player.xPos + 1) % screenwidth;
                break;
        }

        if (body.Count > 0)
        {
            body.Insert(0, previousPosition);
            body.RemoveAt(body.Count - 1);
        }
    }

}



public class Pixel

{

    public int xPos { get; set; }

    public int yPos { get; set; }

    public ConsoleColor schermKleur { get; set; }

    public string karacter { get; set; }

}


