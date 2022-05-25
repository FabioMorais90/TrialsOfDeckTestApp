using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TODBasics;

namespace TODGenerators {

    class DeckGenerator {
        static public PersonalDeck NewDeck(ref Character owner, ref Collection col)
        {
            PersonalDeck pd = new PersonalDeck(owner.GetID());
            switch (owner.GetID())
            {
                case 1:
                    pd = WarriorDeck(1, ref col);
                    break;
                case 2:
                    pd = HuntressDeck(2, ref col);
                    break;
                case 3:
                    pd = MagusDeck(3, ref col);
                    break;
            }
            return pd;
        }

        static public PersonalDeck WarriorDeck(byte id, ref Collection col)
        {
            PersonalDeck pd = new PersonalDeck(id);
            pd.AddCard(col.GetCard(0), 0);//strike
            pd.AddCard(col.GetCard(0), 1);//strike
            pd.AddCard(col.GetCard(0), 2);//strike
            pd.AddCard(col.GetCard(2), 3);//slice
            pd.AddCard(col.GetCard(3), 4);//pummel
            pd.AddCard(col.GetCard(3), 5);//pummel
            pd.AddCard(col.GetCard(5), 6);//taunt
            pd.AddCard(col.GetCard(6), 7);//adrenaline
            pd.AddCard(col.GetCard(8), 8);//inspire
            pd.AddCard(col.GetCard(9), 9);//rally
            return pd;
        }


        static public PersonalDeck HuntressDeck(byte id, ref Collection col)
        {
            PersonalDeck pd = new PersonalDeck(id);
            pd.AddCard(col.GetCard(10), 0);//shoot
            pd.AddCard(col.GetCard(10), 1);//shoot
            pd.AddCard(col.GetCard(12), 2);//lacerate
            pd.AddCard(col.GetCard(12), 3);//lacerate
            pd.AddCard(col.GetCard(13), 4);//envenom
            pd.AddCard(col.GetCard(13), 5);//envenom
            pd.AddCard(col.GetCard(14), 6);//molotov
            pd.AddCard(col.GetCard(14), 7);//aim
            pd.AddCard(col.GetCard(15), 8);//flashbang
            pd.AddCard(col.GetCard(18), 9);//aim
            return pd;
        }


        static public PersonalDeck MagusDeck(byte id, ref Collection col)
        {
            PersonalDeck pd = new PersonalDeck(id);
            pd.AddCard(col.GetCard(20), 0);//bolt
            pd.AddCard(col.GetCard(20), 1);//bolt
            pd.AddCard(col.GetCard(22), 2);//ignite
            pd.AddCard(col.GetCard(22), 3);//ignite
            pd.AddCard(col.GetCard(24), 4);//freeze
            pd.AddCard(col.GetCard(26), 5);//cleanse
            pd.AddCard(col.GetCard(27), 6);//rejuvenate
            pd.AddCard(col.GetCard(27), 7);//rejuvenate
            pd.AddCard(col.GetCard(28), 8);//revitalize
            pd.AddCard(col.GetCard(29), 9);//revive
            return pd;
        }
    }
}
