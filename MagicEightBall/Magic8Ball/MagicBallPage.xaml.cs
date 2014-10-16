using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Magic8Ball
{
	// Magic Eight Ball 🎱
    public partial class MagicBallPage : ContentPage
    {
        public MagicBallPage()
        {
            InitializeComponent();
        }
        string[] options = {
              "It is certain"
			, "It is decidedly so"
			, "Without a doubt"
			, "Yes definitely"
			, "You may rely on it"
			, "As I see it, yes"
			, "Most likely"
			, "Outlook good"
			, "Yes"
			, "Signs point to yes"

			, "Reply hazy try again"
			, "Ask again later"
			, "Better not tell you now"
			, "Cannot predict now"
			, "Concentrate and ask again"

			, "Don't count on it"
			, "My reply is no"
			, "My sources say no"
			, "Outlook not so good"
			, "Very doubtful"
		};

        public void ShakeClicked(object s, EventArgs e) {
            var rnd = new System.Random();
            output.Text = options[rnd.Next(0, options.Length - 1)];
        }
    }
}
