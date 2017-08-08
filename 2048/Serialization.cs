using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace _2048
{
    class Serialization
    {
        static BinaryFormatter bin = new BinaryFormatter();
        public static void Serialize()
        {
            bin.Serialize(Game.file, Game.game);
        }
        public static void Deserialize()
        {
            Game.game = bin.Deserialize(Game.file) as Field;
        }
    }
}
