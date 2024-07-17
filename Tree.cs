using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Windows.Forms;

namespace Fall
{
    public partial class Tree : Form
    {
        private List<string> imgList = new List<string> { "1.png", "2.png", "3.png", "4.png", "5.png", "6.png" };
        private List<string> imgListFigure = new List<string> { "Yellow.png", "Green.png", "Red.png", "Black.png" };
        private Random random = new Random();
        public int Number { get; set; }
        string BaseDirectory { get; set; }
        string ProjectRoot { get; set; }
        string ResourcePath { get; set; }
        public bool Repeat { get; set; }
        public bool VeryFirstMove { get; set; }
        string StateOfGame { get; set; }

        public List<KeyValuePair<string, FigureX>> tempYellowX = new List<KeyValuePair<string, FigureX>>();
        public List<KeyValuePair<string, FigureX>> tempGreenX = new List<KeyValuePair<string, FigureX>>();
        public List<KeyValuePair<string, FigureX>> tempRedX = new List<KeyValuePair<string, FigureX>>();
        public List<KeyValuePair<string, FigureX>> tempBlackX = new List<KeyValuePair<string, FigureX>>();

        public Tree()
        {
            InitializeComponent();
            VeryFirstMove = true;
            LoadImage();
            PutBorderEvent();
            LoadGraphic();
            FillPlayers();
        }

        public void CheckMovement(int position) {
            // dali se napoziciji nalazi žuta figure provjerit preko liste iz tempyellowx -- u njegovim pozicijama po svakoj figure 
            // ako se nalazi onda ga vrati na početnu poziciju
            foreach (KeyValuePair<string, FigureX> yellow in tempYellowX) {
                if (position == yellow.Value.NumPosition) {
                    MoveFromTo(position, 56); return;
                }
            }
            foreach (KeyValuePair<string, FigureX> green in tempYellowX)
            {

            }
            foreach (KeyValuePair<string, FigureX> red in tempYellowX)
            {

            }
            foreach (KeyValuePair<string, FigureX> black in tempYellowX)
            {

            }
        }
        public void LoadGraphic()
        {
            b73.BackColor = SystemColors.ControlDark;
        }


