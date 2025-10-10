using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.Threading;


namespace Eight_Ball
{
    public partial class Form1 : Form
    {
        //RNG for responses
        Random rand = new Random();

        //import for cancelling any pending delay
        private CancellationTokenSource hideTokenSource;
        
        public Form1()
        {
            InitializeComponent();

        }

        private async void pictureBox1_Click(object sender, EventArgs e)
        {
            //cancell previous hide task for text box if pressed again
            hideTokenSource?.Cancel(); // cancel any previous delay
            hideTokenSource = new CancellationTokenSource(); //create new token
            CancellationToken token = hideTokenSource.Token;
            //sound in
            SoundPlayer shake = new SoundPlayer(Properties.Resources.shakeSfx);
            SoundPlayer pop = new SoundPlayer(Properties.Resources.popSfx);
            shake.Play();

            //delay for time of sound before answer
            await Task.Delay(1800);

            //random instance for answer
            int answer = rand.Next(1, 16);

            //set text to switch function pull
            answerBox.Text = outcome(answer);
            pop.Play();
            answerBox.Visible = true;

            //refresh after a set amount of time, but only if one individual press waits for given amount of time
            try
            {
                await Task.Delay(3000, token); // checks for cancellation, like being pressed again
                answerBox.Visible = false;
            }
            catch (TaskCanceledException) {/*cancellation*/}
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private string outcome(int answer)
        {
            switch (answer)
            {
                case 1: return "Yes – definitely.";
                case 2: return "It is certain.";
                case 3: return "Without a doubt.";
                case 4: return "Outlook good.";
                case 5: return "Signs point to yes.";
                case 6: return "Reply hazy, try again.";
                case 7: return "Ask again later.";
                case 8: return "Better not tell you now.";
                case 9: return "Cannot predict now.";
                case 10: return "Concentrate and ask again.";
                case 11: return "Don't count on it.";
                case 12: return "My reply is no.";
                case 13: return "My sources say no.";
                case 14: return "Outlook not so good.";
                case 15: return "Very doubtful.";

                default:
                    return "Never Used";
            }
            
        }

    }
}
