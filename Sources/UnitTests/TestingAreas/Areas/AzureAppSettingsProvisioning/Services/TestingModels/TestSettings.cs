using System.Collections.Generic;

namespace Mmu.Mlazh.AzureApplicationExtensions.UnitTests.TestingAreas.Areas.AzureAppSettingsProvisioning.Services.TestingModels
{
    public class TestSettings
    {
        public ComplexObject AnotherComplexObject { get; set; }
        public List<ComplexObject> ComplexObjects { get; set; }
        public int IntValue { get; set; }
        public IReadOnlyCollection<ComplexObject> MoreComplexObjects { get; set; }
        public ComplexObject MyComplexObject { get; set; }
        public IReadOnlyCollection<string> Strings { get; set; }
        public string StringValue { get; set; }
    }
}