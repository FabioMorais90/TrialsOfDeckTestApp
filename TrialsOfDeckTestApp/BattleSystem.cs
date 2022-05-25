using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TODBasics;
using TODGenerators;

using TrialsOfDeckTestApp;

namespace TODBattleSystem {
    class BattleSystem {
        //Varaibles
        private Battler[] allies;
        private Battler[] enemies;
        private BattleDeck player_deck;
        private BattleDeck enemy_deck;
        private Hand player_hand;
        private Hand enemy_hand;

        public static Random _r = new Random();

        //Methods
        public BattleSystem(ref Character warrior, ref Character huntress, ref Character magus) {
            allies = new Battler[3];
            allies[0] = new Battler(ref warrior, 200, "pictureWarrior");
            allies[1] = new Battler(ref huntress, 200, "pictureHuntress");
            allies[2] = new Battler(ref magus, 200, "pictureMagus");
            player_deck = new BattleDeck(warrior.GetDeck(), huntress.GetDeck(), magus.GetDeck());
            player_hand = new Hand();
            for (int i = 0; i < 5; i++)
            {
                Card c = player_deck.FromTop();
                player_hand.Put(c);
            }

            enemies = null;
            enemy_hand = null;
            enemy_deck = null;

            InitWave(1);
        }


        //Method for event Handling player Option
        public void OnOptionReceived(object source, OptionEventArgs args)
        {
            switch (args.option)
            {
                case 6:
                    {
                        Reshuffle();
                        args.reshuffled = true;
                        HandDisplay();
                        break;
                    }
                default:
                    { 
                        args.turn_ends = PlayCard(args.option - 1);
                        break;
                    }
            }
        }

        //Method for handling calls for EnemyTurn
        public void OnEnemyTurn(object source, EnemyOptionEvent args)
        {
            EnemyTurn(args.option);
        }

        //Method for handling calls for End Game
        public void OnGameEndsCheck(object source, GameEndsEventArgs args)
        {
            args.game_ends = GameEnds(out args.winner);
        }

        //Method for handling calls for State Effects
        public void OnStateEffectCall(object source, GameEndsEventArgs args)
        {
            DoStateEffects();
            args.game_ends = GameEnds(out args.winner);
        }

        //Method for Handling Display Calls
        public void OnDisplayCall(object source, DisplayEventArgs args)
        {
            Display();
        }

        public void InitWave(int wave) {
            enemies = MonsterGenerator.MonsterWave(0);
            enemy_hand = new Hand();
            enemy_deck = new BattleDeck(enemies[0] != null ? enemies[0].GetDeck() : null,
                                        null,
                                        null);
            for (int i = 0; i < 5; i++)
            {
                Card c = enemy_deck.FromTop();
                enemy_hand.Put(c);
            }
        }

        //Gets the list of dead allies
        public Battler[] getDeadAllies(out int n_dead)
        {
            Battler[] dead = new Battler[3];
            int i = n_dead = 0;
            foreach (Battler b in allies)
            {
                if (!b.isAlive()) { dead[i] = b; n_dead++; }
                i++;
            }
            return dead;
        }

        //Apply the damage and effects of a card to its target.
        private void UseCard(Card c, int index) {
            if (c.IsAttack())
            {
                enemies[index].ApplyDamage(-c.GetValue());
                //Add the boost damage
                int user = c.OwnerID() - 1;
                List<State> states = allies[user].GetStates();
                if (states != null ) {
                    for (int i=0; i< states.Count; i++) {
                        if (states[i].GetName() == StateName.Boost )
                            enemies[index].ApplyDamage(-states[i].Damage());
                    }
                } 
                if (c.GetInflicts() != StateName.None)
                    enemies[index].InflictState(c.GetInflicts(), c.GetInflictValue(), c.GetInflictDuration());
            }
            else
            {
                //put here the calls for support cards
                allies[index].ApplyDamage(c.GetValue());
                if (c.GetInflicts() != StateName.None)
                    enemies[index].InflictState(c.GetInflicts(), c.GetInflictValue(), c.GetInflictDuration());
                //Test is there are valid targets
            }
        }

