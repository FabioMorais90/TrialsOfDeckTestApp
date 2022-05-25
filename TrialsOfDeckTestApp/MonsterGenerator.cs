using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TODBasics;
using TODBattleSystem;

namespace TODGenerators
{
    class MonsterGenerator {
        static public Battler[] MonsterWave(int wave) {
            Battler[] b = new Battler[1];
            switch (wave)
            {
                default:
                    b[0] = GraveBlob(500, 4);
                    break;
            }
            return b;
        }

        static public Battler GraveBlob(int life, byte owner)
        {
            Battler b = new Battler("Grave Blob", owner, 500, "Grave Blob");
            PersonalDeck pd = new PersonalDeck(owner);
            pd.AddCard(new Card("Bite", owner, 1, "picture1", true, 30, "Deal 30 damage to a target"));//bite
            pd.AddCard(new Card("Bite", owner, 1, "picture1", true, 30, "Deal 30 damage to a target"));//bite
            pd.AddCard(new Card("Bite", owner, 1, "picture1", true, 30, "Deal 30 damage to a target"));//bite
            pd.AddCard(new Card("Bite", owner, 1, "picture1", true, 30, "Deal 30 damage to a target"));//bite
            pd.AddCard(new Card("Bite", owner, 1, "picture1", true, 30, "Deal 30 damage to a target"));//bite
            pd.AddCard(new Card("Bite", owner, 1, "picture1", true, 30, "Deal 30 damage to a target"));//bite
            pd.AddCard(new Card("Bite", owner, 1, "picture1", true, 30, "Deal 30 damage to a target"));//bite
            pd.AddCard(new Card("Acid", owner, 1, "picture1", true, 30, StateName.Poison, 20, 4, "Deal 30 damage to a target and inflicts 20 poison damage for 4 rounds"));//Acid
            pd.AddCard(new Card("Acid", owner, 1, "picture1", true, 30, StateName.Poison, 20, 4, "Deal 30 damage to a target and inflicts 20 poison damage for 4 rounds"));//Acid
            pd.AddCard(new Card("Acid", owner, 1, "picture1", true, 30, StateName.Poison, 20, 4, "Deal 30 damage to a target and inflicts 20 poison damage for 4 rounds"));//Acid
            b.SetDeck(pd);
            return b;
        }

        
    }
}
