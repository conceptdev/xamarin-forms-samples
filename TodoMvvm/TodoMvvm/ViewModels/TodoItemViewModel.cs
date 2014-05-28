using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace TodoMvvm
{
	class TodoItemViewModel : BaseViewModel
	{
		TodoItem todo;

		ICommand saveCommand, deleteCommand, cancelCommand, speakCommand;

		public TodoItemViewModel (TodoItem todoItem) 
		{
			todo = todoItem;
			saveCommand = new Command (Save);
			deleteCommand = new Command (Delete);
			cancelCommand = new Command (() => Navigation.Pop());
			speakCommand = new Command (Speak);
		}

		public void Save () 
		{
			MessagingCenter.Send (this, "TodoSaved", todo);
			Navigation.Pop ();
		}
		public void Delete () 
		{
			MessagingCenter.Send (this, "TodoDeleted", todo);
			Navigation.Pop ();
		}
		public void Speak () 
		{
			MessagingCenter.Send (this, "TodoSpeak", todo);
		}

		public string Name
		{
			get { return todo.Name; }
			set
			{
				if (todo.Name == value)
					return;
				todo.Name = value;
				OnPropertyChanged ();
			}
		}

		public string Notes
		{
			get { return todo.Notes; }
			set
			{
				if (todo.Notes == value)
					return;
				todo.Notes = value;
				OnPropertyChanged ();
			}
		}

		public bool Done
		{
			get { return todo.Done; }
			set
			{
				if (todo.Done == value)
					return;
				todo.Done = value;
				OnPropertyChanged ();
			}
		}

		public bool CanDelete
		{
			get { return todo.ID > 0; }
		}
		public bool CanCancel
		{
			get { return !CanDelete; }
		}

		/// <summary>Can only speak if there is some text</summary>
		public bool CanSpeak
		{
			get { 
				if (Device.OS == TargetPlatform.iOS)
					return (!string.IsNullOrEmpty(todo.Name)) | (!string.IsNullOrEmpty(todo.Notes)); 
				else
					return false; // not on Android yet
				}
		}

		public ICommand SaveCommand
		{
			get { return saveCommand; }
			set
			{
				if (saveCommand == value)
					return;
				saveCommand = value;
				OnPropertyChanged ();
			}
		}

		public ICommand DeleteCommand
		{
			get { return deleteCommand; }
			set
			{
				if (deleteCommand == value)
					return;
				deleteCommand = value;
				OnPropertyChanged ();
			}
		}

		public ICommand CancelCommand
		{
			get { return cancelCommand; }
			set
			{
				if (cancelCommand == value)
					return;
				cancelCommand = value;
				OnPropertyChanged ();
			}
		}

		public ICommand SpeakCommand
		{
			get { return speakCommand; }
			set
			{
				if (speakCommand == value)
					return;
				speakCommand = value;
				OnPropertyChanged ();
			}
		}
	}
}

