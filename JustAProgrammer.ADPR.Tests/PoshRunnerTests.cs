using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation.Host;
using System.Text;
using NUnit.Framework;

namespace JustAProgrammer.ADPR.Tests
{
    [TestFixture]
    public class PoshRunnerTests
    {
        private AppDomainPoshRunner poshRunner = new AppDomainPoshRunner();
        private ADPRState state = new ADPRState();
        private ADPRHost host;


        public PoshRunnerTests()
        {
            host = new ADPRHost(state);
        }

        [Test]
        public void TestWriteDebug()
        {
            poshRunner.RunScript("Write-Debug 'Debug Message'", host);
        }

        [Test]
        public void TestWriteVerbose()
        {
            poshRunner.RunScript("Write-Verbose 'Verbose Message'", host);
        }

        [Test]
        public void TestWriteHost()
        {
            poshRunner.RunScript("Write-Host 'Host Message'", host);
        }

        [Test]
        public void TestWriteOutput()
        {
            poshRunner.RunScript("Write-Output 'Output Message'", host);
        }

        [Test]
        public void TestWriteWarning()
        {
            poshRunner.RunScript("Write-Warning 'Warning Message'", host);
        }

        [Test]
        public void TestWriteError()
        {
            poshRunner.RunScript("Write-Error 'Error Message'", host);
        }
    }
}
