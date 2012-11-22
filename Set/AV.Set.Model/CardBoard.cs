using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AV.Set.Model
{
    /// <summary>
    /// Contains list of cards that are currently in play
    /// </summary>
    public class CardBoard
    {
        public CardBoard()
        {
            Cards = new Collection<Card>();    
        }

        public ICollection<Card> Cards { get; private set; }
    }
}
