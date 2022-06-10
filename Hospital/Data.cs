using System;
using System.Collections.Generic;

namespace Hospital
{
    public class Data
    {
        public struct Account
        {
            public string Name { get; set; }
            public string mail { get; set; }
            public string password { get; set; }
            public List<Patient> patients { get; set; }
        }

        public struct Patient
        {
            public string fullName { get; set; }
            public TimeOnly start;
            public TimeOnly end;
            public DayOfWeek day { get; set; }

            public string FullString { 
                get
                {
                    return $"{start} - {end} - {fullName}";
                }
            }

            public string Start { 
                get 
                { 
                    return start.ToString(); 
                } 
                set 
                {
                    start = TimeOnly.Parse(value);
                }
            }

            public string End
            {
                get
                {
                    return end.ToString();
                }
                set
                {
                    end = TimeOnly.Parse(value);
                }
            }
        }
    }
}
