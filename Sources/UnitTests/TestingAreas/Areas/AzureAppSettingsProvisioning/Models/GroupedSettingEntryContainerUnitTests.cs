using Mmu.Mlazh.AzureApplicationExtensions.Areas.AzureAppSettingsProvisioning.Models;
using Mmu.Mlh.TestingExtensions.Areas.ConstructorTesting.Services;
using NUnit.Framework;

namespace Mmu.Mlazh.AzureApplicationExtensions.UnitTests.TestingAreas.Areas.AzureAppSettingsProvisioning.Models
{
    [TestFixture]
    public class GroupedSettingEntryContainerUnitTests
    {
        [Test]
        public void Constructor_Works()
        {
            ConstructorTestBuilderFactory.Constructing<GroupedSettingEntryContainer>().UsingDefaultConstructor();
        }
    }
}