        public void FillPlayers()
        {

            tempYellowX.Add(new KeyValuePair<string, FigureX>("y1", new FigureX() { Name = "y1", NumPosition = 56, Postion = "b56" }));
            tempYellowX.Add(new KeyValuePair<string, FigureX>("y2", new FigureX() { Name = "y2", NumPosition = 57, Postion = "b57" }));
            tempYellowX.Add(new KeyValuePair<string, FigureX>("y3", new FigureX() { Name = "y3", NumPosition = 58, Postion = "b58" }));
            tempYellowX.Add(new KeyValuePair<string, FigureX>("y4", new FigureX() { Name = "y4", NumPosition = 59, Postion = "b59" }));

            tempGreenX.Add(new KeyValuePair<string, FigureX>("g1", new FigureX() { Name = "g1", NumPosition = 60, Postion = "b60" }));
            tempGreenX.Add(new KeyValuePair<string, FigureX>("g1", new FigureX() { Name = "g2", NumPosition = 61, Postion = "b61" }));
            tempGreenX.Add(new KeyValuePair<string, FigureX>("g1", new FigureX() { Name = "g3", NumPosition = 62, Postion = "b62" }));
            tempGreenX.Add(new KeyValuePair<string, FigureX>("g1", new FigureX() { Name = "g4", NumPosition = 63, Postion = "b63" }));

            tempRedX.Add(new KeyValuePair<string, FigureX>("r1", new FigureX() { Name = "r1", NumPosition = 64, Postion = "b64" }));
            tempRedX.Add(new KeyValuePair<string, FigureX>("r2", new FigureX() { Name = "r2", NumPosition = 65, Postion = "b65" }));
            tempRedX.Add(new KeyValuePair<string, FigureX>("r3", new FigureX() { Name = "r3", NumPosition = 66, Postion = "b66" }));
            tempRedX.Add(new KeyValuePair<string, FigureX>("r4", new FigureX() { Name = "r4", NumPosition = 67, Postion = "b67" }));

            tempBlackX.Add(new KeyValuePair<string, FigureX>("b1", new FigureX() { Name = "b1", NumPosition = 68, Postion = "b68" }));
            tempBlackX.Add(new KeyValuePair<string, FigureX>("b2", new FigureX() { Name = "b2", NumPosition = 69, Postion = "b69" }));
            tempBlackX.Add(new KeyValuePair<string, FigureX>("b3", new FigureX() { Name = "b3", NumPosition = 70, Postion = "b70" }));
            tempBlackX.Add(new KeyValuePair<string, FigureX>("b4", new FigureX() { Name = "b4", NumPosition = 71, Postion = "b71" }));

        }
        protected void SetSelectedFigure()
        {
            Control activeControl = this.ActiveControl;
            Button btn = new Button();
            if (activeControl != null && activeControl is Button)
            {
                btn = activeControl as Button;
                SelectedFigure = btn.Name;
            }
        }
        protected override bool ProcessTabKey(bool forward)
        {
            Control activeControl = this.ActiveControl;
            Button btn = new Button();
            if (activeControl != null && activeControl is Button)
            {
                btn = activeControl as Button;
                SelectedFigure = btn.Name;
            }

            return base.ProcessTabKey(forward);
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // ako je space i selected button true -- onda idi dalje u suprotnom čekaj i ponavljaj
            if (waitingForSpace && keyData == Keys.Space)
            {
                waitingForSpace = false;
                Button btnActiveControl = ActiveControl as Button;
                if (btnActiveControl != null)
                {
                    Logic();
                }
                return true;
            }

            if (keyData == Keys.Enter && !waitingForSpace)
            {
                RandomImage();
                if (Number == 6)
                {
                    SetButtonColor(true); waitingForSpace = true;
                    SetSelectedFigure();
                    // LogicFor6();
                    LogicFor7();
                }
                else
                {
                    SetButtonColor(false);
                    Logic();
                }
                return true;
            }

            if (keyData == Keys.Escape)
            {
                Close();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
        /*
        public bool IsSameFigureAlreadyOnPosition(string figureColor) {
            if (SelectedFigure == null) {
                switch (figureColor) {
                    case FigureStartPostion.Yellow: { } break;
                    case FigureStartPostion.Green: { } break;
                    case FigureStartPostion.Red: { } break;
                    case FigureStartPostion.Black: { } break;
                }
            }
            return false;
        }
        */
        public int Counters { get; set; }
        public string SelectedFigure { get; set; }
        public void Logic()
        {
            List<KeyValuePair<string, FigureX>> figures = new List<KeyValuePair<string, FigureX>>();
            figures = SelectFigure();
        }

        public void LogicFor7()
        {
            List<KeyValuePair<string, FigureX>> figures = new List<KeyValuePair<string, FigureX>>();
            figures = SelectFigure();
            if (waitingForSpace && SelectedFigure != null)
            {
                MoveFromToStartPosition(new Button() { Name = SelectedFigure });
            }
        }
        public void LogicFor6()
        {
            List<KeyValuePair<string, FigureX>> figures = new List<KeyValuePair<string, FigureX>>();
            figures = SelectFigure();
            MessageBox.Show(SelectedFigure); 
            if (waitingForSpace && SelectedFigure != null)
            {
                int fromPosition = 0;
                int toPosition = 0;
                string name = SelectedFigure.Remove(0, 1);
                int numOfFiguresInGame = FiguresInGame(figures);
                int.TryParse(name, out fromPosition);
                bool alreadyOnPosition = false;
                int.TryParse(FigureStartPostion.Yellow.Remove(0, 1), out toPosition);
                if (numOfFiguresInGame == 1)
                {
                    toPosition = fromPosition + Number;
                    MoveFromTo(fromPosition, toPosition);
                }
                if (toPosition == 0 && numOfFiguresInGame == 0)
                {
                    if (alreadyOnPosition)
                    {
                        StateOfGame = Fall.StateOfGame.SameColorFigureAlreadyOnPosition;
                    }
                    else if (!alreadyOnPosition) {
                        MoveFromTo(fromPosition, toPosition);
                        numOfFiguresInGame++;
                    }
                    return;
                }
                if (fromPosition != 0 && toPosition != 0)
                {
                    if (figures[0].Value.Name.Contains("y"))
                    {

                        if (numOfFiguresInGame == 0 && toPosition != 0)
                        {
                            MoveFromTo(fromPosition, toPosition);
                        }
                        else if (numOfFiguresInGame == 1)
                        {
                            MoveFromTo(fromPosition, toPosition);
                        }
                        else if (numOfFiguresInGame == 2)
                        {
                            MoveFromTo(fromPosition, toPosition);
                        }
                        else if (numOfFiguresInGame == 3)
                        {
                            MoveFromTo(fromPosition, toPosition);
                        }
                    }
                }
            }
        }

        public int FiguresInGame(List<KeyValuePair<string, FigureX>> selectedFigures)
        {
            int cnt = 0;
            foreach (KeyValuePair<string, FigureX> f in selectedFigures)
            {
                if (f.Value.InGame) cnt++;
            }
            return cnt;
        }
        public List<KeyValuePair<string, FigureX>> SelectFigure()
        {
            List<string> lstSelected = new List<string>();
            List<KeyValuePair<string, FigureX>> lstFigure = new List<KeyValuePair<string, FigureX>>();
            if (b73.BackColor == SystemColors.ControlDark)
            {
                foreach (KeyValuePair<string, FigureX> item in tempYellowX)
                {
                    lstSelected.Add(item.Value.Postion);
                    lstFigure.Add(item);
                }
            }
            else if (b74.BackColor == SystemColors.ControlDark)
            {
                foreach (KeyValuePair<string, FigureX> item in tempGreenX)
                {
                    lstSelected.Add(item.Value.Postion);
                    lstFigure.Add(item);
                }
            }
            else if (b75.BackColor == SystemColors.ControlDark)
            {
                foreach (KeyValuePair<string, FigureX> item in tempRedX)
                {
                    lstSelected.Add(item.Value.Postion);
                    lstFigure.Add(item);
                }
            }
            else if (b76.BackColor == SystemColors.ControlDark)
            {
                foreach (KeyValuePair<string, FigureX> item in tempBlackX)
                {
                    lstSelected.Add(item.Value.Postion);
                    lstFigure.Add(item);
                }
            }
            DisableOther(lstSelected);
            return lstFigure;
        }
        public void DisableOther(List<string> keepButtons)
        {
            for (int i = 0; i < 72; i++)
            {
                string name = "b" + i.ToString();
                Button btn = GetControlByName(name) as Button;
                btn.Enabled = false;
                btn.TabStop = false;

                foreach (string btnName in keepButtons)
                {
                    if (btnName == name)
                    {
                        btn.Enabled = true;
                        btn.TabStop = true;
                    }
                }
            }
        }

        public void UpdateFigurePosition(string figure, int position) {
            foreach (KeyValuePair<string, FigureX> item in tempYellowX)
            {
                if (item.Value.Name == "y1") { tempYellowX[0].Value.NumPosition = position; }
            }
        }
        public void MoveFromToStartPosition(Button btn) {
            Button startButton = null;
            Button endButton = null;
            // Move Yellow
            if (btn.Name == "y1") 
            {
                if (tempYellowX[1].Value.NumPosition != 0 && tempYellowX[2].Value.NumPosition != 0 && tempYellowX[3].Value.NumPosition != 0) {
                    tempYellowX[0].Value.NumPosition = 0;
                    tempYellowX[0].Value.InGame = true;
                    startButton = GetControlByName("b56") as Button;                    
                    endButton = GetControlByName("b0") as Button;
                }
            }
            else if (btn.Name == "y2") 
            {
                if (tempYellowX[0].Value.NumPosition != 0 && tempYellowX[2].Value.NumPosition != 0 && tempYellowX[3].Value.NumPosition != 0)
                {
                    tempYellowX[1].Value.NumPosition = 0;
                    tempYellowX[1].Value.InGame = true;
                    startButton = GetControlByName("b57") as Button;
                    endButton = GetControlByName("b0") as Button;
                }
            }
            else if (btn.Name == "y3") 
            {
                if (tempYellowX[0].Value.NumPosition != 0 && tempYellowX[1].Value.NumPosition != 0 && tempYellowX[3].Value.NumPosition != 0)
                {
                    tempYellowX[2].Value.NumPosition = 0;
                    tempYellowX[2].Value.InGame = true;
                    startButton = GetControlByName("b58") as Button;
                    endButton = GetControlByName("b0") as Button;
                }
            }
            else if (btn.Name == "y4") {
                if (tempYellowX[0].Value.NumPosition != 0 && tempYellowX[1].Value.NumPosition != 0 && tempYellowX[1].Value.NumPosition != 0)
                {
                    tempYellowX[3].Value.NumPosition = 0;
                    tempYellowX[3].Value.InGame = true;
                    startButton = GetControlByName("b59") as Button;
                    endButton = GetControlByName("b0") as Button;
                }
            }
            // Move Green
            else if (btn.Name == "g1")
            {
                if (tempGreenX[1].Value.NumPosition != 0 && tempGreenX[2].Value.NumPosition != 0 && tempGreenX[3].Value.NumPosition != 0)
                {
                    tempGreenX[0].Value.NumPosition = 0;
                    tempGreenX[0].Value.InGame = true;
                    startButton = GetControlByName("b60") as Button;
                    endButton = GetControlByName("b10") as Button;
                }
            }
            else if (btn.Name == "g2")
            {
                if (tempGreenX[0].Value.NumPosition != 0 && tempGreenX[2].Value.NumPosition != 0 && tempGreenX[3].Value.NumPosition != 0)
                {
                    tempGreenX[1].Value.NumPosition = 0;
                    tempGreenX[1].Value.InGame = true;
                    startButton = GetControlByName("b61") as Button;
                    endButton = GetControlByName("b10") as Button;
                }
            }
            else if (btn.Name == "g3")
            {
                if (tempGreenX[0].Value.NumPosition != 0 && tempGreenX[1].Value.NumPosition != 0 && tempGreenX[3].Value.NumPosition != 0)
                {
                    tempGreenX[2].Value.NumPosition = 0;
                    tempGreenX[2].Value.InGame = true;
                    startButton = GetControlByName("b62") as Button;
                    endButton = GetControlByName("b10") as Button;
                }
            }
            else if (btn.Name == "g4")
            {
                if (tempGreenX[0].Value.NumPosition != 0 && tempGreenX[1].Value.NumPosition != 0 && tempGreenX[1].Value.NumPosition != 0)
                {
                    tempGreenX[3].Value.NumPosition = 0;
                    tempGreenX[3].Value.InGame = true;
                    startButton = GetControlByName("b63") as Button;
                    endButton = GetControlByName("b10") as Button;
                }
            }
            // Move Red
            else if (btn.Name == "r1")
            {
                if (tempRedX[1].Value.NumPosition != 0 && tempRedX[2].Value.NumPosition != 0 && tempRedX[3].Value.NumPosition != 0)
                {
                    tempRedX[0].Value.NumPosition = 0;
                    tempRedX[0].Value.InGame = true;
                    startButton = GetControlByName("b64") as Button;
                    endButton = GetControlByName("b20") as Button;
                }
            }
            else if (btn.Name == "r2")
            {
                if (tempRedX[0].Value.NumPosition != 0 && tempRedX[2].Value.NumPosition != 0 && tempRedX[3].Value.NumPosition != 0)
                {
                    tempRedX[1].Value.NumPosition = 0;
                    tempRedX[1].Value.InGame = true;
                    startButton = GetControlByName("b65") as Button;
                    endButton = GetControlByName("b20") as Button;
                }
            }
            else if (btn.Name == "r3")
            {
                if (tempRedX[0].Value.NumPosition != 0 && tempRedX[1].Value.NumPosition != 0 && tempRedX[3].Value.NumPosition != 0)
                {
                    tempRedX[2].Value.NumPosition = 0;
                    tempRedX[2].Value.InGame = true;
                    startButton = GetControlByName("b66") as Button;
                    endButton = GetControlByName("b20") as Button;
                }
            }
            else if (btn.Name == "r4")
            {
                if (tempRedX[0].Value.NumPosition != 0 && tempRedX[1].Value.NumPosition != 0 && tempRedX[1].Value.NumPosition != 0)
                {
                    tempRedX[3].Value.NumPosition = 0;
                    tempRedX[3].Value.InGame = true;
                    startButton = GetControlByName("b67") as Button;
                    endButton = GetControlByName("b20") as Button;
                }
            }
            // Move Black
            else if (btn.Name == "b1")
            {
                if (tempBlackX[1].Value.NumPosition != 0 && tempBlackX[2].Value.NumPosition != 0 && tempBlackX[3].Value.NumPosition != 0)
                {
                    tempBlackX[0].Value.NumPosition = 0;
                    tempBlackX[0].Value.InGame = true;
                    startButton = GetControlByName("b68") as Button;
                    endButton = GetControlByName("b30") as Button;
                }
            }
            else if (btn.Name == "b2")
            {
                if (tempBlackX[0].Value.NumPosition != 0 && tempBlackX[2].Value.NumPosition != 0 && tempBlackX[3].Value.NumPosition != 0)
                {
                    tempBlackX[1].Value.NumPosition = 0;
                    tempBlackX[1].Value.InGame = true;
                    startButton = GetControlByName("b69") as Button;
                    endButton = GetControlByName("b30") as Button;
                }
            }
            else if (btn.Name == "b3")
            {
                if (tempBlackX[0].Value.NumPosition != 0 && tempBlackX[1].Value.NumPosition != 0 && tempBlackX[3].Value.NumPosition != 0)
                {
                    tempBlackX[2].Value.NumPosition = 0;
                    tempBlackX[2].Value.InGame = true;
                    startButton = GetControlByName("b70") as Button;
                    endButton = GetControlByName("b30") as Button;
                }
            }
            else if (btn.Name == "b4")
            {
                if (tempBlackX[0].Value.NumPosition != 0 && tempBlackX[1].Value.NumPosition != 0 && tempBlackX[1].Value.NumPosition != 0)
                {
                    tempBlackX[3].Value.NumPosition = 0;
                    tempBlackX[3].Value.InGame = true;
                    startButton = GetControlByName("b71") as Button;
                    endButton = GetControlByName("b30") as Button;
                }
            }
            if (startButton != null && endButton != null && startButton.Image != null) {
                endButton.Image = startButton.Image;
                startButton.Image = null;
            }
        }
        public void MoveFromTo(int startPosition, int endPosition, bool enableButtons = true)
        {
            for (int i = 0; i < 72; i++)
            {
                if (i == startPosition)
                {
                    Button btnStart = GetControlByName("b" + i.ToString()) as Button;
                    if (btnStart != null)
                    {
                        // UpdateFigurePosition(1,1);
                        Button btnEnd = GetControlByName("b" + endPosition.ToString()) as Button;
                        if (btnEnd != null)
                        {
                            btnEnd.Image = btnStart.Image;
                            btnStart.Image = null;
                        }
                    }
                }
            }
        }
        public Control GetControlByName(string Name)
        {
            foreach (Control c in this.Controls)
                if (c.Name == Name)
                    return c;

            return null;
        }
        public void SetButtonColor(bool buttonFocus)
        {
            if (buttonFocus) return;
            else if (b73.BackColor == SystemColors.ControlDark && !buttonFocus)
            {
                b73.BackColor = SystemColors.GradientActiveCaption;
                b74.BackColor = SystemColors.ControlDark;
            }
            else if (b74.BackColor == SystemColors.ControlDark && !buttonFocus)
            {
                b74.BackColor = SystemColors.GradientActiveCaption;
                b75.BackColor = SystemColors.ControlDark;
            }
            else if (b75.BackColor == SystemColors.ControlDark && !buttonFocus)
            {
                b75.BackColor = SystemColors.GradientActiveCaption;
                b76.BackColor = SystemColors.ControlDark;
            }
            else if (b76.BackColor == SystemColors.ControlDark && !buttonFocus)
            {
                b76.BackColor = SystemColors.GradientActiveCaption;
                b73.BackColor = SystemColors.ControlDark;
            }
        }
        private bool waitingForSpace = false;
        public void RandomImage()
        {
            int index = random.Next(imgList.Count);
            // rrm Get 6 multiple times -> index = 5;
            string selectedImage = imgList[index];
            Number = index + 1;
            b72.Image = Image.FromFile(selectedImage);
        }
        public void LoadImage()
        {
            BaseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            ProjectRoot = Directory.GetParent(BaseDirectory).Parent.Parent.FullName;
            ResourcePath = Path.Combine(ProjectRoot, "resources");
            imgList = new List<string>
            {
                Path.Combine(ResourcePath, "1.png"),
                Path.Combine(ResourcePath, "2.png"),
                Path.Combine(ResourcePath, "3.png"),
                Path.Combine(ResourcePath, "4.png"),
                Path.Combine(ResourcePath, "5.png"),
                Path.Combine(ResourcePath, "6.png")
            };
            imgListFigure = new List<string> {
                Path.Combine(ResourcePath, "Yellow.png"),
                Path.Combine(ResourcePath, "Green.png"),
                Path.Combine(ResourcePath, "Red.png"),
                Path.Combine(ResourcePath, "Black.png")
            };
        }
        public void PutBorderEvent()
        {
            foreach (Control control in Controls)
            {
                if (control.Name == "b0")
                {
                    b0.Click += B0_Click;
                }
            }
        }
        private void B0_Click(object sender, EventArgs e)
        {
            // string x = ((Button)sender).Name;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            SelectFigure();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 72; i++)
            {
                Button btn = GetControlByName("b" + i.ToString()) as Button;
                if (btn != null)
                {
                    btn.Enabled = true;
                }
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {

        }
        public void ShortWriteOut()
        {
            List<string> lstName = new List<string>();
            for (int i = 0; i < 72; i++)
            {
                string name = "b" + i.ToString() + ".Enabled = true;";
                lstName.Add(name);
            }
            File.WriteAllLines("dinova.txt", lstName);
        }

        int counters = 0;
        private void t_Tick(object sender, EventArgs e)
        {
            int from = 0;
            int to = 0;
            int.TryParse(rf.Text.Remove(0, 1).ToString(), out from);
            int.TryParse(rt.Text.Remove(0,1).ToString(), out to);

            MoveFromTo(from, to);

            t.Stop(); // Stop the timer once the counter reaches 10
        }
        private void bM_Click(object sender, EventArgs e)
        {
            t.Start();
            int toPosition = 0;
            int.TryParse(rt.Text.Remove(0,1), out toPosition);
            CheckMovement(toPosition);
        }

        private void mv_Click(object sender, EventArgs e)
        {
            MoveFromToStartPosition(new Button() { Name = rn.Text });
        }
    }

    static class FigureStartPostion
    {
        public static string Yellow { get { return "b0"; } }
        public static string Green { get { return "b10"; } }
        public static string Red { get { return "b20"; } }
        public static string Black { get { return "b30"; } }

    }

    static class StateOfGame
    {
        public static string Status { get; set; }
        public static string Start { get; set; }
        public static string End { get; set; }
        public static string ErrorsGoToPostionIsZero { get { return Errors.GoToPostionIsZero; } }
        public static string SameColorFigureAlreadyOnPosition { get { return Errors.SameColorFigureAlreadyOnPosition; } }
    }

    static class Errors
    {
        public static string GoToPostionIsZero { get; set; }
        public static string SameColorFigureAlreadyOnPosition { get; set; }
    }
}
