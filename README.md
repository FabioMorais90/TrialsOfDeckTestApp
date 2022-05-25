# TrialsOfDeckTestApp
A prototype of a RPG battle system using card game logic as inspiration

Many Turn-Based RPG like Final Fantasy or Dragon Quest have a combat system where the characters and the enemies take turns by selecting their actions on a menu, like attack, cast magic, among other options.
This combat system was developed to remove the navigation of menus with an easy to use card game to resolve combat. Using cards to represent abilities that your heroes can use, you can easily access your combat options and by adding the drawing from a deck, adding a sensation of on-the-fly thinking in combat. 
So each character is given 10 cards to them for a fight (there can be multiples of any card), and these are called “Personal Decks”. So in this scenario, The Warrior, Huntress and Magus will combine their Personal Decks and are then shuffled together to form the party’s Battle Deck. The monsters also create their Battle Deck in the same way (their decks can range from 10 to 30 depending on the number of monsters).
Each group then draws 5 cards from the deck and proceed to use cards and drawing more until one side doesn’t have characters alive to fight.
Being a card game, two situations can occur as you play:
  1. The fight takes too long that a Battle Deck is empty.
  2. The card you hold on your hand cannot be used (the owner can’t use their ability or no valid targets).
To address the first issue, I implemented a method to put back on the bottom of the Deck each card that was played previously, making sure that the Battle Deck is never empty. The second issue is solved by implementing a Magic the Gathering mechanic called “Wheeling” by the community, where a player discards their hand and draws a new set of cards from the deck. 
If a player can't use or doesn’t want to use the cards in their hand, he can shuffle thier hand with the Battle Deck and draw a new hand. 
However there are cases where a character is not alive, so drawing cards for that character is a waste of resources. To correct this, every time a character is not alive, the cards that he owns are removed from the BattleDeck and reshuffled. If the character is revived the cards are added back in and reshuffled.
