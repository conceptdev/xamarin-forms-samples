using System;
using Xamarin.Forms;

namespace TicTacForms
{
    public class App
    {
        public static Page GetMainPage()
        {
     		return new TicTacForms.TicTacToePage();
        }
    }
}
