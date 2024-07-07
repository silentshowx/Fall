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
            LoadEvents();
            LoadGraphic();
            FillPlayers();
        }
        public void LoadGraphic() {
            b73.FlatStyle = FlatStyle.Popup;
            b73.BackColor = SystemColors.ControlDark;
        }

        List<KeyValuePair<string, string>> tempYellow = new List<KeyValuePair<string, string>>();

        List<KeyValuePair<string, FigureX>> tempYellowX = new List<KeyValuePair<string, FigureX>>();
        List<KeyValuePair<string, FigureX>> tempGreenX = new List<KeyValuePair<string, FigureX>>();
        List<KeyValuePair<string, FigureX>> tempRedX = new List<KeyValuePair<string, FigureX>>();
        List<KeyValuePair<string, FigureX>> tempBlackX = new List<KeyValuePair<string, FigureX>>();

        public void FillPlayers() {
            // tempYellow.Add(new KeyValuePair<string, string>("p56", "y1"));
            // tempYellow.Add(new KeyValuePair<string, string>("p57", "y2"));
            // tempYellow.Add(new KeyValuePair<string, string>("p58", "y3"));
            // tempYellow.Add(new KeyValuePair<string, string>("p59", "y4"));

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
        
        public int Counters { get; set; }
        public bool WaitForInput { get; set; }
        public bool SelectedButton { get; set; }
        public void MeLogicX()
        {
            List<KeyValuePair<string, FigureX>> figures = new List<KeyValuePair<string, FigureX>>();
            if (Number == 6) {
                // ako niti jedan nije vani izvadi prvoga žutoga
                figures = SelectFigure();
                if (WaitForInput) { WaitForInput = false; return; }
                else if (!WaitForInput && SelectedButton)
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
        public void MeLogic(List<KeyValuePair<string, FigureX>> lstFigureX) {
            foreach (KeyValuePair<string, FigureX> figure in lstFigureX)
            {
                if (Number < 6) {
                    SelectFigure();
                    MoveFromTo(1, 5);
                }
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

        public void Logic() {
            // izvadi igrača ako je broj 6 // odaberi željeni ring i dalje radi logiku
            if (Number == 6) {
                if (b73.FlatStyle == FlatStyle.Popup)
                {
                    // iz odabrane grupe vuci poteze
                    // ako su svi u kućici izvuci prvoga
                    // rrm -- za sada je najvažnije da dobiješ gibanje jednoga - znači radiš samo sa jednom figurom - koju isčitavaš
                    foreach (var yellow in tempYellowX)
                    {
                        if (yellow.Key == "y1" && yellow.Value.Postion == "b0") { }
                    }
                }
                FigureX f = new FigureX() {  };
                string btnName = "b5";
                Button newButton = GetControlByName(btnName) as Button;
                newButton.Image = Image.FromFile("");

                // čekaj za input sa kojim igračem hoće igrati user 1,2,3,4

                // 1. ako su svi u rupi počni igrati automatski sa prvim igračem
                // 2. ako je jedan vani i nije na nuli pitaj sa kojim igračem želi igrati žuti
                // 2.1 ako je prvi igrač vani na nuli i to je isto kolo onda počmi sa njima dalje igrati u sljedećem potezu


                // ako je broj 6 onda pozovi figuru "y1" i definiraj joj na koji button ide
                // zato je potrebna metoda koja indexira figuru od postojećega polja
                // metoda 
            }
        }
        // figura y1 ili neka druga prikaži se na željenoj poziciji
        // metoda ShowOnPosition(Button btn){ btn.Image = this.Picture - slika od figure }
        // klasa Figura treba imati i sliku od figure da bi implementirao ShowOnPosition metodu
        /*
            ShowOnPosition( - ubaci parametar Number) i ova metoda automatski isčita trenutno polje i prikazuje za uvećanu vrijednost na drugoj poziciji i briše se 
            usage: figure.ShowOnPosition(Number);
            ShowOnPosition(int number){ 
                NumPosition = NumPosition + Number; // NumPosition = 2 + Number = 3  .. endPosition = 5 -- pronađi "b5"
                // pronađi button koji ima tu poziciju
                    // za svaki button koji postoji isčitaj ime i ako je jednako onome gore pod njegov image postavi "image figure - picture"
                string btnName = "b" + NumPosition.ToString();
                foreach (Control control in this.Controls){ 
                    if (control is Button btn && btn.Name == btnName)
                    {
                        // prikaži na novoj poziciji buttona - image od Figure
                        btn.Image = "nova verzija" // ovo treba provjeriti da se vidi dali to radi 
                    }
                    }
            }
         */

        public Control GetControlByName(string Name)
        {
            foreach (Control c in this.Controls)
                if (c.Name == Name)
                    return c;

            return null;
        }

        public void TempGameLogic() {
            if (Repeat)
            {
                FigureX firstFigure = new FigureX() { Name = "y1" };
                firstFigure = tempYellowX[0].Value;

                // set second position
                FigureX secondFigure = new FigureX();
                secondFigure.NumPosition = firstFigure.NumPosition + Number;
                secondFigure.Postion = "b" + secondFigure.NumPosition.ToString(); 

                // get name of first position 
                Button firstButton = new Button();
                foreach (Button btn in this.Controls) {
                    if (btn.Name == secondFigure.Postion) {
                        // fill information in button to new postion
                        firstButton = btn;
                    }
                }
                // move to another position
                foreach (Button btn in this.Controls) 
                {
                    if (btn.Name == firstFigure.Postion) {
                        firstButton.Image = btn.Image;
                    }
                }
                Repeat = false;
            }
            if (b73.FlatStyle == FlatStyle.Popup) { 
                if (Number == 6) {
                    if (tempYellowX[0].Value.Postion == "b56")
                    {
                        b0.Image = b56.Image;
                        b56.Image = null;
                        tempYellowX[0] = new KeyValuePair<string, FigureX>("y1", new FigureX() { Name = "y1", NumPosition = 0, Postion = "b0" });
                        Repeat = true;
                    }
                    // if is "y1" already out and not on 0 position put "y2" on 0 positino
                    else if (tempYellowX[0].Value.NumPosition > 0) {
                        // check for other figure to Move out
                        if (tempYellowX[1].Value.Postion == "b57")
                        {
                            // Move "y2" to position 0
                            b0.Image = b57.Image;
                            b57.Image = null;
                            tempYellowX[1] = new KeyValuePair<string, FigureX>("y2", new FigureX() { Name = "y2", NumPosition = 0, Postion = "b0" });
                        }
                    }
                }
            }
        }
        private CustomPictureBox FindPictureBoxByPosition(int position)
        {
            foreach (Control control in this.Controls)
            {
                if (control is CustomPictureBox pictureBox && pictureBox.Position == position)
                {
                    return pictureBox;
                }
            }
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
        public void LoadEvents() {

        }
        private bool waitingForSpace = false;

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (waitingForSpace && keyData == Keys.Space)
            {
                // Exit the "waiting for space" state
                waitingForSpace = false;
                WaitForInput = false;
                return true;
            }

            if (keyData == Keys.Enter)
            {
                RandomImage();
                MeLogicX();
                if (WaitForInput)
                {
                    waitingForSpace = true;
                    return true;
                }
                // if (Number == 6) Repeat = true;
                // if (Repeat == false) SetButtonStyle();
                // TempGameLogic();
                // VeryFirstMove = false;
                return true;
            }

            if (keyData == Keys.Escape)
            {
                Close();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

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
