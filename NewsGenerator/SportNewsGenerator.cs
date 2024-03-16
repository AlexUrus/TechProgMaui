using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsGenerator
{
    public class SportNewsGenerator : NewsGenerator
    {
        protected override string Category { get => "sport" ;}
        protected override string FileName { get => "SportsNews.json"; }
    }
}
