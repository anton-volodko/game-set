using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AV.Set.Infrustructure.Services;
using AV.Set.Model;

namespace AV.Web.Set.Controllers
{
    public class PlayersController : ApiController
    {
        private readonly ISetGameAppService _appService;

        public PlayersController(ISetGameAppService appService)
        {
            _appService = appService;
        }

        // GET api/players
        public IEnumerable<Player> Get()
        {
            return _appService.GetGame().Players;
        }

        // GET api/players/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/players
        public void Post(string value)
        {
        }

        // PUT api/players/5
        public void Put(int id, string value)
        {
        }

        // DELETE api/players/5
        public void Delete(int id)
        {
        }
    }
}
