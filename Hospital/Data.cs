using System;
using System.Collections.Generic;

namespace Hospital
{
    public class Data
    {
        public struct Account
        {
            public string Name;
            public string mail;
            public string password;
            public List<Patient> patients;
        }

        public struct Patient
        {
            public string fullName;
            public TimeOnly start;
            public TimeOnly end;
            public DayOfWeek day;

            public string FullString { 
                get
                {
                    return $"{start} - {end} - {fullName}";
                }
            }
        }
    }
}
