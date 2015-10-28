using System;
using System.Collections.Generic;
using System.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace TrainingTest
{
    [TestClass]
    public class IrrationalRootTest
    {
        [TestMethod]
        public void Constructor_reject_squares()
        {
            try
            {
                new IrrationalRoot(4);
            }
            catch (ArgumentException e)
            {
                StringAssert.Contains(e.Message, "input is a perfect square so will not have an irrational root!");
            }
        }

        [TestMethod]
        public void Constructor_reject_nonPositives()
        {
            try
            {
                new IrrationalRoot(-1);
            }
            catch (ArgumentException e)
            {
                StringAssert.Contains(e.Message, "Require positive input!");
            }
        }

        [TestMethod]
        public void ContinuedFractionExpansion_testCorrectOutput_root3()
        {
            var iR = new IrrationalRoot(3);
            var expansion = iR.getCFExpansion();
            var expectedResult = new List<int>(){1,1,2};
            CollectionAssert.AreEqual(expectedResult, expansion,"Wrong continued fraction expansion root 3");
        }

        [TestMethod]
        public void CFExpansion_testCorrectOutput_root31()
        {
            var iR = new IrrationalRoot(31);
            var expansion = iR.getCFExpansion();
            var expectedResult = new List<int>() { 5,1,1,3,5,3,1,1,10};
            CollectionAssert.AreEqual(expectedResult, expansion, "Wrong continued fraction expansion root 31");
        }

        [TestMethod]
        public void getDecExpansion_testCorrectExpansionString_root3_10dps()
        {
            var iR = new IrrationalRoot(3);
            var expansionString = iR.getDecimalExp(10);
            var expectedString = "1.7320508075";
            Assert.AreEqual(expectedString, expansionString);
        }

        [TestMethod]
        public void getDecExpansion_testCorrectExpansionString_root3_60dps()
        {
            var iR = new IrrationalRoot(3);
            var expansionString = iR.getDecimalExp(60);
            var expectedString = "1.732050807568877293527446341505872366942805253810380628055806";
            Assert.AreEqual(expectedString,expansionString);
        }

        [TestMethod]
        public void getDecExpansion_testCorrectExpansionString_root2_397dps()
        {
            var iR = new IrrationalRoot(2);
            var expansionString = iR.getDecimalExp(396);
            var expectedString = "1.414213562373095048801688724209698078569671875376948073176679737990"+
            "732478462107038850387534327641572735013846230912297024924836055850737212644121497099935831"+
            "4132226659275055927557999505011527820605714701095599716059702745345968620147285174186408891"+
            "98609552329230484308714321450839762603627995251407989687253396546331808829640620615258352"+
            "395054745750287759961729835575220337531857011354374603408498";
            Assert.AreEqual(expectedString, expansionString);
        }

    }
}
