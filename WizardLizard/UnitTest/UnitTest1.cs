using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WizardLizard;

namespace TestWizardLizard
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestPlayerHealth()
        {
            int result = WizardLizard.Player.Health;
            Assert.AreEqual(6, result);
        }
    }
}
