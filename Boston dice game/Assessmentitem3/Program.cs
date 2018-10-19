/*
Author- James Higgins (15620181)
Date: 17/12/17
Change history:
	17/11/17-started project, created classes
	22/11/17-implemented opening menu and two player match play
	1/12/17-implememted two player score play
	6/12/17-introduced error handling for two player modes
	7/12/17-created a one player option
	12/12/17-implemented restart and quit options during and at end of game
	15/12/17-introduced error handling to one player modes
	16/12/17-focused on creating a smooth playing game
	17/12/17-Added extra error handling

*/
using System;
using System.Collections.Generic;
//Game class that runs all the functions required to play the game
class Game
{
    //main function where the program starts
    public static void Main(String[] args)
    {

        String choice;
        bool valid = false;
        int oneportwop = 0;
        String except = "";
        //Gets choice from the user whether they want to play 1 player or two player
        //Uses exception handling to ensure the program does not break
        //uses while loop to make sure the variable oneportwop is valid before being used
        Console.WriteLine("Welcome to Going to Boston!");
        while (valid == false)
        {
            try
            {
                Console.WriteLine();
                Console.WriteLine("1 Player or 2 Player");
                choice = Console.ReadLine();
                oneportwop = Convert.ToInt32(choice);
                if ((oneportwop == 1) || (oneportwop == 2))
                {
                    valid = true;
                }
                else
                {
                    Console.WriteLine("Number isn't a 1 or a 2");
                }
            }
            catch (FormatException e)
            {
                Console.WriteLine("Invalid input please try again");
                except = except + e;
                valid = false;

            }
            catch (Exception e)
            {
                except = except + e;
                Console.WriteLine(e);
                valid = false;
            }
        }

        //If oneportwop is 1 then the game runs the one player code 
        //If oneportwop is 2 then the game runs the two player code
        //No need for error handling as the variable has been validated in the previous loop
        if (oneportwop == 1)
        {
            Game.onePlayer();
        }
        else if (oneportwop == 2)
        {
            Game.twoPlayer();
        }

    }

