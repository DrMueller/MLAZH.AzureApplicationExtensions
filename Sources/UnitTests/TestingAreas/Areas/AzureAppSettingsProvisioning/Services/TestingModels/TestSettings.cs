using System.Collections.Generic;

namespace Mmu.Mlazh.AzureApplicationExtensions.UnitTests.TestingAreas.Areas.AzureAppSettingsProvisioning.Services.TestingModels
{
    public class TestSettings
    {
        public ComplexObject MyComplexObject { get; set; }
        public ComplexObject AnotherComplexObject { get; set; }
        public List<ComplexObject> ComplexObjects { get; set; }
        public int IntValue { get; set; }
        public List<string> Strings { get; set; }
        public string StringValue { get; set; }
    }
}