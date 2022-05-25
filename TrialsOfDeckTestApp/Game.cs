using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TODBattleSystem;

namespace TrialsOfDeckTestApp
{
    // Event Arguments for the player options selection
    class OptionEventArgs : EventArgs
    {
        public int option;
        public bool turn_ends;
        public bool reshuffled;

        public OptionEventArgs()
        {
            option = 0;
            turn_ends = false;
            reshuffled = false;
        }
    }

    //Event Arguments for Enemy option
    class EnemyOptionEvent : EventArgs
    {
        public int option;
        public EnemyOptionEvent() { option = 0; }
    }

    //Event Arguments for the Game Ends validation
    class GameEndsEventArgs : EventArgs
    {
        public bool game_ends;
        public bool winner;
        public GameEndsEventArgs() { 
            game_ends = false;
            winner = false;
        }

    }
    //Event Arguments for the Game Display (fullLog is debug purposes)
    class DisplayEventArgs : EventArgs
    {
        public bool fullLog = false; 
        public DisplayEventArgs(){
            bool fullLog = false;
        }
    }

    class Game {

        // Simpler way to create a EventHandler
        public event EventHandler<OptionEventArgs> OptionReceived;
        public event EventHandler<EnemyOptionEvent> EnemyOption;
        public event EventHandler<GameEndsEventArgs> GameEndsCheck;
        //Calls battle system to do the state effects and check if the game is over
        public event EventHandler<GameEndsEventArgs> StateEffectsCall;
        public event EventHandler<DisplayEventArgs> DisplayCall;

        public static Random _c = new Random();

        //  Raise the Events
        protected virtual void OnOptionReceived(OptionEventArgs args)
        {
            if (OptionReceived != null)
                OptionReceived(this, args);
        }
        
        protected virtual void OnEnemyOptionGenerated(EnemyOptionEvent args)
        {
            if (EnemyOption != null)
                EnemyOption(this, args);
        }
       
        protected virtual void OnGameEndsCheck(GameEndsEventArgs args)
        {
            if ( GameEndsCheck != null)
                GameEndsCheck(this, args);
        } 
        
        protected virtual void OnSateEffectsCall(GameEndsEventArgs args)
        {
            if (StateEffectsCall != null)
                StateEffectsCall(this, args);
        }
        protected virtual void OnDisplayCall(DisplayEventArgs args)
        {
            if (DisplayCall != null)
                DisplayCall(this, args);
        }


        public void PlayerTurn()
        {
            OptionEventArgs args = new OptionEventArgs();
            while (!args.turn_ends)
            {
                if (args.reshuffled)
                    Console.WriteLine("\nPick a card (1-5)");
                else
                    Console.WriteLine("\nPick a card (1-5) or Ask for a new Hand (6)");
                
                //Read card option (number-1);
                int option = Convert.ToInt32(Console.ReadLine());
                if (option > 7 || option <= 0) {
                    Console.WriteLine("\nInvalid card, Try again.");
                    continue;
                }
                else {
                    args.option = option;
                    OnOptionReceived(args);
                }
            }
        }

        public void EnemyTurn()
        {
            EnemyOptionEvent args = new EnemyOptionEvent() { option = _c.Next(5) };
            OnEnemyOptionGenerated(args);
        }

        public bool Run(ref BattleSystem bs)
        {
            int round = 1;
            GameEndsEventArgs gameState = new GameEndsEventArgs();
            DisplayEventArgs displayArgs = new DisplayEventArgs();
            //displayArgs.fullLog = true; //debug purposes

            Console.WriteLine("WAVE 1");
            while (!gameState.game_ends)
            {
                Console.WriteLine("Round {0}\n", round);
                //Do the states effects damage and check if game ends
                OnSateEffectsCall(gameState);
                Console.WriteLine();
                if (gameState.game_ends) break;

                // bs.Display(); // Create a Eventhandler for the Display Option
                OnDisplayCall(displayArgs);

                //Player turn
                PlayerTurn();

                //Perform Enemy turn;
                EnemyTurn(); 
 
                Console.WriteLine("----------------------------------------------------------------------------------\n");
                //Game ends if heroes die or monsters die
                OnGameEndsCheck(gameState); 

                round++;
                Console.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++\n");
            }
            return gameState.winner;
        }
    }
}