    //code for the one player version of the game
    public static void onePlayer()
    {
        String ex = "";
        // try catch statement to ensure that if the users computer does not have the memory to create an object it won't crash the program
        try
        {
            //creates instances of the player class for the player and the CPU
            Player player = new Player();
            Player CPU = new Player();
            //Creates an instance of the die class to use
            Die d2 = new Die();
            String endkey = "";
            //Gets the user to choose between match and score play
            Console.WriteLine("Match play or score play?");
            String choice = (Console.ReadLine()).ToLower();
            String playerwin = "";
            //This loop ensures that only "match" or "score" is used 
            while ((!choice.Equals("match")) && (!choice.Equals("score")))
            {
                Console.WriteLine("Please enter either score or match play");
                Console.WriteLine("Match play or score play?");
                choice = (Console.ReadLine()).ToLower();
            }

            int rounds = 1;
            //Runs the code for match play
            if (choice.Equals("match"))
            {
                //Runs the gamne until either the player or the CPu has reached a score of 5
                while ((player.score < 5) && (CPU.score < 5))
                {
                    //Runs the code to play 1 round, the function outputs the winner of the round
                    playerwin = Game.play1pround(d2);
                    //Uses a switch statement with the value returned from the play1pround function to display the winner of the round and update the scores
                    //Displays the overall scores after each round
                    switch (playerwin)
                    {
                        case "player":
                            Console.WriteLine("Player wins round " + rounds);
                            player.updateScore();
                            Console.WriteLine("Player score = " + player.score + " CPU score = " + CPU.score);
                            Console.WriteLine();
                            break;

                        case "CPU":
                            Console.WriteLine("CPU wins round " + rounds);
                            CPU.updateScore();
                            Console.WriteLine("Player score = " + player.score + " CPU score = " + CPU.score);
                            Console.WriteLine();
                            break;

                        case "draw":
                            Console.WriteLine("Round is a draw");
                            Console.WriteLine();
                            break;
                    }


                    rounds += 1;
                }
                //Checks to see if the player or the CPU has won the game and displays the winner to the screen
                if (player.score > CPU.score)
                {
                    Console.WriteLine("Player wins");
                }
                else
                {
                    Console.WriteLine("CPU wins");
                }
                Console.WriteLine("Press r to restart the game or any other key to quit");
                Console.WriteLine();
                //reads the key input from the player and converts it to a string 
                endkey = Console.ReadKey().Key.ToString();
                Console.WriteLine();
                //if the user presses the r key then the program restarted otheriwse it ends
                if (endkey.Equals("R"))
                {
                    Console.WriteLine("You have restarted");
                    Game.onePlayer();
                }
                else
                {
                    Console.WriteLine("You have quit the game");
                    Environment.Exit(0);
                }
                //runs the code for score play 
            }
            else if (choice.Equals("score"))
            {
                //Creates a list for the values of the scores for each round for the player and the CPU 
                List<int> player_scores = new List<int>();
                List<int> CPU_scores = new List<int>();
                //while loop plays 5 rounds of the game
                while (rounds < 6)
                {
                    //Outputs round number to the user
                    //rolls the dice for the player and the user and adds the value to a list of scores
                    //uses the list to get a current score for both the player and the computer and outputs it to the screen 
                    Console.WriteLine("Round number:" + rounds);
                    Console.WriteLine();
                    player_scores.Add(rolldice(d2, "player"));
                    Console.WriteLine("Player total is now " + get_totals(player_scores));
                    Console.WriteLine();

                    CPU_scores.Add(rolldice(d2, "CPU"));
                    Console.WriteLine("CPU total is now " + get_totals(CPU_scores));
                    Console.WriteLine();

                    rounds += 1;
                }
                //Gets the total scores of the player and the computer after five rounds
                player.total = get_totals(player_scores);
                CPU.total = get_totals(CPU_scores);

                //compares player and computer totals and outputs the winner of the game
                if (player.total > CPU.total)
                {
                    Console.WriteLine("Player wins with a score of " + player.total);
                }
                else if (CPU.total > player.total)
                {
                    Console.WriteLine("CPU wins with a score of " + CPU.total);
                }
                else
                {
                    Console.WriteLine("The match is a draw with the scores at " + player.total);
                }

                Console.WriteLine("Press r to restart the game or any other key to quit");
                Console.WriteLine();
                //reads the key input from the player and converts it to a string 
                endkey = Console.ReadKey().Key.ToString();
                Console.WriteLine();
                //if the user presses the r key then the program restarted otheriwse it ends
                if (endkey.Equals("R"))
                {
                    Console.WriteLine("You have restarted");
                    Game.onePlayer();
                }
                else
                {
                    Console.WriteLine("You have quit the game");
                    Environment.Exit(0);
                }

            }
            else
            {
                Console.WriteLine("Invalid choice");
            }
        }
        catch (OutOfMemoryException e)
        {
            ex = ex + e;
            Console.WriteLine("You have ran out of memory to create this object");
        }
        catch (Exception e)
        {
            ex = ex + e;
            Console.WriteLine("An exception has occured");
        }
    }

    public static int rolldice(Die d, String user)
    {
        int highest = 0;
        int tmp = 0;
        int roundtotal = 0;
        //if it is the players turn then the computer prompts the player to press a key depending on what they want to do
        //the if statement means that if it is the computers turns then it will not ask for a key to be pressed
        if (user.Equals("player"))
        {
            Console.WriteLine("Player: press ENTER to roll");
            Console.WriteLine("        press r to restart");
            Console.WriteLine("        press q to quit");
            Console.WriteLine();
            //reads the key inputted by the player and converts in to a string so it can be used in a switch statement to decide what the program should do next 
            String key = Console.ReadKey().Key.ToString();
            Console.WriteLine();
            switch (key)
            {
                case "Enter":
                    break;
                case "R":
                    Console.WriteLine("You have restarted");
                    Game.onePlayer();
                    break;
                case "Q":
                    Console.WriteLine("You have quit the game");
                    Environment.Exit(0);
                    break;
            }
        }
        //rolls the dice
        //runs three times with the number of dice being rolled decreasing each time
        for (int j = 3; j > 0; j--)
        {
            highest = 0;
            for (int i = 0; i < j; i++)
            {
                tmp = d.roll();
                if (tmp > highest)
                {
                    highest = tmp;
                }
            }
            //highest roll of each run is added to a total for that round
            roundtotal += highest;
        }
        //inputs the players score for that round and returns it 
        Console.WriteLine(user + " scored " + roundtotal);
        return roundtotal;

    }

