using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace security_lighting_microsite.models
{
    public class OpCoFixtures
    {
        public OpCoFixtures()
        {
            Fixtures = new List<Fixture>();
        }

        public string OpCoCode { get; set; }        
        public List<Fixture> Fixtures { get; set; }
        public bool HasLEDs { get; set; }
    }
}