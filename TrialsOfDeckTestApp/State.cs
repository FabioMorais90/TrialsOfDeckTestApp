using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TODBasics {

    public enum StateName { None, Bleed, Burn, Poison, Stun, Shield, Boost, Hidden, Taunt, Restore };

    class State{
        //Variables
        private StateName name;
        private int value;
        private int duration; //-1 is infinite

        //Methods
        public State(StateName state, int dur) {
            name = state;
            value = 0;
            duration = dur;
        }
        public State(StateName state, int val, int dur){
            name = state;
            value = val;
            duration = dur;
        }

        public StateName GetName() { return name; }

        public int GetValue() { return value; }

        public int Damage() {
            if (name == StateName.Shield || name == StateName.Restore || name == StateName.Boost || name == StateName.Taunt || name == StateName.Hidden)
                return value;
            else
                return -value;
        }

        public int GetDuration() { return duration; }

        public void Increase() { duration++; }

        public void Decrease() { duration--; }


        public void Update(int dur, int val ) {
            value += val;
            if (duration < dur) duration = dur;
        }

        public String ToText()
        {
            string s = name + "(" + value + ") for " +  duration + " rounds";
            return s;
        }
    }
}
