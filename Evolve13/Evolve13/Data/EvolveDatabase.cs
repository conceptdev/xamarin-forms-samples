using System;
using SQLite;
using System.Collections.Generic;
using System.Linq;

namespace Evolve13
{
	public class EvolveDatabase 
	{
		static object locker = new object ();

		SQLiteConnection database;

		/// <summary>
		/// Initializes a new instance of the <see cref="Tasky.DL.TaskDatabase"/> TaskDatabase. 
		/// if the database doesn't exist, it will create the database and all the tables.
		/// </summary>
		/// <param name='path'>
		/// Path.
		/// </param>
		public EvolveDatabase(SQLiteConnection conn)
		{
			database = conn;
			// create the tables
			database.CreateTable<Speaker>();
			database.CreateTable<Session>();
			database.CreateTable<SessionSpeaker>();
			database.CreateTable<Favorite>();
		}

		public IList<Speaker> GetSpeakers ()
		{
			lock (locker) {
				var speakers = database.Query<Speaker>("SELECT * FROM [Speaker] ORDER BY [Name]");
				return speakers.ToList();
			}
		}

		public IList<Session> GetSessions ()
		{
			lock (locker) {
				var sessions = database.Query<Session>("SELECT * FROM [Session] ORDER BY [Begins]");
				return sessions.ToList();
			}
		}

	}
}

