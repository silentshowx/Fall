using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.ExceptionServices;

namespace Fall
{
    public partial class frmGame : Form
    {
        Game game { get; set; }        
        public frmGame()
        {
            InitializeComponent();
            Tree tree = new Tree(); tree.ShowDialog(); 

            // News news = new News();
            // news.ShowDialog();

            game = new Game();
            // setup players
            Player yellow = new Player() { Name = "zu1" };
            Player green = new Player() { Name = "ze1" };
            Player red = new Player() { Name = "cr1" };
            Player black = new Player() { Name = "c1" };
            game.lstPlayers = new List<Player>() { yellow, green, red, black };
            game.State = GameState.Start;
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            foreach (Player x in game.lstPlayers)
            {
                game.ActivePlayer = x;
                switch (game.ActivePlayer.Name) {
                    case "zu1": { borderYellow.BorderStyle = BorderStyle.FixedSingle; DisableAllBorders("borderYellow"); } break;
                    case "ze1": { borderGreen.BorderStyle = BorderStyle.FixedSingle; DisableAllBorders("borderGreen"); } break;
                    case "cr1": { borderRed.BorderStyle = BorderStyle.FixedSingle; DisableAllBorders("borderRed"); } break;
                    case "c1" : { borderBlack.BorderStyle = BorderStyle.FixedSingle; DisableAllBorders("borderBlack"); } break;
                }
                lblCube.Text = game.ActivePlayer.LastNumber.ToString();
                if (game.Bingo) lblCube.BorderStyle = BorderStyle.FixedSingle;
                else lblCube.BorderStyle = BorderStyle.None;
                game.RunCubeForActivePlayer(game.ActivePlayer);        
                DisplayFigureByPostion();
                switch (game.ActivePlayer.Name) {
                    case "zu1": { game.ActivePlayer.Name = "ze1"; } break;
                    case "ze1": { game.ActivePlayer.Name = "cr1"; } break;
                    case "cr1": { game.ActivePlayer.Name = "c1"; } break;
                    case "c1": { game.ActivePlayer.Name = "zu1"; } break;
                }
                break;
            }
        }
        public List<KeyValuePair<string, string>> lstCheckNotationPosition() {
            // svako polje ima svoju oznaku i provjeri
            // pretpostavimo da su neka aktivna polja popunjena 
            List<KeyValuePair<string, string>> lst = new List<KeyValuePair<string, string>>();
            lst.Add(new KeyValuePair<string, string>("lbl1", "y2"));
            lst.Add(new KeyValuePair<string, string>("lbl3", "g1"));
            lst.Add(new KeyValuePair<string, string>("lbl5", "g2"));
            lst.Add(new KeyValuePair<string, string>("lbl6", "y1"));
            return lst;
        }

        public void ShowTempPosition(List<KeyValuePair<string, string>> lstKeyValue) {
            foreach (KeyValuePair<string, string> x in lstKeyValue)
            {
                foreach (Control control in this.Controls)
                {
                    if (control is Label label)
                    {
                        if (label.Name == x.Key)
                        {
                            label.Text = x.Key;
                            label.BorderStyle = BorderStyle.FixedSingle;
                        }
                    }
                }
            }
        }
        private void btnMove_Click(object sender, EventArgs e)
        {
            // first run
            if (game.State == GameState.Start) {
                game.whichPlayer = WhichPlayer.Yellow;
            } game.State = GameState.Play;
            // provjeri sve pozicije
            game.lstNotationPosition = lstCheckNotationPosition();
            ShowTempPosition(game.lstNotationPosition);
            // vrti kuglu
            int step = game.GenerateRandomNumber();
            if (game.whichPlayer == WhichPlayer.Yellow)
            {
                // ako ima žutoga na poziciji ako ima u listi žutoga povuci za step
                foreach (KeyValuePair<string, string> x in game.lstNotationPosition) { 
                    
                }
                
                game.whichPlayer = WhichPlayer.Green;
                return;
            }
            else if (game.whichPlayer == WhichPlayer.Green)
            {
                game.whichPlayer = WhichPlayer.Red;
                return;
            }
            else if (game.whichPlayer == WhichPlayer.Red)
            {
                game.whichPlayer = WhichPlayer.Black;
                return;
            }
            else if (game.whichPlayer == WhichPlayer.Black)
            {
                game.whichPlayer = WhichPlayer.Yellow;
                return;
            }
            else { game.whichPlayer = WhichPlayer.None; }
            // other runs
        }

        public int Maths(int from, int step, int max) {
            int cnt = from + step;
            int to = 0;
            if (from + step > max) to = cnt - max;
            else to = from + step;
            return to;
        }

        public void SaveText(object from, object to) {
            if (from != null && to != null) {
                if (from.ToString() != null && to.ToString() != null) {
                    string finalDestination = ((ComboBox)to).SelectedItem.ToString();
                    string clean = finalDestination.Remove(0, 3);
                    int where = Int32.Parse(clean);

                    MessageBox.Show(clean);
                }
            }
        }
    }
}
