using Mmu.Mlazh.AzureApplicationExtensions.Areas.ApplicationInsights.Services.Implementation;
using Mmu.Mlazh.AzureApplicationExtensions.Areas.ApplicationInsights.Services.Servants;
using Moq;
using NUnit.Framework;

namespace Mmu.Mlazh.AzureApplicationExtensions.UnitTests.TestingAreas.Areas.ApplicationInsights.Services
{
    [TestFixture]
    public class TelemetryClientProxyUnitTests
    {
        [Test]
        public void Creating_AssuresTelemtryClientIsInitialized_once()
        {
            // Arrange
            var initServantMock = new Mock<IApplicationInsightsInitializationServant>();

            // Act
            new TelemetryClientProxy(initServantMock.Object);

            // Assert
            initServantMock.Verify(f => f.AssureApplictionInsightsIsInitialized(), Times.Once);
        }
    }
}