using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TODBasics {
    class Card {
        //Variables
        private string name;
        private byte owner_id;//1-Warrior, 2-Magus, 3-Huntress, others monsters
        private byte level; 
        private string art;
        private string text;
        private bool type; //true-attack, false-support
        private int value;
        private StateName inflicts;
        private int inflict_value;
        private int inflict_duration;
        

        //Methods
        public Card(string new_name, byte owner, byte lv, string new_art, bool is_attack, int val, string new_text) {
            name = new_name;
            owner_id = owner;
            level = lv;
            art = new_art;
            text = new_text;
            type = is_attack;
            value = val;
            inflicts = StateName.None;
            inflict_value = 0;
            inflict_duration = 0;
        }

        public Card(string new_name, byte owner, byte lv, string new_art, bool is_attack, int val,  StateName inf, int inf_val, int inf_dur, string new_text)
        {
            name = new_name;
            owner_id = owner;
            level = lv;
            art = new_art;
            text = new_text;
            type = is_attack;
            value = val;
            inflicts = inf;
            inflict_value = inf_val;
            inflict_duration = inf_dur;
        }

        public string Name() { return name; }

        public byte OwnerID() { return owner_id; }

        public byte Level() { return level; }

        public bool IsAttack() { return type; }

        public int GetValue() { return value; }

        public StateName GetInflicts() { return inflicts; }

        public int GetInflictValue() { return inflict_value; }

        public int GetInflictDuration() { return inflict_duration; }

        public void Description() {
            Console.WriteLine("Name: {0}, Owner: {1}, Lv: {2}, Attack: {3}, Damage: {4}, Text: {5}.", name, owner_id, level, type, value, text);
        }

        public void Display() {
            Console.WriteLine("Name: {0}, Lv: {1}, Text: {2}.", name, level, text);
        }
    }

}
