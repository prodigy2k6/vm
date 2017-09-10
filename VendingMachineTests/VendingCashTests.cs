using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using VendingMachine;
using VendingMachine.DTO;
using VendingMachine.DTO.Enums;

namespace VendingMachineTest
{
    [TestFixture]
    public class VendingCashTests
    {
      
        [Test]
        public void VendingCash_Sum_Correct()
        {
            var vendingCash = new VendingCash();

            vendingCash.AddCoins(new List<Denomination>
            {
                Currency.Pound, Currency.TwoPence, Currency.Pound, Currency.Penny, Currency.FiftyPence
            });

            var total = vendingCash.CurrentTotal();

            total.ShouldBeEquivalentTo(2.53);
        }

        [Test]
        public void VendingCash_CoinsOrderedByHighestValue()
        {
            var vendingCash = new VendingCash();

            vendingCash.AddCoins(new List<Denomination>
            {
                Currency.FivePence,
                Currency.FiftyPence,
                Currency.Pound,
                Currency.TenPence,
                Currency.TwentyPence,
                Currency.TwoPence,
                Currency.Pound,
                Currency.Penny,
                Currency.TwoPound,
                Currency.FiftyPence,
                Currency.Pound,
            });

            vendingCash.InternalCash.Count.Should().Be(8);

            vendingCash.InternalCash.Keys.ElementAt(0).Name.Should().Be(DenominationNames.TwoPound);
            vendingCash.InternalCash.Keys.ElementAt(1).Name.Should().Be(DenominationNames.Pound);
            vendingCash.InternalCash.Keys.ElementAt(2).Name.Should().Be(DenominationNames.FiftyPence);
            vendingCash.InternalCash.Keys.ElementAt(3).Name.Should().Be(DenominationNames.TwentyPence);
            vendingCash.InternalCash.Keys.ElementAt(4).Name.Should().Be(DenominationNames.TenPence);
            vendingCash.InternalCash.Keys.ElementAt(5).Name.Should().Be(DenominationNames.FivePence);
            vendingCash.InternalCash.Keys.ElementAt(6).Name.Should().Be(DenominationNames.TwoPence);
            vendingCash.InternalCash.Keys.ElementAt(7).Name.Should().Be(DenominationNames.Penny);
        }

        [Test]
        public void VendingCash_AddCoins_CoinCountCorrect()
        {
            var vendingCash = new VendingCash();

            vendingCash.AddCoins(new List<Denomination>
            {
                Currency.FivePence,
                Currency.FiftyPence,
                Currency.Pound,
                Currency.TenPence,
                Currency.TwentyPence,
                Currency.Penny,
                Currency.TwoPence,
                Currency.Pound,
                Currency.Penny,
                Currency.TwoPound,
                Currency.FiftyPence,
                Currency.Pound,
                Currency.FivePence,
            });

            vendingCash.InternalCash.Count.Should().Be(8);

            vendingCash.InternalCash.Values.ElementAt(0).Should().Be(1);
            vendingCash.InternalCash.Values.ElementAt(1).Should().Be(3);
            vendingCash.InternalCash.Values.ElementAt(2).Should().Be(2);
            vendingCash.InternalCash.Values.ElementAt(3).Should().Be(1);
            vendingCash.InternalCash.Values.ElementAt(4).Should().Be(1);
            vendingCash.InternalCash.Values.ElementAt(5).Should().Be(2);
            vendingCash.InternalCash.Values.ElementAt(6).Should().Be(1);
            vendingCash.InternalCash.Values.ElementAt(7).Should().Be(2);
        }

        [TestCase(1)]
        [TestCase(1.30)]
        [TestCase(3.20)]
        [TestCase(0.10)]
        [TestCase(0.03)]
        [TestCase(0.50)]
        [TestCase(0.04)]
        [TestCase(0.74)]
        public void VendingCash_GetChange_ChangeAvailable_Success(decimal payment)
        {
            var vendingCash = new VendingCash();

            vendingCash.AddCoins(new List<Denomination>
            {
                Currency.FivePence,
                Currency.FiftyPence,
                Currency.Pound,
                Currency.TenPence,
                Currency.TwentyPence,
                Currency.Penny,
                Currency.TwoPence,
                Currency.Pound,
                Currency.Penny,
                Currency.TwoPound,
                Currency.FiftyPence,
                Currency.Pound,
                Currency.FivePence,
            });

            vendingCash.CanReturnChange(payment).Success.Should().BeTrue();

        }

        [TestCase(23)]
        [TestCase(1.01)]
        public void VendingCash_GetChange_ChangeUnAvailable_FailureExpected(decimal payment)
        {
            var vendingCash = new VendingCash();

            vendingCash.AddCoins(new List<Denomination>
            {
                Currency.FivePence,
                Currency.FiftyPence,
                Currency.Pound,
                Currency.TenPence,
                Currency.TwentyPence,
                Currency.TwoPence,
                Currency.Pound,
                Currency.TwoPound,
                Currency.FiftyPence,
                Currency.Pound,
                Currency.FivePence
            });

            vendingCash.CanReturnChange(payment).Success.Should().BeFalse();

        }

        [TestCase(1.01)]
        public void VendingCash_GetChange_ExactChange_Success(decimal payment)
        {
            var vendingCash = new VendingCash();

            vendingCash.CanReturnChange(payment, new List < Denomination >
            {
                Currency.Pound,
                Currency.Penny
            }).Success.Should().BeTrue();

        }
    }
}
