using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsGenerator
{
    public class StorageNews : News
    {
        public override string ViewTitle { get => "News from Storage: " + Title;}
    }
}
