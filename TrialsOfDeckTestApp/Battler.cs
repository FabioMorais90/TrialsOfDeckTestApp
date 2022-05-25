using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TODBasics;

namespace TODBattleSystem
{
    class Battler : Character
    {
        //Variables
        private int hit_points;
        private int max_hit_points;
        private string art;
        private bool is_alive;
        private List<State> states;

        //Methods
        public Battler(string new_name, byte new_id, int hp, string new_art) : base(new_name, new_id) {
            hit_points = max_hit_points = hp;
            art = new_art;
            is_alive = true;
            states = new List<State>();
        }

        public Battler(ref Character c, int hp, string new_art) : base(ref c) {
            hit_points = max_hit_points = hp;
            art = new_art;
            is_alive = true;
            states = new List<State>();
        }

        public int GetHP() { return hit_points; }

        public bool isAlive() { return is_alive; }

        public void SetHP(int hp) { hit_points = max_hit_points = hp; }

        public List<State> GetStates() { return states;}

        public State GetState(StateName n) {
            for (int i = 0; i < states.Count; i++) {
                if (states[i] != null && states[i].GetName() == n) {
                    return states[i];
                }
            }
            return null;
        }

        public int GetStateIndex(StateName n)
        {
            for (int i = 0; i < states.Count; i++)
            {
                if (states[i] != null && states[i].GetName() == n)
                {
                    return i ;
                }
            }
            return -1;
        }

        public bool HasState(StateName n) {
            for (int i = 0; i < states.Count; i++) {
                if (states[i] != null && states[i].GetName() == n) {
                    return true;
                }
            }
            return false;
        }

        public void CountDownStates()
        {
            for (int i = 0; i < states.Count; i++)
            {
                if (states[i] != null && states[i].GetName()!=StateName.Shield ) { states[i].Decrease(); }
            }

            //Remove all that have dur == 0
            states.RemoveAll(s => s.GetDuration() == 0);
        }

        //Remove Shields if they are bellow or equal to zero
        public void RemoveShield() {
            var itemToRemove = states.SingleOrDefault(s => s.GetName() == StateName.Shield && s.GetValue() <= 0);
            if (itemToRemove != null)
                states.Remove(itemToRemove);

        }
        public void RemoveHarmfullStates()
        {
            states.RemoveAll(s => s.GetName() == StateName.Bleed || s.GetName() == StateName.Burn || s.GetName() == StateName.Poison || s.GetName() == StateName.Stun);
        }

        public void ApplyDamage(int val) {
            if (is_alive)
            {
                if (HasState(StateName.Shield) && val < 0) {
                    //Damage the shield first them pass the rest to the character
                    int shield = GetState(StateName.Shield).GetValue() + val;
                    if (shield < 0) { hit_points += shield; }
                    int index = GetStateIndex(StateName.Shield);
                    states[index].Update(-1, shield);
                    RemoveShield();
                }
                else
                    hit_points += val;

                if (hit_points <= 0) { hit_points = 0; is_alive = false; }
                if (hit_points > max_hit_points) hit_points = max_hit_points;
            }
        }

        public void InflictState(StateName inf_name, int inf_val, int inf_dur)
        {
            //see if state exists
            for(int i= 0; i < states.Count; i++)
            {
                if (states[i].GetName() == inf_name) { 
                    states[i].Update(inf_dur, inf_val);
                    return;
                }
            }
            //If doenst exists, add it
            states.Add(new State(inf_name, inf_val, inf_dur));
        }

        public void Set_Alive(bool alive=true) { is_alive = alive; }

        public new void Display() {
            //base.Display();
            Console.WriteLine("{0}: {1}/{2} : {3};", GetName(), hit_points, max_hit_points, is_alive?"Alive":"Dead");
            for (int i = 0; i < states.Count; i++)
            {
                if (states[i] != null) Console.Write(" {0}; ", states[i].ToText());
            }
            Console.WriteLine();
        }

    }
}
