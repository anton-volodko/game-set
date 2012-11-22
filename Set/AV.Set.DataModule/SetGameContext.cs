using System.Data.Entity;
using AV.Set.Model;

namespace AV.Set.DataModule
{
    public class SetGameContext: DbContext
    {
        public SetGameContext(): base("GameSetContext")
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<CardStack> CardStacks { get; set; }
        public DbSet<CardSet> CardSets { get; set; }
        public DbSet<CardBoard> CardBoards { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
