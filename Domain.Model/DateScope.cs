﻿using System;
using System.Collections.Generic;

namespace Domain.Model
{
    public enum Quarter
    {
        Q1 = 1,
        Q2,
        Q3,
        Q4
    }

    public class DateScope
    {
        public DateScope(DateTime start, DateTime end)
        {
            if (start > end)
            {
                this.End = start;
                this.Start = end;
            }
            else
            {
                this.Start = start;
                this.End = end;
            }
        }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        // this function is used to get how many quarter in the date scope, 
        // becasue sina just get stock history by quarter
        public IEnumerable<KeyValuePair<int, Quarter>> GetQuarters()
        {
            List<KeyValuePair<int, Quarter>> quaterSpan = new List<KeyValuePair<int, Quarter>>();
            
            Quarter startQ = GetQuarter(this.Start);
            Quarter endQ = GetQuarter(this.End);

            int startYear = Start.Year;
            int endYear = End.Year;

            //int totalQ1 = startQ <= endQ ? (int)endQ : (int)endQ + 4 * (endYear - startYear);

            int totalQ = 4*(endYear - startYear) + (int) endQ - (int) startQ + 1;

            for (int q = 0; q < totalQ; q++)
            {
                Quarter value = (q + (int)startQ) % 4 == 0 ? Quarter.Q4 : (Quarter)((q + (int)startQ) % 4);
                quaterSpan.Add(new KeyValuePair<int, Quarter>(startYear + (q +(int)startQ  -1) / 4, value));
            }
            
            return quaterSpan;
        } 

        private static Quarter GetQuarter(DateTime date)
        {
            switch (date.Month)
            {
                case 3:
                case 2:
                case 1:
                    return Quarter.Q1;
                case 6:
                case 5:
                case 4:
                    return Quarter.Q2;
                case 9:
                case 8:
                case 7:
                    return Quarter.Q3;
                default:
                    return Quarter.Q4;
            }
        }
    }
}
