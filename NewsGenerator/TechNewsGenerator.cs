using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsGenerator
{
    public class TechNewsGenerator : NewsGenerator
    {
        protected override string Category { get => "technology"; }
        protected override string FileName { get => "TechNews.json"; }
    }
}
