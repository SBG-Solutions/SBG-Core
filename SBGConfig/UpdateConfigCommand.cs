using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBGCore
{
    class UpdateConfigCommand : BaseCommand
    {
        public const string NAME = "UpdateConfigCommand";

        public UpdateConfigCommand()
            : base(NAME) { }

        public override bool Execute(string input)
        {
            
            return true;
        }
    }
}
