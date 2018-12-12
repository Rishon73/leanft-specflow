using System;
using System.Linq;
using NUnit.Framework;
using NUnit.Common;

using HP.LFT.Report;
using HP.LFT.Report.Configuration;
using HP.LFT.UnitTesting;

namespace leanft_specflow
{
    [TestFixture]
    public abstract class UnitTestClassBase : UnitTestBase
    {
        [OneTimeSetUp]
        public void GlobalSetup()
        {
            TestSuiteSetup();
        }

        [OneTimeTearDown]
        public void GlobalTearDown()
        {
            TestSuiteTearDown();
            Reporter.GenerateReport();
        }

        [SetUp]
        public void BasicSetUp()
        {
            TestSetUp();
        }

        [TearDown]
        public void BasicTearDown()
        {
            Console.WriteLine("***** Shahar ***** UnitTestClassBase::TearDown");
            TestTearDown();
        }

        protected override string GetClassName()
        {
            return TestContext.CurrentContext.Test.FullName;
        }

        protected override string GetTestName()
        {
            return TestContext.CurrentContext.Test.Name;
        }

        protected override Status GetFrameworkTestResult()
        {
            Console.WriteLine("***** Shahar ***** UnitTestClassBase::GetFrameworkTestResult(" + TestContext.CurrentContext.Result.Outcome.Status.ToString() + ")");

            switch (TestContext.CurrentContext.Result.Outcome.Status)
            {
                case NUnit.Framework.Interfaces.TestStatus.Failed:
                    return Status.Failed;
                case NUnit.Framework.Interfaces.TestStatus.Inconclusive:
                case NUnit.Framework.Interfaces.TestStatus.Skipped:
                    return Status.Warning;
                case NUnit.Framework.Interfaces.TestStatus.Passed:
                    return Status.Passed;
                default:
                    return Status.Passed;
            }
        }
    }
}