        //Due special cards like Revive, Taunt, Aim, etc.
        private bool PlaySpecialCard(Card c)
        {
            //put here the calls for support cards that don't target
            if (c.Name().Equals("Taunt") || c.Name().Equals("Adrenaline") || c.Name().Equals("Guard"))
            {
                allies[0].InflictState(c.GetInflicts(), c.GetInflictValue(), c.GetInflictDuration());
                Console.WriteLine("----------------------------------------------------------------------------------\n");
                Console.WriteLine("Warrior uses {0}", c.Name());
            }
            else if (c.Name().Equals("Aim") || c.Name().Equals("Hide"))
            {
                allies[1].InflictState(c.GetInflicts(), c.GetInflictValue(), c.GetInflictDuration());
                Console.WriteLine("----------------------------------------------------------------------------------\n");
                Console.WriteLine("Huntress uses {0}", c.Name());
            }
            else if (c.Name().Equals("Medicine") )
            {
                Console.WriteLine("\nPick a Target: 1-Warrior, 2-Huntress, 3-Magus");
                //Read target option (number-1);
                int index = Convert.ToInt32(Console.ReadLine()) - 1;
                if (index < 0 || index > 3)
                {
                    Console.WriteLine("Invalid Target, Try Again.");
                    return false;
                }
                allies[index].RemoveHarmfullStates();
                UseCard(c, index);
                Console.WriteLine("----------------------------------------------------------------------------------\n");
                Console.WriteLine("Huntress uses {0}", c.Name());
            }
            else if (c.Name().Equals("Cleanse"))
            {
                for (int i=0; i<allies.Length; i++) allies[i].RemoveHarmfullStates();
                Console.WriteLine("----------------------------------------------------------------------------------\n");
                Console.WriteLine("Magus uses {0}", c.Name());
            }
            else if (c.Name().Equals("Inspire"))
            {
                for (int i=0; i< allies.Length; i++) allies[i].InflictState(c.GetInflicts(), c.GetInflictValue(), c.GetInflictDuration());
                Console.WriteLine("----------------------------------------------------------------------------------\n");
                Console.WriteLine("Warrior uses {0}", c.Name());
            }
            //Put here the calls that require targets
            else if (c.Name().Equals("Revive") || c.Name().Equals("Rally"))
            {
                //pick allies that are dead if none exist return invalid
                int n_dead = 0;
                int index = 0;
                Battler[] dead_b = getDeadAllies(out n_dead);
                if (n_dead == 0)
                {
                    Console.WriteLine("No Valid Targets, Try Again.");
                    return false;
                }
                else
                {
                    for (int i = 0; i < n_dead; i++)
                    {
                        if (dead_b != null) Console.Write("{0}-{1} ", dead_b[i].GetID(), dead_b[i].GetName());
                    }
                    Console.WriteLine();
                    index = Convert.ToInt32(Console.ReadLine()) - 1;
                    if (index <= 0 || index > allies.Length)
                    {
                        Console.WriteLine("Invalid Target, Try Again.");
                        return false;
                    }
                    allies[index - 1].Set_Alive(true); //Revive the man before applying damage/heal
                }
                UseCard(c, index);
                Console.WriteLine("----------------------------------------------------------------------------------\n");
                if (c.OwnerID() == 1) Console.WriteLine("Warrior uses {0}", c.Name());
                else if (c.OwnerID() == 2) Console.WriteLine("Huntress uses {0}", c.Name());
                else Console.WriteLine("Magus uses {0}", c.Name());
            }
            return true;
        }

        // Returns if the Card played was valid or not NOTE: MIssing the STUN Clauses and how it affects playing cards
        public bool PlayCard(int card_number)
        {
            //put the code here for the playing and calculate damage, apply states, etc.
            Card c = player_hand.GetCard(card_number);
            if (c.IsAttack())
            {
                //assing the target and use
                UseCard(c, 0);
                Console.WriteLine("----------------------------------------------------------------------------------");
                if (c.OwnerID() == 1) Console.WriteLine("Warrior uses {0}", c.Name());
                else if (c.OwnerID() == 2) Console.WriteLine("Huntress uses {0}", c.Name());
                else Console.WriteLine("Magus uses {0}", c.Name());
            }
            else //Due the support ones
            {
                if (c.Name().Equals("Taunt") || c.Name().Equals("Adrenaline") || c.Name().Equals("Guard") || c.Name().Equals("Aim") || c.Name().Equals("Hide") || c.Name().Equals("Revive") || c.Name().Equals("Rally") || c.Name().Equals("Inspire"))
                {
                    if (!PlaySpecialCard(c))
                        return false;
                }
                else
                {
                    Console.WriteLine("\nPick a Target: 1-Warrior, 2-Huntress, 3-Magus");
                    //Read card option (number-1);
                    int index = Convert.ToInt32(Console.ReadLine())-1;
                    if (index < 0 || index > 3 )
                    {
                        Console.WriteLine("Invalid Target, Try Again.");
                        return false;
                    }
                    UseCard(c, index);
                    Console.WriteLine("----------------------------------------------------------------------------------");
                    if (c.OwnerID() == 1) Console.WriteLine("Warrior uses {0}", c.Name());
                    else if (c.OwnerID() == 2) Console.WriteLine("Huntress uses {0}", c.Name());
                    else Console.WriteLine("Magus uses {0}", c.Name());
                }
            }

            //draw new card from and place old card on bootom
            c = player_hand.Take(card_number);
            player_hand.Put(player_deck.FromTop());
            player_deck.ToBottom(c);

            return true;
        }

