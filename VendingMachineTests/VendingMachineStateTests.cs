using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using VendingMachine;
using VendingMachine.DTO;
using VendingMachine.Exceptions;

namespace VendingMachineTest
{
    [TestFixture]
    public class VendingMachineStateTests
    {
        [TestCase("TwoPound - 10,  Pound -  2,   Penny - 10")]
        [TestCase("  Penny -  12,   Penny -10")]
        [TestCase(" fiftypence - 22")]
        public void VendingMachineState_ProcessString_Successful(string input)
        {
            var vendingMachineState = new VendingMachineState(new VendingCash());

            var inputCoins = vendingMachineState.ProcessInputCoins(input);

            inputCoins.Count.Should().Be(22);
        }

        [TestCase("TwoPound 10")]
        [TestCase("  Penny -  12,  -10")]
        [TestCase(" fiftypence - 22; TenPence-23")]
        public void VendingMachineState_ProcessString_Failed(string input)
        {
            var vendingMachineState = new VendingMachineState(new VendingCash());

            Assert.Throws<InvalidInputException>(() => vendingMachineState.ProcessInputCoins(input));
        }

        [Test]
        public void VendingMachineState_ProductExist()
        {
            var vendingMachineState = new VendingMachineState(new VendingCash())
            {
                Products = new Dictionary<Product, int>
                {
                    {new Product("Mars", 0.85m), 10},
                    {new Product("Snickers", 0.85m), 10}
                }
            };
            vendingMachineState.ProductExists("Mars").Should().NotBeNull();
            vendingMachineState.ProductExists("Snickers").Price.Should().Be(0.85m);
            vendingMachineState.ProductExists("Snickes").Should().BeNull();
        }
    }
}
