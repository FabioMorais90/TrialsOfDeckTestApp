using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TODBasics
{
    class Collection {
        //constants
        const int player_collection_size = 3*1*10; //nº of chars * nº of levels * nº of diferent cards

        //Variables
        private Card[] player_cards;

        //Methods
        public Card GetCard(int pos) {
            Card c = null;
            if (pos < player_collection_size)
                c = player_cards[pos];
            return c;
        }

        public void Display() {
            for (int i = 0; i < player_collection_size; i++) { 
                player_cards[i].Display();
                Console.WriteLine("------------------------------");
            }
        }

        public void Init() {
            player_cards = new Card[player_collection_size];
            
            //Warrior cards
            player_cards[0] = new Card("Strike", 1, 1, "picture", true, 50,  "Deal 50 damage to an enemy");
            player_cards[1] = new Card("Cleave", 1, 1, "picture", true, 40, "Deal 40 damage to all enemies");
            player_cards[2] = new Card("Slice", 1, 1, "picture", true, 30, StateName.Bleed, 20, 3, "Deal 30 damage to an enemy and inflicts 20 bleed damage for 3 rounds");
            player_cards[3] = new Card("Pummel", 1, 1, "picture", true, 30, StateName.Stun, 0, 2, "Deal 30 damage and Stun an enemy for 2 rounds");
            player_cards[4] = new Card("Bellow", 1, 1, "picture", true, 0, StateName.Stun, 0, 2, "Stuns all enemies for 2 rounds");
            player_cards[5] = new Card("Taunt", 1, 1, "picture", false, 0, StateName.Taunt,0, 5, "Taunts enemies attack the hero for 5 rounds");
            player_cards[6] = new Card("Adrenaline", 1, 1, "picture", false, 0, StateName.Restore, 30, 3, "Heal self 30 damage for 3 rounds");
            player_cards[7] = new Card("Guard", 1, 1, "picture", false, 0, StateName.Shield, 80, -1, "Shield 80 damage to self");
            player_cards[8] = new Card("Inspire", 1, 1, "picture", false, 0, StateName.Boost, 40, 3, "Boosts all allies damage by 40 for 3 rounds");
            player_cards[9] = new Card("Rally", 1, 1, "picture", false, 200, "Revives fallen ally with 200 hit points");
            
            //Huntress
            player_cards[10] = new Card("Shoot", 2, 1, "picture", true, 50, "Deal 50 damage to an enemy");
            player_cards[11] = new Card("Buckshot", 2, 1, "picture", true, 40, "Deal 40 damage to all enemies");
            player_cards[12] = new Card("Lacerate", 2, 1, "picture", true, 30, StateName.Bleed, 20, 3, "Deal 30 damage to an enemy and inflicts 20 bleed damage for 3 rounds");
            player_cards[13] = new Card("Envenom", 2, 1, "picture", true, 20, StateName.Poison, 20, 4, "Deal 20 damage to an enemy and inflicts 20 poison damage for 4 rounds");
            player_cards[14] = new Card("Molotov", 2, 1, "picture", true, 30, StateName.Burn, 30, 2, "Deal 30 damage to an enemy and inflicts 30 burn damage for 2 rounds");
            player_cards[15] = new Card("Flashbang", 2, 1, "picture", true, 0, StateName.Stun, 0, 2, "Stun an enemy for 2 rounds");
            player_cards[16] = new Card("Medicine", 2, 1, "picture", false, 0, StateName.Restore, 50, 3, "Removes harmfull states from an ally and heals 50 damage to them for 3 rounds");
            player_cards[17] = new Card("Concoction", 2, 1, "picture", false, 0, StateName.Boost, 50, 2, "Boost an ally's damage by 50 for 2 rounds");
            player_cards[18] = new Card("Aim", 2, 1, "picture", false, 0, StateName.Boost, 80, 2, "Boost character's attack damage by 80 for the next 2 rounds");
            player_cards[19] = new Card("Hide", 2, 1, "picture", false, 0, StateName.Hidden, 0, 3, "Enemies can't target the hero for 2 rounds");

            //Magus
            player_cards[20] = new Card("Bolt", 3, 1, "picture", true, 50, "Deal 50 damage to an enemy");
            player_cards[21] = new Card("Storm", 3, 1, "picture", true, 40, "Deal 40 damage to all enemies");
            player_cards[22] = new Card("Ignite", 3, 1, "picture", true, 30, StateName.Burn, 30, 3, "Deal 30 damage to an enemy and inflicts 30 burn damage for 2 rounds");
            player_cards[23] = new Card("Inferno", 3, 1, "picture", true, 30, StateName.Burn, 30, 3, "Deal 30 damage to all enemy and inflicts 30 burn damage for 2 rounds");
            player_cards[24] = new Card("Freeze", 3, 1, "picture", true, 20, StateName.Stun, 0 , 3, "Deal 20 damage and Stun an enemy for 3 rounds");
            player_cards[25] = new Card("Blizzard", 3, 1, "picture", true, 20, StateName.Stun, 0, 3, "Deal 20 damage and Stun all enemies for 3 rounds");
            player_cards[26] = new Card("Cleanse", 3, 1, "picture", false, 0, "Removes harmfull states from all allies.");
            player_cards[27] = new Card("Rejuvenate", 3, 1, "picture", false, 100, "Heal 100 damage to an ally");
            player_cards[28] = new Card("Revitalize", 3, 1, "picture", false, 80,  "Heal 80 damage to all allies");
            player_cards[29] = new Card("Revive", 3, 1, "picture", false, 200, "Revives fallen ally with 200 hit points");
        }
    }
}