        //put here the logic of the enemy turn
        public void EnemyTurn(int card_number) {
            if (!enemies[0].HasState(StateName.Stun)) { 

                Card c = enemy_hand.GetCard(card_number);
                int target = _r.Next(3); //Assign random target
                //Test if target is valid 
                if (allies[0].HasState(StateName.Taunt)) { target = 0; }
                if (allies[1].HasState(StateName.Hidden) && target == 1) {
                    while (target == 1) target = _r.Next(3);
                }

                allies[target].ApplyDamage(-c.GetValue());
                if (c.GetInflicts() != StateName.None) allies[target].InflictState(c.GetInflicts(), c.GetInflictValue(), c.GetInflictDuration());

                Console.WriteLine("{0} uses {1}", enemies[0].GetName(), c.Name());

                //draw new card from and place old card on bootom
                c = enemy_hand.Take(card_number);
                enemy_hand.Put(enemy_deck.FromTop());
                enemy_deck.ToBottom(c);
            }
        }

        //Remove cards form hand, shuffle deck and draw new hand. (Enable reshufle for enemies?)
        public void Reshuffle()
        {
            player_hand.Clear();
            player_deck.Shuffle();
            for (int i = 0; i < 5; i++)
            {
                Card c = player_deck.FromTop();
                player_hand.Put(c);
            }
        }

        //Due the damage from States and Reduce the durations
        public void DoStateEffects()
        {
            //ALLIES
            for (int i=0; i< allies.Length; i++) {
                List<State> states = allies[i].GetStates();
                for (int j = 0; j < states.Count; j++) {
                    if (states[j] != null) {
                        if (states[j].GetName() == StateName.Burn || states[j].GetName() == StateName.Poison || states[j].GetName() == StateName.Bleed)
                        {
                            allies[i].ApplyDamage(-states[j].GetValue());
                            Console.WriteLine("{0} took {1} points of {2} damage.", allies[i].GetName(), states[j].GetValue(), states[j].GetName());
                        }

                        if (states[j].GetName() == StateName.Restore)
                        {
                            allies[i].ApplyDamage(states[j].GetValue());
                            Console.WriteLine("{0} restored {1} hit points.", allies[i].GetName(), states[j].GetValue());
                        }
                        allies[i].ApplyDamage(states[j].GetValue());
                    }
                }
                allies[i].CountDownStates();
            }
            //ENEMIES
            for (int i = 0; i < enemies.Length; i++)
            {
                List<State> states = enemies[i].GetStates();
                for (int j = 0; j < states.Count; j++)
                {
                    if (states[j] != null)
                    {
                        if (states[j].GetName() == StateName.Burn || states[j].GetName() == StateName.Poison || states[j].GetName() == StateName.Bleed)
                        {
                            enemies[i].ApplyDamage(-states[j].GetValue());
                            Console.WriteLine("{0} took {1} points of {2} damage.", enemies[i].GetName(), states[j].GetValue(), states[j].GetName());
                        }
                        if (states[j].GetName() == StateName.Restore)
                        {
                            enemies[i].ApplyDamage(states[j].GetValue());
                            Console.WriteLine("{0} restored {1} hit points.", enemies[i].GetName(), states[j].GetValue());
                        }
                    }
                }
                enemies[i].CountDownStates();
            }
        }

        //Checks if the game is over, second output indicates the winner (true-player wins, false-player loses)
        public bool GameEnds(out bool winner)
        {
            winner = false;
            int count = 0;
            //Test to see if heroes are dead
            for (int i = 0; i < allies.Length; i++)
            {
                if (!allies[i].isAlive()) count++;
            }
            if (count == allies.Length)
            {
                winner = false;
                return true;
            }

            //Test to see if Monsters are dead
            count = 0;
            for (int i=0; i < enemies.Length; i++) {
                if (!enemies[i].isAlive()) count++;
            }
            if (count == enemies.Length) {
                winner = true;
                return true;
            }
            return false;
        }

        public Hand GetHand() { return player_hand; }

        public void Display() {
           for (int i=0; i < enemies.Length; i++) {
                if (enemies[i] != null) enemies[i].Display();
           }
           Console.WriteLine();
           foreach (Battler b in allies) { b.Display(); }
           Console.WriteLine();
           player_hand.Display();
        }

        public void HandDisplay()
        {
            Console.WriteLine();
            player_hand.Display();
        }

        public void FullLog()
        {
            foreach (Battler b in allies) { b.Display(); }
            player_hand.Display();
            Console.WriteLine("-------------------------------------------------------------------");
            player_deck.Display();
            Console.WriteLine("-------------------------------------------------------------------");

            for (int i = 0; i < enemies.Length; i++) {
                if (enemies[i] != null) enemies[i].Display();
            }
            enemy_hand.Display();
            Console.WriteLine("-------------------------------------------------------------------");
            enemy_deck.Display();
        }

    }
}
