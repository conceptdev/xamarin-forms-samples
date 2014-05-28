using System;

namespace TodoMvvm
{
	class TodoItemCellViewModel : BaseViewModel
	{
		TodoItem todo;

		public TodoItem Item { get { return todo; }}

		public string Name { get { return todo.Name; }}

		public bool Done {get { return todo.Done; }}

		public TodoItemCellViewModel (TodoItem todoItem)
		{
			todo = todoItem;
		}
	}
}

