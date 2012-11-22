using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using AV.Set.Infrustructure;
using AV.Set.Infrustructure.Services;
using AV.Set.Model;

namespace AV.Set.CoreServices
{
    class SetGameApp: ISetGameAppService
    {
        private IMvcApplication _application;

        public SetGameApp(IMvcApplication application)
        {
            _application = application;
        }

        public IEnumerable<User> GetUsers()
        {
            return _application.ActiveUsers;
        }

        public void AddUser(string userName)
        {
            _application.ActiveUsers.Add(new User() { Name = userName });
        }

        public void RemoveUser(string userName)
        {
            var user = _application.ActiveUsers.SingleOrDefault( u => u.Name == userName);
            if (user != null) _application.ActiveUsers.Remove(user);
        }

        public Game GetGame()
        {
            return _application.CurrentGame;
        }

        public void Reset()
        {
            _application.StartNewGame();
        }
    }
}
