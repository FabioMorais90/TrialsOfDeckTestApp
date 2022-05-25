using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TODBasics {
    class Character {
        //Variables
        private string name;
        private byte char_id; //1-Warrior, 2-Magus, 3-Huntress, others monsters
        private PersonalDeck deck;

        //Methods
        public Character(string new_name, byte new_id) {
            name = new_name;
            char_id = new_id;
            deck = null;
        }

        public Character(ref Character c) {
            name = c.name;
            char_id = c.char_id;
            deck = c.deck;
        }
        
        public string GetName() { return name; }

        public byte GetID() { return char_id; }

        public PersonalDeck GetDeck() { return deck; }

        public void SetDeck(PersonalDeck pd){ deck = pd; }

        public void Display() {
            Console.WriteLine("Name: {0}, Type: {1} ", name, char_id);
            Console.WriteLine("Personal Deck :[");
            if (deck != null)
                  deck.Display();
            Console.WriteLine("];");
        }
    }
}