    public static String play1pround(Die d)
    {
        //uses the rolldice function to get a total for the round for each player 
        int proll = 0;
        proll = Game.rolldice(d, "player");
        int cpuroll = 0;
        cpuroll = Game.rolldice(d, "CPU");

        //compares the two round totals and returns the winner of the round
        String winner = "";
        if (proll > cpuroll)
        {
            winner = "player";
        }
        if (cpuroll > proll)
        {
            winner = "CPU";
        }
        if (proll == cpuroll)
        {
            winner = "draw";
        }
        return winner;
    }

    public static void twoPlayer()
    {
        String ex = "";
        // try catch statement to ensure that if the users computer does not have the memory to create an object it won't crash the program
        try
        {
            int rounds = 1;

            //creates two new instances of the player class, player1 and player2 and a new instance of the dice class, d
            Player player1 = new Player();
            Player player2 = new Player();
            Die d = new Die();
            String endkey = "";

            //gets the players to choose between match and score play
            Console.WriteLine("Match play or score play?");
            String choice = (Console.ReadLine()).ToLower();
            String playerwin = "";
            //loop to ensure that only "match" or "score" can be used
            while ((!choice.Equals("match")) && (!choice.Equals("score")))
            {
                Console.WriteLine("Please enter either score or match play");
                Console.WriteLine("Match play or score play?");
                choice = (Console.ReadLine()).ToLower();
            }
            //if the players entered match then the match code is run
            if (choice.Equals("match"))
            {
                //runs the game until one of the players has a score 5
                while ((player1.score < 5) && (player2.score < 5))
                {
                    //runs the playround function to get a winner for each round
                    playerwin = Game.playround(d);
                    //switch statement to show which player won the round, the winner is displayed to the screen, the scores are updated and the current score in rounds is displayed
                    switch (playerwin)
                    {
                        case "player1":
                            Console.WriteLine("Player1 wins round " + rounds);
                            player1.updateScore();
                            Console.WriteLine("Player1 score = " + player1.score + " Player2 score = " + player2.score);
                            Console.WriteLine();
                            break;

                        case "player2":
                            Console.WriteLine("Player2 wins round " + rounds);
                            player2.updateScore();
                            Console.WriteLine("Player1 score = " + player1.score + " Player2 score = " + player2.score);
                            Console.WriteLine();
                            break;

                        case "draw":
                            Console.WriteLine("Round is a draw");
                            Console.WriteLine();
                            break;
                    }


                    rounds += 1;
                }

                //compares the players scores after the game has finished and displays the winning player to the screen
                if (player1.score > player2.score)
                {
                    Console.WriteLine("Player1 wins");
                }
                else
                {
                    Console.WriteLine("Player2 wins");
                }

                Console.WriteLine("Press r to restart the game or any other key to quit");
                Console.WriteLine();
                //reads the key input from the player and converts it to a string 
                endkey = Console.ReadKey().Key.ToString();
                Console.WriteLine();
                //if the user presses the r key then the program restarted otheriwse it ends
                if (endkey.Equals("R"))
                {
                    Console.WriteLine("You have restarted");
                    Game.twoPlayer();
                }
                else
                {
                    Console.WriteLine("You have quit the game");
                    Environment.Exit(0);
                }

                //if the players entered score then the score code is run	
            }
            else if (choice.Equals("score"))
            {
                //creates lists that hold the score of each round for both players
                List<int> p1_scores = new List<int>();
                List<int> p2_scores = new List<int>();
                //plays six rounds of the game
                while (rounds < 6)
                {
                    //display round number to users
                    //rolls the dice for that round for each player
                    //adds each score to the list and displays a running total after each round
                    Console.WriteLine("Round number:" + rounds);
                    Console.WriteLine();
                    p1_scores.Add(rolldice(d, 1));
                    Console.WriteLine("Player1 total is now " + get_totals(p1_scores));
                    Console.WriteLine();

                    p2_scores.Add(rolldice(d, 2));
                    Console.WriteLine("Player2 total is now " + get_totals(p2_scores));
                    Console.WriteLine();

                    rounds += 1;
                }
                //sets the total scores of the players after the five rounds
                player1.total = get_totals(p1_scores);
                player2.total = get_totals(p2_scores);

                //compares the total scores for the players and displays the winner for the game
                if (player1.total > player2.total)
                {
                    Console.WriteLine("Player1 wins with a score of " + player1.total);
                }
                else if (player2.total > player1.total)
                {
                    Console.WriteLine("Player2 wins with a score of " + player2.total);
                }
                else
                {
                    Console.WriteLine("The match is a draw with the scores at " + player1.total);
                }

                Console.WriteLine("Press r to restart the game or any other key to quit");
                Console.WriteLine();
                //reads the key input from the player and converts it to a string 
                endkey = Console.ReadKey().Key.ToString();
                Console.WriteLine();
                //if the user presses the r key then the program restarted otheriwse it ends
                if (endkey.Equals("R"))
                {
                    Console.WriteLine("You have restarted");
                    Game.twoPlayer();
                }
                else
                {
                    Console.WriteLine("You have quit the game");
                    Environment.Exit(0);
                }


            }
            else
            {
                Console.WriteLine("Invalid choice");
            }
        }
        catch (OutOfMemoryException e)
        {
            ex = ex + e;
            Console.WriteLine("You have ran out of memory to create this object");
        }
        catch (Exception e)
        {
            ex = ex + e;
            Console.WriteLine("An exception has occured");
        }
    }


