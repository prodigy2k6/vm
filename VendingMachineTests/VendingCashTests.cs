using NUnit.Framework;
using System.Collections.Generic;
using FluentAssertions;
using VendingMachine;

namespace VendingMachineTest
{
    [TestFixture]
    public class VendingCashTests
    {
        public VendingCash vendingCash = new VendingCash();


        public VendingCashTests()
        {
           
        }

        [Test]
        public void VendingCash_Sum_Correct()
        {
            vendingCash.addCash(new List<Denomination> { new Pound(), new TwoPence(), new Pound(), new Penny(), new FiftyPence() });

            var total = vendingCash.CurrentTotal2();

            total.ShouldBeEquivalentTo(2.53);
        }
    }
}
