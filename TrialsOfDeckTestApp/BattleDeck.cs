using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TODBasics;

namespace TODBattleSystem
{
    class BattleDeck {
        public static Random _r = new Random();

        //Variables
        private Card[] list_of_cards;
        private Card[] deck;
        private uint top_deck;
        private uint bottom_deck;

        //Methods
        public BattleDeck(PersonalDeck d1, PersonalDeck d2, PersonalDeck d3) {
            uint deck_size = 0;
            if (d1 != null) deck_size += 10;
            if (d2 != null) deck_size += 10;
            if (d3 != null) deck_size += 10;
            list_of_cards = new Card[deck_size];

            top_deck = 0;
            bottom_deck = 0;
            if (d1 != null) { d1.GetDeck().CopyTo(list_of_cards, bottom_deck); bottom_deck += 10; }
            if (d2 != null) { d2.GetDeck().CopyTo(list_of_cards, bottom_deck); bottom_deck += 10; }
            if (d3 != null) { d3.GetDeck().CopyTo(list_of_cards, bottom_deck); bottom_deck += 10; }
            bottom_deck--;
            //shuffle Deck
            Shuffle();
        }

        public void Shuffle() {
            deck = new Card[list_of_cards.Length];
            int i = 0;
            while (i < list_of_cards.Length) {
                int pos = _r.Next(list_of_cards.Length);
                if (deck[pos] != null) {
                    while (deck[pos] != null) {
                        pos++;
                        if (pos == list_of_cards.Length) pos = 0;
                    }
                }
                deck[pos] = list_of_cards[i];
                i++;
            }
        }

        //Take a card from the top of the deck, updates bottom of deck at the first card draw
        public Card FromTop() {
            Card c = deck[top_deck];
            deck[top_deck] = null;
            top_deck++;
            if (top_deck >= deck.Length) top_deck = 0;
            return c;
        }

        //Puts a card at the bottom of the deck
        public void ToBottom(Card c) {
            bottom_deck++;
            if (bottom_deck >= deck.Length) bottom_deck = 0;
            deck[bottom_deck] = c;
        }

        public void Display() {
            Console.WriteLine("Deck: ({0},{1})[", top_deck, bottom_deck );
            for (int i = 0; i < list_of_cards.Length; i++) {
                if (deck[i] != null)
                {
                    Console.Write("\t {0} ", i);
                    deck[i].Display();
                }
            }
            Console.WriteLine("]");
        }
    }
}
