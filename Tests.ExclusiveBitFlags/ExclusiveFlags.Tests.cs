namespace Tests.ExclusiveBitFlags {
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using com.akoimeexx.Common.Attributes;

    [TestClass]
    public class ExclusiveFlagsUnitTests {
        [TestMethod]
        public void NoExclusiveValuesSet() {
            CLISwitches switches =
                CLISwitches.Recursive |
                CLISwitches.LongListing |
                CLISwitches.HumanReadable;

            Assert.AreEqual(true, switches.IsValidExclusive());
            Assert.AreEqual(
                true, 
                ExclusiveFlagsAttribute.IsValidExclusive(switches)
            );
        }
        [TestMethod]
        public void ExclusiveValuesSet() {
            CLISwitches switches = 
                CLISwitches.LongListing | 
                CLISwitches.WideListing;

            Assert.AreEqual(false, switches.IsValidExclusive());
            Assert.AreEqual(
                false, 
                ExclusiveFlagsAttribute.IsValidExclusive(switches)
            );
        }
        [TestMethod]
        public void EnumWithNoExclusiveAttributes() {
            NonExclusive non = 
                NonExclusive.Top | 
                NonExclusive.Right | 
                NonExclusive.Bottom | 
                NonExclusive.Left;

            Assert.AreEqual(true, non.IsValidExclusive());
            Assert.AreEqual(
                true,
                ExclusiveFlagsAttribute.IsValidExclusive(non)
            );
        }
        [TestMethod]
        public void NonFlagEnum() {
            Assert.AreEqual(true, NonFlag.Up.IsValidExclusive());
            Assert.AreEqual(
                true,
                ExclusiveFlagsAttribute.IsValidExclusive(NonFlag.Up)
            );
        }
        [TestMethod]
        public void InvalidAttributeEnum() {
            Assert.AreEqual(
                false, 
                InvalidAttribParam.Eight.IsValidExclusive()
            );
            Assert.AreEqual(
                false,
                ExclusiveFlagsAttribute.IsValidExclusive(
                    InvalidAttribParam.Eight
                )
            );
        }
        [TestMethod]
        public void EmptyExclusiveConstructorEnum() {
            Assert.AreEqual(
                true, 
                EmptyExclusiveConstructor.EmptyConstructor.IsValidExclusive()
            );
            Assert.AreEqual(
                true, 
                ExclusiveFlagsAttribute.IsValidExclusive(
                    EmptyExclusiveConstructor.EmptyConstructor
                )
            );
        }
    }
}
