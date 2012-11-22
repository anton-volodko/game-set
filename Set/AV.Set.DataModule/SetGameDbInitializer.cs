using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AV.Set.DataModule
{
    public class SetGameDbInitializer : DropCreateDatabaseIfModelChanges<SetGameContext>
    {
        protected override void Seed(SetGameContext context)
        {
            base.Seed(context);
        }
    }
}
