using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Domain.Model.Test
{
    [TestClass]
    public class DateScopeTest
    {
        [TestMethod]
        public void Test_GetQuarters_InSameMonth()
        {
            // in the single month
            DateTime start = new DateTime(2012, 2, 2);
            DateTime end = new DateTime(2012, 2, 26);
            DateScope dateScope = new DateScope(start, end);

            var quarter = dateScope.GetQuarters();
            Assert.AreEqual(quarter.ElementAt(0), new KeyValuePair<int, Quarter>(2012, Quarter.Q1));
        }

        [TestMethod]
        public void Test_GetQuarters_InDiffMonths()
        {
            // in the multiple months
            DateTime start = new DateTime(2012, 1, 2);
            DateTime end = new DateTime(2012, 3, 4);
            DateScope dateScope = new DateScope(start, end);
            List<KeyValuePair<int, Quarter>> quarters = new List<KeyValuePair<int, Quarter>>
            {
                new KeyValuePair<int, Quarter>(2012, Quarter.Q1)
            };

            Assert.IsTrue(IsEqualOfQuarters(dateScope.GetQuarters(), quarters));
        }

        [TestMethod]
        public void Test_GetQuarters_CrossMultiQuarter()
        {
            DateTime start = new DateTime(2012, 2, 5);
            DateTime end = new DateTime(2012, 4, 20);
            DateScope dataScope = new DateScope(start, end);
            List<KeyValuePair<int, Quarter>> quarters = new List<KeyValuePair<int, Quarter>>
            {
                new KeyValuePair<int, Quarter>(2012, Quarter.Q1),
                new KeyValuePair<int, Quarter>(2012, Quarter.Q2)
            };
            Assert.IsTrue(IsEqualOfQuarters(dataScope.GetQuarters(), quarters));
        }

        [TestMethod]
        public void Test_GetQuarters_CrossMultiYears()
        {
            DateTime start = new DateTime(2013, 1, 5);
            DateTime end = new DateTime(2012, 12, 20);
            DateScope dataScope = new DateScope(start, end);
            List<KeyValuePair<int, Quarter>> quarters = new List<KeyValuePair<int, Quarter>>
            {
                new KeyValuePair<int, Quarter>(2012, Quarter.Q4),
                new KeyValuePair<int, Quarter>(2013, Quarter.Q1)
            };
            Assert.IsTrue(IsEqualOfQuarters(dataScope.GetQuarters(), quarters));
        }

        private static bool IsEqualOfQuarters(
           IEnumerable<KeyValuePair<int, Quarter>> actural,
           IEnumerable<KeyValuePair<int, Quarter>> expectation)
        {
            return !actural.Where((t, i) => !t.Equals(expectation.ElementAt(i))).Any();
        }
    }
}
