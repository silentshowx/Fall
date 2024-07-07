using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml.Serialization;
using static Fall.News;

namespace Fall
{
    public partial class Tree : Form
    {
        private List<string> imgList = new List<string> { "1.png", "2.png", "3.png", "4.png", "5.png", "6.png" };
        private Random random = new Random();
        public int Number { get; set; }
        string baseDirectory { get; set; }
        string projectRoot { get; set; }
        string resourcesPath { get; set; }
        public bool Repeat { get; set; }
        public bool VeryFirstMove { get; set; }

        public Tree()
        {
            InitializeComponent();
            VeryFirstMove = true;
            // WaitForInput = true;
            LoadImage();
            PutBorderEvent();
            LoadGraphic();
            FillPlayers();
        }
        public void LoadGraphic() {
            b73.FlatStyle = FlatStyle.Popup;
            b73.BackColor = SystemColors.ControlDark;
        }

        List<KeyValuePair<string, FigureX>> tempYellowX = new List<KeyValuePair<string, FigureX>>();
        List<KeyValuePair<string, FigureX>> tempGreenX = new List<KeyValuePair<string, FigureX>>();
        List<KeyValuePair<string, FigureX>> tempRedX = new List<KeyValuePair<string, FigureX>>();
        List<KeyValuePair<string, FigureX>> tempBlackX = new List<KeyValuePair<string, FigureX>>();

