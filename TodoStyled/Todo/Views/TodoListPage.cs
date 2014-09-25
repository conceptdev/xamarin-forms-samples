using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Todo
{
	/// <summary>
	/// Required for PlatformRenderer
	/// </summary>
	public class NotesListView : ListView 
	{
	}

	public class TodoListPage : ContentPage
	{
		ListView listView;
		Image newImage;
		RelativeLayout layout;

		public TodoListPage ()
		{
			Title = "Todo";
			BackgroundColor = Constants.Yellow;
			NavigationPage.SetHasNavigationBar (this, true);
            int listY = 0;

			listView = new NotesListView ();
			listView.ItemTemplate = new DataTemplate (typeof (TodoItemCell));
			listView.BackgroundColor = Constants.Yellow;

			listView.ItemSelected += (sender, e) => {
				if (e.SelectedItem == null) {
					return; // ensures we ignore this handler when the selection is just being cleared
				}
				var todoItem = (TodoItem)e.SelectedItem;
				var todoPage = new TodoItemPage();
				todoPage.BindingContext = todoItem;
				Navigation.PushAsync(todoPage);
				((ListView)sender).SelectedItem = null; // clears the 'selected' background
			};

            layout = new RelativeLayout();

            if (Device.OS == TargetPlatform.WinPhone)
            {
                listY = 60;
                layout.Children.Add(new BoxView { BackgroundColor = Constants.Brown },
                    xConstraint: Constraint.Constant(0),
                    yConstraint: Constraint.Constant(0),
                    widthConstraint: Constraint.RelativeToParent((parent) => { return parent.Width; }),
                    heightConstraint: Constraint.Constant(listY)
                );
                layout.Children.Add(new Label { Text = "Todo", Font= Font.SystemFontOfSize(36), XAlign = TextAlignment.Center },
                    xConstraint: Constraint.Constant(0),
                    yConstraint: Constraint.Constant(10),
                    widthConstraint: Constraint.RelativeToParent((parent) => { return parent.Width; }),
                    heightConstraint: Constraint.Constant(listY - 10)
                );
            }

                

            layout.Children.Add(listView,
                xConstraint: Constraint.Constant(0),
                yConstraint: Constraint.Constant(listY),
                widthConstraint: Constraint.RelativeToParent((parent) => { return parent.Width; }),
                heightConstraint: Constraint.RelativeToParent((parent) => { return parent.Height; })
            );

            #region floating Add button for iOS and Android
            if (Device.OS != TargetPlatform.WinPhone)
            {
                // make floating (+) image at bottom of screen
                var tap = new TapGestureRecognizer(async (View obj) =>
                {
                    var todoItem = new TodoItem();
                    var todoPage = new TodoItemPage();
                    todoPage.BindingContext = todoItem;

                    var b = newImage.Bounds;
                    b.X = b.X + 5;
					b.Y = b.Y + 5;

                    await newImage.LayoutTo(b, 100);
                    b.X = b.X - 5;
					b.Y = b.Y - 5;
                    await newImage.LayoutTo(b, 100);

                    Navigation.PushAsync(todoPage);
                });
                newImage = new Image
                {
                    Source = "newitem.png",
                    WidthRequest = 40,
                    Opacity = 0.8f
                };
                newImage.GestureRecognizers.Add(tap);

                layout.Children.Add(newImage,
                xConstraint: Constraint.RelativeToParent((parent) =>
                {
                    return (parent.Width / 2) - 20; // center of image (which is 40 wide)
                }),
                yConstraint: Constraint.RelativeToParent((parent) =>
                {
                    return parent.Height - 60;
                }));
            }
            #endregion

			Content = layout;


			#region toolbar for WinPhone (only)
            if (Device.OS == TargetPlatform.WinPhone)
            {
                var tbi = new ToolbarItem("add", "add.png", () =>
                {
                    var todoItem = new TodoItem();
                    var todoPage = new TodoItemPage();
                    todoPage.BindingContext = todoItem;
                    Navigation.PushAsync(todoPage);
                }, 0, 0);
                ToolbarItems.Add(tbi);
			
                var tbi2 = new ToolbarItem("speak", "transport.play.png", () =>
                {
                    var todos = App.Database.GetItemsNotDone();
                    var tospeak = "";
                    foreach (var t in todos)
                        tospeak += t.Name + " ";
                    if (tospeak == "") tospeak = "there are no tasks to do";
                    App.Speech.Speak(tospeak);
                }, 0, 0);
                ToolbarItems.Add(tbi2);
			} else {
				var tbi2 = new ToolbarItem("read", "chat.png", () =>
					{
						var todos = App.Database.GetItemsNotDone();
						var tospeak = "";
						foreach (var t in todos)
							tospeak += t.Name + " ";
						if (tospeak == "") tospeak = "there are no tasks to do";
						App.Speech.Speak(tospeak);
					}, 0, 0);
				ToolbarItems.Add(tbi2);
			}
			#endregion
		}

		protected override void OnAppearing ()
		{
			base.OnAppearing ();
			listView.ItemsSource = App.Database.GetItems ();
		}
	}
}

