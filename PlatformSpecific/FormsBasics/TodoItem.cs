using System;

namespace PlatformSpecific
{
	public class TodoItem
	{
		public TodoItem ()
		{
		}
		public int ID { get; set; }
		public string Name { get; set; }
		public string Notes { get; set; }
		public bool Done { get; set; }
	}
}

