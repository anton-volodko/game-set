using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AV.Set.Model;

namespace AV.Set.Infrustructure.Services
{
    public interface ISetGameAppService
    {
        IEnumerable<User> GetUsers();

        void AddUser(string userName);

        void RemoveUser(string userName);

        Game GetGame();

        void Reset();
    }
}
