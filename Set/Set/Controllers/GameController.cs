using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AV.Set.Infrustructure.Services;
using AV.Set.Model;
using AV.Web.Set.Binders;
using AV.Web.Set.Models;

namespace AV.Web.Set.Controllers
{
    [Authorize]
    public class GameController : Controller
    {
        private ISetGameAppService _app;

        public GameController(ISetGameAppService app)
        {
            _app = app;
        }

        /// <summary>
        /// Page where user should join any active game
        /// </summary>
        /// <returns></returns>
        public ActionResult Join()
        {
            _app.GetGame().Players.Add(CreatePlayer());
            return RedirectToAction("Waiting");
        }

        private Player CreatePlayer()
        {
            var currentUser = _app.GetUsers().Single(u => u.Name == HttpContext.User.Identity.Name);
            return new Player() 
            { 
                User = currentUser,
                State = PlayerState.Active,                
            };
        }

        public ActionResult Waiting()
        {
            var playersAtTheGame = from user in _app.GetUsers() orderby user.Name select user.Name;
            return View(playersAtTheGame.ToArray());
        }

        public ActionResult Play()
        {
            var game = _app.GetGame();
            game.State = GameState.BoardScanning;
            return View(game);
        }

        public JsonResult GetGameDetails()
        {
            var game =_app.GetGame();
            var stack = game.CardStack;            
            return Json(new { cardsLeft = stack.Count } );
        }

        public JsonResult GetPlayingCards()
        {
            var game = _app.GetGame();
            var board = game.CardBoard;
            var cards = from card in board.Cards
                        select TemplatedCardBinder.ToViewModel(card);
            return Json(cards);
        }       

        public void AddCards()
        {
            var game = _app.GetGame();
            var stack = game.CardStack;

            // The largest group of cards you can put together without creating a set is 20
            if (game.CardBoard.Cards.Count < 20)
            {
                // add 3 cards
                for (int cardIndex = 0; cardIndex < 3; cardIndex++)
                {
                    if (stack.Count != 0)
                    {
                        var card = stack.Pop();
                        game.CardBoard.Cards.Add(card);
                    }
                }

                // reset players with falls
                foreach (var player in game.Players.Where(p => p.State == PlayerState.HavingFall))
                {
                    player.State = PlayerState.Active;
                }
            }
        }

        public void RaiseHand()
        {
            var game = _app.GetGame();
            game.State = GameState.SetSelection;
        }

        public ActionResult CheckSet(Card[] set)
        {
            var colorsCorrect = IsCorrect(set.Select( c => c.Color));
            var fillingsCorrect = IsCorrect(set.Select( c => c.Filling));
            var shapesCorrect = IsCorrect(set.Select( c => c.Shape));
            var countCorrect = IsCorrect(set.Select( c => c.ShapeCount));
            var isCorrect = colorsCorrect && fillingsCorrect && shapesCorrect && countCorrect;
            var game = _app.GetGame();
            if (isCorrect)
            {
                foreach (var card in set) game.CardBoard.Cards.Remove(card);
                if (game.CardBoard.Cards.Count < 9) AddCards();                
            }
            else
            {
                var currentPlayer = game.Players.Single(
                    player => 
                        player.User.Name == HttpContext.User.Identity.Name);
                currentPlayer.State = PlayerState.HavingFall;
            }
            return Json(isCorrect);
        }

        public bool IsCorrect<TValue>(IEnumerable<TValue> valuesToCheck) where TValue: struct
        {
            var firstVal = valuesToCheck.First();
            var theSame = !valuesToCheck.Any(val => !val.Equals(firstVal));
            if (theSame) return true;

            for (int valIndex = 0; valIndex < valuesToCheck.Count() - 1; valIndex++ )
            {
                var valToCheck = valuesToCheck.Skip(valIndex).Take(1).First();
                var failed = valuesToCheck.Skip(valIndex + 1).Any(val => val.Equals(valToCheck));
                if (failed) return false;
            }

            return true;
        }

        public ActionResult Reset()
        {
            _app.Reset();
            _app.GetGame().Players.Add(CreatePlayer());
            return RedirectToAction("Play");
        }
    }
}
