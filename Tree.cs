using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Security.Policy;
using System.Windows.Forms;

namespace Fall
{
    public partial class Tree : Form
    {
        private List<string> imgList = new List<string> { "1.png", "2.png", "3.png", "4.png", "5.png", "6.png" };
        private Random random = new Random();
        public int Number { get; set; }
        string BaseDirectory { get; set; }
        string ProjectRoot { get; set; }
        string ResourcePath { get; set; }
        public bool Repeat { get; set; }
        public bool VeryFirstMove { get; set; }
        string StateOfGame { get; set; }

        public Tree()
        {
            InitializeComponent();
            VeryFirstMove = true;
            LoadImage();
            PutBorderEvent();
            LoadGraphic();
            FillPlayers();
        }
        public void LoadGraphic()
        {
            b73.BackColor = SystemColors.ControlDark;
        }

        List<KeyValuePair<string, FigureX>> tempYellowX = new List<KeyValuePair<string, FigureX>>();
        List<KeyValuePair<string, FigureX>> tempGreenX = new List<KeyValuePair<string, FigureX>>();
        List<KeyValuePair<string, FigureX>> tempRedX = new List<KeyValuePair<string, FigureX>>();
        List<KeyValuePair<string, FigureX>> tempBlackX = new List<KeyValuePair<string, FigureX>>();

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
                    LogicFor6();
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
        public int Counters { get; set; }
        public string SelectedFigure { get; set; }
        public void Logic()
        {
            List<KeyValuePair<string, FigureX>> figures = new List<KeyValuePair<string, FigureX>>();
            figures = SelectFigure();
        }
        public void LogicFor6()
        {
            List<KeyValuePair<string, FigureX>> figures = new List<KeyValuePair<string, FigureX>>();
            figures = SelectFigure();
            if (waitingForSpace && SelectedFigure != null)
            {
                int fromPosition = 0;
                int toPosition = 0;
                string name = SelectedFigure.Remove(0, 1);
                int numOfFiguresInGame = FiguresInGame(figures);
                int.TryParse(name, out fromPosition);
                int.TryParse(FigureStartPostion.Yellow.Remove(0, 1), out toPosition);
                if (toPosition == 0)
                {
                    StateOfGame = Fall.StateOfGame.ErrorsGoToPostionIsZero;
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
        public void MoveFromTo(int startPosition, int endPosition)
        {
            // search over whole position - when you find start - you know which figure is -- select those figure and change it value and show 
            // on appropriate postion - remove old one
            for (int i = 0; i < 72; i++)
            {
                if (i == startPosition)
                {
                    Button btnStart = GetControlByName("b" + i.ToString()) as Button;
                    if (btnStart != null)
                    {
                        Button btnEnd = GetControlByName("b" + i.ToString()) as Button;
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
    }

    static class Errors
    {
        public static string GoToPostionIsZero { get; set; }
    }
}