    //function that goes through the list of scores for each player and gets the total+
    public static int get_totals(List<int> a)
    {
        int t = 0;
        for (int b = 0; b < a.Count; b++)
        {
            t += a[b];
        }
        return t;
    }

    //function thats plays a round of match play for two players
    public static String playround(Die d)
    {
        //rolls the dice for each player for that round 
        int p1roll = 0;
        p1roll = Game.rolldice(d, 1);
        int p2roll = 0;
        p2roll = Game.rolldice(d, 2);

        //compares the two dice rolls and returns the winner of the round
        String winner = "";
        if (p1roll > p2roll)
        {
            winner = "player1";
        }
        if (p2roll > p1roll)
        {
            winner = "player2";
        }
        if (p1roll == p2roll)
        {
            winner = "draw";
        }
        return winner;
    }

    //function that rolls the dice 
    public static int rolldice(Die d, int player)
    {
        int highest = 0;
        int tmp = 0;
        int roundtotal = 0;

        //prompts the player to press a key to decide what to option they want to do
        Console.WriteLine("Player" + player + ": press ENTER to roll");
        Console.WriteLine("         press r to restart");
        Console.WriteLine("         press q to quit");
        Console.WriteLine();
        //reads the key input from the player and converts it to a string 
        String key = Console.ReadKey().Key.ToString();
        Console.WriteLine();
        //switch statement decides what the program should do next by looking at what key the user has pressed
        switch (key)
        {
            case "Enter":
                break;
            case "R":
                Console.WriteLine("You have restarted");
                Game.twoPlayer();
                break;
            case "Q":
                Console.WriteLine("You have quit the game");
                Environment.Exit(0);
                break;
        }
        //rolls the dice
        //runs three rounds with the number of dice decreasing by one each time
        for (int j = 3; j > 0; j--)
        {
            highest = 0;
            for (int i = 0; i < j; i++)
            {
                tmp = d.roll();
                if (tmp > highest)
                {
                    highest = tmp;
                }
            }
            //gets the highest score from each round
            roundtotal += highest;
        }
        //returns and outputs the player score for that round
        Console.WriteLine("Player" + player + " scored " + roundtotal);
        Console.WriteLine();
        return roundtotal;

    }

}
//die class creates die objects and has function to roll the dice
class Die
{
    //creates a random object so the die can be rolled 
    Random r = new Random();

    //rolls the die, returns a number between 1 and 6
    public int roll()
    {
        int randomroll = r.Next(1, 7);
        return randomroll;
    }

}

//player class create player objects and houses functions to update the score
class Player
{
    //score and total attributes
    public int score;
    public int total;
    //constructor for each player created
    public Player()
    {
        score = 0;
        total = 0;
    }
    //updates the player score if they win a round
    public void updateScore()
    {

        score += 1;
    }
}