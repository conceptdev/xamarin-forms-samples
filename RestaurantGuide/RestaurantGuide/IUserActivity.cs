using System;

namespace RestaurantGuide
{
	public interface IUserActivity
	{
		void Start(Restaurant restaurant);
		void Stop();
	}
}

