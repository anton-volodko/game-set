using System.Collections.Generic;
using AV.Set.Model;

namespace AV.Set.Infrustructure
{
    public interface IMvcApplication
    {
        ICollection<User> ActiveUsers { get; }
        Game CurrentGame { get; }
        void StartNewGame();
    }
}
