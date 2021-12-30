using System.Collections.Generic;
using System.IO;
using System.Web;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace security_lighting_microsite.models
{
    public class Fixture
    {
        public string id { get; set; }
        public string MetaDescription { get; set; }
        public string OpCoCode { get; set; }
        public string Type { get; set; }
        public string[] Features { get; set; }
        public string Summary { get; set; }
       //public string Specsheet { get; set; }
        public string[] Applications { get; set; }
        public Variation[] Variations { get; set; }
        public SpecSheet[] SpecSheets { get; set; }
        public bool HasLedBulbs
        {
            get
            {
                if (String.IsNullOrEmpty(OpCoCode))
                    return Variations.Any(v => v.Bulb == "LED");

                return Variations.Where(v => v.Availability.Contains(OpCoCode)).Any(v => v.Bulb == "LED");
            }
        }

        public string[] ModifiedFeatures
        {
            get
            {
                var newFeatures = Features.ToList();
                if (HasLedBulbs)
                {
                    newFeatures.Add("Available in Bright White Light Emitting Diode (LED)");
                    /* OLD 
                    if (this.Type == "Shoebox")
                    {
                        //newFeatures.Remove("Available in Bright White Light (Metal Halide)");
                        newFeatures.Add("Available in Bright White Light Emitting Diode (LED)");
                    }
                    else
                    {
                        newFeatures.Add("Available in Light Emitting Diode (LED)");
                    }
                    */
                }
                if (!String.IsNullOrEmpty(OpCoCode) && this.Type == "Shoebox")
                {

                    var multipleWattageLocations = new List<string>() { "ETI" };
                    if (multipleWattageLocations.Contains(OpCoCode))
                        newFeatures.Add("Available in Various Wattage Sizes");

                }
                if (!String.IsNullOrEmpty(OpCoCode) && this.Type == "Acorn")
                {
                    var multipleWattageLocations = new List<string>() { "EAI" };
                    if (multipleWattageLocations.Contains(OpCoCode))
                        newFeatures.Add("Available in Various Wattage Sizes");
                }
                if (!String.IsNullOrEmpty(OpCoCode) && this.Type == "Open Bottom")
                {
                    var multipleWattageLocations = new List<string>() { "ETI", "EAI" };
                    if (multipleWattageLocations.Contains(OpCoCode))
                        newFeatures.Add("Available in Various Wattage Sizes");
                }

                return newFeatures.OrderBy(s => s).ToArray();
            }
        }

        public class Variation
        {
            public string Bulb { get; set; }
            public string Fixture { get; set; }
            public Illumination Illumination { get; set; }
            public List<string> Availability { get; set; }
            public List<string> Zoning { get; set; }
        }

        public class SpecSheet
        {
            public string Filename { get; set; }
            public string Description { get; set; }
            public List<string> Availability { get; set; }
        }

        public class Illumination
        {
            public int? Diameter { get; set; }
            public int? Width { get; set; }
            public int? Depth { get; set; }
        }

        public static OpCoFixtures GetFixtures(string OpCoCode = null)
        {
            var json = File.ReadAllText(HttpContext.Current.Server.MapPath("~/app_data/fixtures.json"));

            List<Fixture> fixtures = JsonConvert.DeserializeObject<List<Fixture>>(json);

            var retVal = new OpCoFixtures();
            retVal.OpCoCode = OpCoCode;
            retVal.Fixtures = fixtures;
            retVal.HasLEDs = false;

            retVal.Fixtures.ForEach(f => f.OpCoCode = OpCoCode);

            if (!string.IsNullOrWhiteSpace(OpCoCode))
            {
                retVal.Fixtures = fixtures.Where(f => f.Variations.Any(v => v.Availability.Contains(OpCoCode))).ToList();
                retVal.HasLEDs = fixtures.Any(f => f.Variations.Any(v => v.Availability.Contains(OpCoCode) && v.Fixture.Contains("LED")));
            }

            return retVal;
        }

        public static Fixture GetFixture(string OpCoCode, string FixtureType)
        {
            //if(FixtureType.Contains("directional-flood")){
            //    FixtureType = FixtureType.Replace('-', '/');
            //}
            //else{
            //    FixtureType = FixtureType.Replace('-', ' ');   
            //}

            return GetFixtures(OpCoCode).Fixtures.Find(f => String.Equals(f.id, FixtureType, StringComparison.InvariantCultureIgnoreCase));
        }
    }

}