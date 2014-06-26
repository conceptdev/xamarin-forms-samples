using System;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.WindowsAzure.MobileServices.Sync;

namespace TodoXaml
{
	/// <summary>
	/// Manager classes are an abstraction on the data access layers
	/// </summary>
	public class TodoItemManager {
	
		IMobileServiceSyncTable<TodoItem> todoTable;
		IMobileServiceClient client;

		public TodoItemManager (IMobileServiceClient client, IMobileServiceSyncTable<TodoItem> todoTable) 
		{
			this.client = client;
			this.todoTable = todoTable;
		}

		public async Task<TodoItem> GetTaskAsync(string id)
		{
			try 
			{
				await SyncAsync();
				return await todoTable.LookupAsync(id);
			} 
			catch (MobileServiceInvalidOperationException msioe)
			{
				Debug.WriteLine(@"INVALID {0}", msioe.Message);
			}
			catch (Exception e) 
			{
				Debug.WriteLine(@"ERROR {0}", e.Message);
			}
			return null;
		}

		public async Task<List<TodoItem>> GetTasksAsync ()
		{
			try 
			{
				await SyncAsync();
				return new List<TodoItem> (await todoTable.ReadAsync());
			} 
			catch (MobileServiceInvalidOperationException msioe)
			{
				Debug.WriteLine(@"INVALID {0}", msioe.Message);
			}
			catch (Exception e) 
			{
				Debug.WriteLine(@"ERROR {0}", e.Message);
			}
			return null;
		}

		public async Task SaveTaskAsync (TodoItem item)
		{
			if (item.ID == null)
				await todoTable.InsertAsync(item);
			else
				await todoTable.UpdateAsync(item);
			//await SyncAsync ();
		}

		public async Task DeleteTaskAsync (TodoItem item)
		{
			try 
			{
				await todoTable.DeleteAsync(item);
				//await SyncAsync ();
			} 
			catch (MobileServiceInvalidOperationException msioe)
			{
				Debug.WriteLine(@"INVALID {0}", msioe.Message);
			}
			catch (Exception e) 
			{
				Debug.WriteLine(@"ERROR {0}", e.Message);
			}
		}

		public async Task SyncAsync()
		{
			try
			{
				await this.client.SyncContext.PushAsync();
				await this.todoTable.PullAsync();
			}
			catch (MobileServiceInvalidOperationException e)
			{
				Debug.WriteLine(@"Sync Failed: {0}", e.Message);
			}
		}
	}
}

