using System;
using Xamarin.Forms;

namespace TodoMvvm
{
	class ViewModelNavigation
	{
		readonly Page implementor;

		public ViewModelNavigation (Page implementor)
		{
			this.implementor = implementor;
		}

		public void Push (Page page)
		{
			implementor.Navigation.PushAsync (page);
		}

		public void Push<TViewModel> ()
			where TViewModel : BaseViewModel
		{
			Push (ViewFactory.CreatePage<TViewModel> ());
		}

		public void Pop ()
		{
			implementor.Navigation.PopAsync ();
		}

		public void PopToRoot ()
		{
			implementor.Navigation.PopToRootAsync ();
		}

		public void PushModal (Page page)
		{
			implementor.Navigation.PushModalAsync (page);
		}

		public void PushModal<TViewModel> ()
			where TViewModel : BaseViewModel
		{
			PushModal (ViewFactory.CreatePage<TViewModel> ());
		}

		public void PopModal ()
		{
			var modalParent = implementor;
			while (modalParent.Parent as Page != null)
				modalParent = (Page) modalParent.Parent;
			implementor.Navigation.PopModalAsync ();
		}
	}
}

