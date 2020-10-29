using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Docker.DotNet;
using Docker.DotNet.Models;
using DotNet.Testcontainers.Containers.Builders;
using DotNet.Testcontainers.Containers.Configurations.Databases;
using DotNet.Testcontainers.Containers.Modules.Databases;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DockerIntegrationTest.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task TestMethod1()
        {
            var config = new MsSqlTestcontainerConfiguration();
            config.Environments.Add("SA_PASSWORD", "Th1s1smyP4ssw0rd!!!!!");

            var testContainersBuilder =
                new TestcontainersBuilder<MsSqlTestcontainer>()
                .WithDatabase(config);

            await using var testContainer = testContainersBuilder.Build();
            await testContainer.StartAsync();

            await Lib.Class1.CreateTables(testContainer.ConnectionString);
            await Lib.Class1.SeedData(testContainer.ConnectionString);
            var data = await Lib.Class1.GetData(testContainer.ConnectionString);

            Assert.IsTrue(data.Any());
            Assert.AreEqual(1, data.First().TestId);
        }

    }
}
