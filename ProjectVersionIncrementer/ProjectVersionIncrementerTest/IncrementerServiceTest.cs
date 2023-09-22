using ProjectVersionIncrementer;
using Xunit;
using Xunit.Sdk;

namespace ProjectVersionIncrementerTest
{
    public class IncrementerServiceTests
    {
        [Fact]
        public void TestFeatureRelease()
        {
            var currentVersion = "10.8.15.7";
            var incrementer = new VersionIncrementerService();
            currentVersion = incrementer.FeatureRelease(currentVersion);
            Assert.Equal("10.8.16.7", currentVersion);
        }

        [Fact]
        public void TestBugFixRelease()
        {
            var currentVersion = "10.8.15.7";
            var incrementer = new VersionIncrementerService();
            currentVersion = incrementer.BugFixRelease(currentVersion);
            Assert.Equal("10.8.15.8", currentVersion);
        }
    }
}