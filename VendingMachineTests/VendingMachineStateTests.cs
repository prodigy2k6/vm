using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using VendingMachine;
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
            var vendingMachineState = new VendingMachineState();

            var inputCoins = vendingMachineState.ProcessInputCoins(input);

            inputCoins.Count.Should().Be(22);
        }

        [TestCase("TwoPound 10")]
        [TestCase("  Penny -  12,  -10")]
        [TestCase(" fiftypence - 22; TenPence-23")]
        public void VendingMachineState_ProcessString_Failed(string input)
        {
            var vendingMachineState = new VendingMachineState();

            Assert.Throws<InvalidInputException>(() => vendingMachineState.ProcessInputCoins(input));
        }
    }
}