        public void FillPlayers() {

            tempYellowX.Add(new KeyValuePair<string, FigureX>("y1", new FigureX() { Name = "y1", NumPosition = 56, Postion = "b56" }));
            tempYellowX.Add(new KeyValuePair<string, FigureX>("y2", new FigureX() { Name = "y2", NumPosition = 57, Postion = "b57" }));
            tempYellowX.Add(new KeyValuePair<string, FigureX>("y3", new FigureX() { Name = "y3", NumPosition = 58, Postion = "b58" }));
            tempYellowX.Add(new KeyValuePair<string, FigureX>("y4", new FigureX() { Name = "y4", NumPosition = 59, Postion = "b59" }));

            tempGreenX.Add(new KeyValuePair<string, FigureX>("g1", new FigureX() { Name = "g1", NumPosition = 60, Postion = "b60" }));
            tempGreenX.Add(new KeyValuePair<string, FigureX>("g1", new FigureX() { Name = "g2", NumPosition = 61, Postion = "b61" }));
            tempGreenX.Add(new KeyValuePair<string, FigureX>("g1", new FigureX() { Name = "g3", NumPosition = 62, Postion = "b62" }));
            tempGreenX.Add(new KeyValuePair<string, FigureX>("g1", new FigureX() { Name = "g4", NumPosition = 63, Postion = "b63" }));

            tempRedX.Add(new KeyValuePair<string, FigureX>("y1", new FigureX() { Name = "r1", NumPosition = 64, Postion = "b64" }));
            tempRedX.Add(new KeyValuePair<string, FigureX>("y1", new FigureX() { Name = "r2", NumPosition = 65, Postion = "b65" }));
            tempRedX.Add(new KeyValuePair<string, FigureX>("y1", new FigureX() { Name = "r3", NumPosition = 66, Postion = "b66" }));
            tempRedX.Add(new KeyValuePair<string, FigureX>("y1", new FigureX() { Name = "r4", NumPosition = 67, Postion = "b67" }));

            tempBlackX.Add(new KeyValuePair<string, FigureX>("y1", new FigureX() { Name = "b1", NumPosition = 68, Postion = "b68" }));
            tempBlackX.Add(new KeyValuePair<string, FigureX>("y1", new FigureX() { Name = "b2", NumPosition = 69, Postion = "b69" }));
            tempBlackX.Add(new KeyValuePair<string, FigureX>("y1", new FigureX() { Name = "b3", NumPosition = 70, Postion = "b70" }));
            tempBlackX.Add(new KeyValuePair<string, FigureX>("y1", new FigureX() { Name = "b4", NumPosition = 71, Postion = "b71" }));

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
            if (SelectedButton && keyData == Keys.Space) {
                SelectedButton = false;
            }
            if (waitingForSpace && keyData == Keys.Space)
            {
                waitingForSpace = false;
                WaitForInput = false;
                return true;
            }

            if (keyData == Keys.Enter)
            {
                RandomImage();
                SetButtonStyle();
                Logic();
                if (WaitForInput)
                {
                    waitingForSpace = true;
                    return true;
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
        public bool WaitForInput { get; set; }
        public bool SelectedButton { get; set; }
        public string SelectedFigure { get; set; }
        public void Logic()
        {
            List<KeyValuePair<string, FigureX>> figures = new List<KeyValuePair<string, FigureX>>();
            if (Number == 6) {
                // ako niti jedan nije vani izvadi prvoga žutoga
                figures = SelectFigure();
                if (WaitForInput) { WaitForInput = false; return; }
                else if (!WaitForInput && !SelectedButton) {
                    // go to selectButton --- selectedButtonIs // -- wait for space to get value from selectedButton
                    SelectedButton = true; return;
                }
                else if (!WaitForInput && SelectedButton && SelectedFigure != String.Empty)
                {
                    WaitForInput = true;

                    // ako su figure InGame onda Move nako što si odabrao jednu od ponuđenih
                    foreach (var item in figures)
                    {
                        if (item.Value.InGame)
                        {

                        }
                    }

                    foreach (var item in figures)
                    {
                        if (item.Value.Name == "b" + item.Value.Postion)
                        {

                        }
                    }

                    MoveFromTo(56, 0);
                    WaitForInput = false;
                }
            }
            if (Number < 6)
            {
                /*
                SelectFigure();
                if (Counters == 0) MoveFromTo(1, 5);
                else if (Counters == 1) MoveFromTo(5, 7);
                else if (Counters == 2) MoveFromTo(7, 8);
                else if (Counters == 3) MoveFromTo(8, 11);
                else if (Counters == 4) MoveFromTo(11, 15);
                else if (Counters == 5) MoveFromTo(15, 17);
                Counters++;
                */
            }
        }
        public List<KeyValuePair<string, FigureX>> SelectFigure()
        {
            List<string> lstSelected = new List<string>();
            List<KeyValuePair<string, FigureX>> lstFigure = new List<KeyValuePair<string, FigureX>>();
            if (b73.FlatStyle == FlatStyle.Popup)
            {
                b73.FlatStyle = FlatStyle.Standard;
                foreach (KeyValuePair<string, FigureX> item in tempYellowX)
                {
                    lstSelected.Add(item.Value.Postion);
                    lstFigure.Add(item);
                }
            }
            else if (b74.FlatStyle == FlatStyle.Popup)
            {
                b73.FlatStyle = FlatStyle.Standard;
                foreach (KeyValuePair<string, FigureX> item in tempGreenX)
                {
                    lstSelected.Add(item.Value.Postion);
                    lstFigure.Add(item);
                }
            }
            else if (b75.FlatStyle == FlatStyle.Popup)
            {
                b73.FlatStyle = FlatStyle.Standard;
                foreach (KeyValuePair<string, FigureX> item in tempRedX)
                {
                    lstSelected.Add(item.Value.Postion);
                    lstFigure.Add(item);
                }
            }
            else if (b76.FlatStyle == FlatStyle.Popup)
            {
                b73.FlatStyle = FlatStyle.Standard;
                foreach (KeyValuePair<string, FigureX> item in tempBlackX)
                {
                    lstSelected.Add(item.Value.Postion);
                    lstFigure.Add(item);
                }
            }
            DisableOther(lstSelected);
            return lstFigure;
        }
        public void DisableOther(List<string> keepButtons) {            
            for (int i = 0; i < 72; i++) {
                string name = "b" + i.ToString();
                Button btn = GetControlByName(name) as Button;
                btn.Enabled = false;
                btn.TabStop = false;

                foreach (string btnName in keepButtons)
                {
                    if (btnName == name) {
                        btn.Enabled = true;
                        btn.TabStop = true;
                    }
                }
            }
        }
        public void MoveFromTo(int startPosition, int endPosition) {
            // search over whole position - when you find start - you know which figure is -- select those figure and change it value and show 
            // on appropriate postion - remove old one
            for (int i = 0; i < 72; i++) {
                if (i == startPosition) {
                    Button btnStart = GetControlByName("b" + i.ToString()) as Button;
                    if (btnStart != null)
                    {
                        Button btnEnd = GetControlByName("b" + i.ToString()) as Button;
                        if (btnEnd != null) {
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
        public void SetButtonStyle()
        {
            if (b73.FlatStyle == FlatStyle.Popup)
            {
                b73.FlatStyle = FlatStyle.Standard;
                b73.BackColor = SystemColors.GradientActiveCaption;
                b74.FlatStyle = FlatStyle.Popup;
                b74.BackColor = SystemColors.ControlDark;
            }
            else if (b74.FlatStyle == FlatStyle.Popup)
            {
                b74.FlatStyle = FlatStyle.Standard;
                b74.BackColor = SystemColors.GradientActiveCaption;
                b75.FlatStyle = FlatStyle.Popup;
                b75.BackColor = SystemColors.ControlDark;
            }
            else if (b75.FlatStyle == FlatStyle.Popup)
            {
                b75.FlatStyle = FlatStyle.Standard;
                b75.BackColor = SystemColors.GradientActiveCaption;
                b76.FlatStyle = FlatStyle.Popup;
                b76.BackColor = SystemColors.ControlDark;
            }
            else if (b76.FlatStyle == FlatStyle.Popup)
            {
                b76.FlatStyle = FlatStyle.Standard;
                b76.BackColor = SystemColors.GradientActiveCaption;
                b73.FlatStyle = FlatStyle.Popup;
                b73.BackColor = SystemColors.ControlDark;
            }
        }
        private bool waitingForSpace = false;
        public void RandomImage() {
            int index = random.Next(imgList.Count);
            // rrm Get 6 multiple times -> index = 5;
            string selectedImage = imgList[index];
            Number = index + 1;
            b72.Image = Image.FromFile(selectedImage);
        }
        public void LoadImage() {
            baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            projectRoot = Directory.GetParent(baseDirectory).Parent.Parent.FullName;
            resourcesPath = Path.Combine(projectRoot, "resources");
            imgList = new List<string>
            {
                Path.Combine(resourcesPath, "1.png"),
                Path.Combine(resourcesPath, "2.png"),
                Path.Combine(resourcesPath, "3.png"),
                Path.Combine(resourcesPath, "4.png"),
                Path.Combine(resourcesPath, "5.png"),
                Path.Combine(resourcesPath, "6.png")
            };
        }
        public void PutBorderEvent() {
            foreach (Control control in Controls)
            {
                if (control.Name == "b0") {
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
        public void ShortWriteOut() {
            List<string> lstName = new List<string>();
            for (int i = 0; i < 72; i++)
            {
                string name = "b" + i.ToString() + ".Enabled = true;";
                lstName.Add(name);
            }
            File.WriteAllLines("dinova.txt", lstName);
        }
    }
}
