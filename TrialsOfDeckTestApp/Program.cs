using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TODBasics;
using TODGenerators;
using TODBattleSystem;

namespace TrialsOfDeckTestApp {

    class Program {
        public static BattleSystem bs;
        public static Game game;

        static void Main(string[] args) {
            Collection col = new Collection();
            col.Init();

            Character warrior = new Character("Warrior", 1);
            Character huntress = new Character("Huntress", 2);
            Character magus = new Character("Magus", 3);
            warrior.SetDeck(DeckGenerator.NewDeck(ref warrior, ref col));
            huntress.SetDeck(DeckGenerator.NewDeck(ref huntress, ref col));
            magus.SetDeck(DeckGenerator.NewDeck(ref magus, ref col));

            Console.WriteLine("Welcome to the Trials of Deck prototype build.");
            Console.WriteLine("\nThis build was designed to test the battle mechanics. \nYou will have three heroes fight a against a monster. Here are the rules:");
            Console.WriteLine(" 1. A battle round is played in turns: first the player turn then the enemy turn.");
            Console.WriteLine(" 2. You will start wih a hand of five card, each card represents an abillity of a hero.");
            Console.WriteLine(" 3. Click the number of the card to play it, alternatevly you can click '6' to get a new hand of cards for the turn.");
            Console.WriteLine(" 4. Some cards require you to pick a target (except for attack cards in this prototype), select one of them to use the card.");
            Console.WriteLine(" 5. If no avalible targets exist, the card is unusable during that turn and you must pick a new card.");
            Console.WriteLine(" 6. Some cards can cause States, which take effect at the start of each battle round.");
            Console.WriteLine(" 7. The game ends if you kill the monster or all of your heroes die.");
            Console.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++\n");

            Console.WriteLine("What do you want to do?\n 1: Play the game.\n 0: Exit the game");
            string s = Console.ReadLine();
            if ( Convert.ToBoolean( Convert.ToByte(s) ) )  {
                bool stop_game = false;

                game = new Game(); //Publisher
                bs = new BattleSystem(ref warrior, ref huntress, ref magus); //Subscriber

                //Atrributes Functions to the EventHandlers
                game.OptionReceived += bs.OnOptionReceived;
                game.EnemyOption += bs.OnEnemyTurn;
                game.GameEndsCheck += bs.OnGameEndsCheck;
                game.StateEffectsCall += bs.OnStateEffectCall;
                game.DisplayCall += bs.OnDisplayCall;

                while (!stop_game) {
                    Console.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++\n");
                    
                    bool victory = game.Run(ref bs);

                    Console.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++\n");
                    if (victory)
                        Console.WriteLine("Player Won!\n");
                    else
                        Console.WriteLine("Player Lost!\n");

                    Console.WriteLine("Would You like to play again? (1-Yes, 0-No)");
                    s = Console.ReadLine();
                    stop_game = !Convert.ToBoolean(Convert.ToByte(s));
                }
            }
        }
    } 
}

