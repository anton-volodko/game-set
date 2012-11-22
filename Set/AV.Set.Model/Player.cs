using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AV.Set.Model
{
    /// <summary>
    /// Represent an user in the current game.
    /// </summary>
    public class Player
    {
        public Player()
        {
            FoundSets = new Collection<CardSet>();
        }

        public User User { get; set; }
        public PlayerState State { get; set; }
        public ICollection<CardSet> FoundSets { get; private set; }
    }
}
