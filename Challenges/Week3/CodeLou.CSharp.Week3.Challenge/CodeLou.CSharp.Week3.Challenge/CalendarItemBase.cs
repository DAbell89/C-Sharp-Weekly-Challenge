using System;
using System.Collections.Generic;

namespace CodeLou.CSharp.Week3.Challenge
{
	public abstract class CalendarItemBase
	{
		public int Id { get; set; }
        public DateTime startDateTime;
        public DateTime endDateTime;
        public string Location;
        public List<string> attendees;
    }
}
