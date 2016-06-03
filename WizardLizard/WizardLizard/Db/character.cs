using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizardLizard.Db
{
    internal  class character : TableRow
    {
        public string name { get; set; }
        public int health { get; set; }
        public int Level { get; set; }
        public int PetID { get; set; }
        public int spellID { get; set; }

    }
}
