using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsGenerator
{
    public class ApiNews : News
    {
        public override string ViewTitle { get => "News from Api: " + Title; }
    }
}
