using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AV.Set.Infrustructure.Services;
using Microsoft.Practices.Unity;

namespace AV.Set.CoreServices
{
    public class Module
    {
        private IUnityContainer _container;

        public Module(IUnityContainer container)
        {
            _container = container;
        }

        public void Register()
        {
            _container.RegisterType<ISetGameAppService, SetGameApp>();
        }
    }
}
