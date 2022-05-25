using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TODBasics {
    class PersonalDeck {
        //Variables
        private Card[] deck; //It initializes at null
        private uint available_spot;
        private byte owner_id;

        //Methods
        public PersonalDeck(byte owner){
            deck = new Card[10];
            available_spot = 0;
            owner_id = owner;
        }

        public void AddCard(Card c) {
            deck[available_spot] = c;
            available_spot++;
            if (available_spot >= 10) available_spot = 0;
        }

        public void AddCard(Card c, uint pos){
            deck[pos] = c;
        }

        public Card[] GetDeck() {
            return deck;
        }

        public void Display(){
            foreach (Card c in deck) {
                if (c != null) {
                    Console.Write("\t");
                    c.Display();
                    //c.Description();
                }
            }
        }
    }
}
