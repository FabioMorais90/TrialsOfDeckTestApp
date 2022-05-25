using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TODBasics;

namespace TODBattleSystem {
    class Hand {
        //Variables
        private Card[] cards;
        private byte last_available_pos;
        
        //Methods
        public Hand(){
            cards = new Card[5];
            last_available_pos = 0;
        }

        public Card GetCard(int pos) {
            return cards[pos];
        } 

        public void Put(Card c) {
            cards[last_available_pos] = c;
            if (last_available_pos < 4) last_available_pos++;
        }

        public Card Take(int pos){
            Card c = cards[pos];
            cards[pos] = null;
            Arrange(pos);
            return c;
        }

        public void Arrange(int pos) {
            for (int i = 0; i < 5; i++) {
                if (i <= pos) continue;
                cards[i-1] = cards[i];
                cards[i] = null;
            }

        }

        public void RemoveCards(byte id) {
            for (int i=0; i < 5; i++) {
                if ((cards[i] != null) & (cards[i].OwnerID() == id)) {
                    cards[i] = null;
                    Arrange(i);
                }
            }
        }

        public void Clear()
        {
            for(int i=0; i<5; i++) cards[i] = null;
            last_available_pos = 0;
        }

        public void Display() {
            Console.WriteLine("Hand: ");
            byte i = 0;
            foreach (Card c in cards) {
                if (c != null) {
                    Console.Write("Card {0} - ", (i+1));
                    c.Display();
                }
                i++;
            }
        }

    }
}
