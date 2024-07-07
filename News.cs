using System;
using System.Drawing;
using System.Net.NetworkInformation;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Fall
{
    public partial class News : Form
    {
        public class CustomPictureBox : PictureBox
        {
            public int Position { get; set; }
            public int Figure { get; set; }
        }

        private Timer animationTimer;
        private int currentIndex;
        private int targetIndex;
        private int step;
        private int cubeNumber = 0;


        public News()
        {
            InitializeComponent();
            CreateCustomPictureBoxes();
            InitializeAnimation();
            InitializeOther();
        }

        private void InitializeAnimation()
        {
            animationTimer = new Timer();
            animationTimer.Interval = 100; // Animation step interval in milliseconds
            animationTimer.Tick += AnimationTimer_Tick;
            currentIndex = -1;
            targetIndex = -1;
        }
        private void InitializeOther()
        {
            pCube.Paint += PictureBox_Paint;
            bCube.Paint += BCube_Paint;
        }

        private void BCube_Paint(object sender, PaintEventArgs e)
        {
            if (sender is Button) {
                e.Graphics.DrawString(cubeNumber.ToString(), this.Font, Brushes.Black, new PointF(10, 10));
            }
        }


        private void CreateCustomPictureBoxes()
        {
            for (int i = 0; i < 71; i++)
            {
                CustomPictureBox pictureBox = new CustomPictureBox
                {
                    Position = i,
                    Figure = i, // Assign any figure value you need
                    Width = 100,
                    Height = 100,
                    BorderStyle = BorderStyle.FixedSingle,
                    // Location = new System.Drawing.Point(10 + (i % 10) * 110, 10 + (i / 10) * 110),
                    TabIndex = i, // Set the TabIndex to match the loop index
                    TabStop = true, // Ensure TabStop is true
                    Tag = i // Store the position in the Tag property
                };
                // case design
                Point p = new Point(0, 0);
                Figure f = new Figure();
                switch (i) {
                    case 0: { p = new Point(450, 1110); Name = "p0"; f.Name = "p0"; } break;
                    case 1: { p = new Point(450, 1000); Name = "p1"; f.Name = "p1"; } break;
                    case 2: { p = new Point(450, 890); Name = "p2"; f.Name = "p2"; } break;
                    case 3: { p = new Point(450, 780); Name = "p3"; f.Name = "p3"; } break;
                    case 4: { p = new Point(340, 780); Name = "p4"; f.Name = "p4"; } break;
                    case 5: { p = new Point(230, 780); Name = "p5"; f.Name = "p5"; } break;
                    case 6: { p = new Point(120, 780); Name = "p6"; f.Name = "p6"; } break;
                    case 7: { p = new Point(10, 780); Name = "p7"; f.Name = "p7"; } break;
                    case 8: { p = new Point(10, 670); Name = "p8"; f.Name = "p8"; } break;
                    case 9: { p = new Point(10, 560); Name = "p9"; f.Name = "p9"; } break;
                    case 10: { p = new Point(10, 450); Name = "p10"; f.Name = "p10"; } break;
                    case 11: { p = new Point(120, 450); Name = "p11"; f.Name = "p11"; } break;
                    case 12: { p = new Point(230, 450); Name = "p12"; f.Name = "p12"; } break;
                    case 13: { p = new Point(340, 450); Name = "p13"; f.Name = "p13"; } break;
                    case 14: { p = new Point(450, 450); Name = "p14"; f.Name = "p14"; } break;
                    case 15: { p = new Point(450, 340); Name = "p15"; f.Name = "p15"; } break;
                    case 16: { p = new Point(450, 230); Name = "p16"; f.Name = "p16"; } break;
                    case 17: { p = new Point(450, 120); Name = "p17"; f.Name = "p17"; } break;
                    case 18: { p = new Point(450, 10); Name = "p18"; f.Name = "p18"; } break;
                    case 19: { p = new Point(560, 10); Name = "p19"; f.Name = "p19"; } break;
                    case 20: { p = new Point(670, 10); Name = "p20"; f.Name = "p20"; } break;
                    case 21: { p = new Point(670, 120); Name = "p21"; f.Name = "p21"; } break;
                    case 22: { p = new Point(670, 230); Name = "p22"; f.Name = "p22"; } break;
                    case 23: { p = new Point(670, 340); Name = "p23"; f.Name = "p23"; } break;
                    case 24: { p = new Point(670, 450); Name = "p24"; f.Name = "p24"; } break;
                    case 25: { p = new Point(780, 450); Name = "p25"; f.Name = "p25"; } break;
                    case 26: { p = new Point(890, 450); Name = "p26"; f.Name = "p26"; } break;
                    case 27: { p = new Point(1000, 450); Name = "p27"; f.Name = "p27"; } break;
                    case 28: { p = new Point(1110, 450); Name = "p28"; f.Name = "p28"; } break;
                    case 29: { p = new Point(1110, 560); Name = "p29"; f.Name = "p29"; } break;
                    case 30: { p = new Point(1110, 670); Name = "p30"; f.Name = "p30"; } break;
                    case 31: { p = new Point(1000, 670); Name = "p31"; f.Name = "p31"; } break;
                    case 32: { p = new Point(890, 670); Name = "p32"; f.Name = "p32"; } break;
                    case 33: { p = new Point(780, 670); Name = "p33"; f.Name = "p33"; } break;
                    case 34: { p = new Point(670, 670); Name = "p34"; f.Name = "p34"; } break;
                    case 35: { p = new Point(670, 780); Name = "p35"; f.Name = "p35"; } break;
                    case 36: { p = new Point(670, 890); Name = "p36"; f.Name = "p36"; } break;
                    case 37: { p = new Point(670, 1000); Name = "p37"; f.Name = "p37"; } break;
                    case 38: { p = new Point(670, 1110); Name = "p38"; f.Name = "p38"; } break;
                    case 39: { p = new Point(560, 1110); Name = "p39"; f.Name = "p39"; } break;
                    case 40: { p = new Point(560, 1000); Name = "p40"; f.Name = "p40"; } break;
                    case 41: { p = new Point(560, 890); Name = "p41"; f.Name = "p41"; } break;
                    case 42: { p = new Point(560, 780); Name = "p42"; f.Name = "p42"; } break;
                    case 43: { p = new Point(560, 670); Name = "p43"; f.Name = "p43"; } break;
                    case 44: { p = new Point(120, 560); Name = "p44"; f.Name = "p44"; } break;
                    case 45: { p = new Point(230, 560); Name = "p45"; f.Name = "p45"; } break;
                    case 46: { p = new Point(340, 560); Name = "p46"; f.Name = "p46"; } break;
                    case 47: { p = new Point(450, 560); Name = "p47"; f.Name = "p47"; } break;
                    case 48: { p = new Point(560, 120); Name = "p48"; f.Name = "p48"; } break;
                    case 49: { p = new Point(560, 230); Name = "p49"; f.Name = "p49"; } break;
                    case 50: { p = new Point(560, 340); Name = "p50"; f.Name = "p50"; } break;
                    case 51: { p = new Point(560, 450); Name = "p51"; f.Name = "p51"; } break;
                    case 52: { p = new Point(1000, 560); Name = "p52"; f.Name = "p52"; } break;
                    case 53: { p = new Point(890, 560); Name = "p53"; f.Name = "p53"; } break;
                    case 54: { p = new Point(780, 560); Name = "p54"; f.Name = "p54"; } break;
                    case 55: { p = new Point(670, 560); Name = "p55"; f.Name = "p55"; } break;
                    case 56: { p = new Point(10, 1110); Name = "p56"; f.Name = "y1"; } break;
                    case 57: { p = new Point(10, 1000); Name = "p57"; f.Name = "y2"; } break;
                    case 58: { p = new Point(120, 1000); Name = "p58"; f.Name = "y3"; } break;
                    case 59: { p = new Point(120, 1110); Name = "p59"; f.Name = "y4"; } break;
                    case 60: { p = new Point(10, 120); Name = "p60"; f.Name = "g1"; } break;
                    case 61: { p = new Point(10, 10); Name = "p61"; f.Name = "g2"; } break;
                    case 62: { p = new Point(120, 10); Name = "p62"; f.Name = "g3"; } break;
                    case 63: { p = new Point(120, 120); Name = "p63"; f.Name = "g4"; } break;
                    case 64: { p = new Point(900, 120); Name = "p64"; f.Name = "r1"; } break;
                    case 65: { p = new Point(900, 10); Name = "p65"; f.Name = "r2"; } break;
                    case 66: { p = new Point(1110, 10); Name = "p66"; f.Name = "r3"; } break;
                    case 67: { p = new Point(1110, 120); Name = "p67"; f.Name = "r4"; } break;
                    case 68: { p = new Point(900, 1110); Name = "p68"; f.Name = "b1"; } break;
                    case 69: { p = new Point(900, 1000); Name = "p69"; f.Name = "b2"; } break;
                    case 70: { p = new Point(1110, 1000); Name = "p70"; f.Name = "b3"; } break;
                    case 71: { p = new Point(1110, 1110); Name = "p71"; f.Name = "b4"; } break;
                }

                pictureBox.Location = p;

                pictureBox.Click += PictureBox_Click;
                pictureBox.Paint += PictureBox_Paint;

                // Add the PictureBox to the form's controls
                this.Controls.Add(pictureBox);
            }
        }

        private string UpdateName(object sender) {
            string name = string.Empty;
            if (sender is CustomPictureBox pictureBox) {                
                switch (pictureBox.Position)
                {
                    case 0: name = "y0"; break;
                    case 1: name = "y1"; break;
                    case 2: name = "y2"; break;
                    case 3: name = "y3"; break;
                    case 4: name = "g1"; break;
                    case 5: name = "g2"; break;
                    case 6: name = "g3"; break;
                    case 7: name = "g4"; break;
                    case 8: name = "r1"; break;
                    case 9: name = "r2"; break;
                    case 10: name = "r3"; break;
                    case 11: name = "r4"; break;
                    case 12: name = "b1"; break;
                    case 13: name = "b2"; break;
                    case 14: name = "b3"; break;
                    case 15: name = "b4"; break;
                    case 16: name = "p1"; break;
                    case 17: name = "p2"; break;
                    case 18: name = "p3"; break;
                    case 19: name = "p4"; break;
                    case 20: name = "p5"; break;
                    case 21: name = "p6"; break;
                    case 22: name = "p7"; break;
                    case 23: name = "p8"; break;
                    case 24: name = "p9"; break;
                    case 25: name = "p10"; break;
                    case 26: name = "p11"; break;
                    case 27: name = "p12"; break;
                    case 28: name = "p13"; break;
                    case 29: name = "p14"; break;
                    case 30: name = "p15"; break;
                    case 31: name = "p16"; break;
                    case 32: name = "p17"; break;
                    case 33: name = "p18"; break;
                    case 34: name = "p19"; break;
                    case 35: name = "p20"; break;
                    case 36: name = "p21"; break;
                    case 37: name = "p22"; break;
                    case 38: name = "p23"; break;
                    case 39: name = "p24"; break;
                    case 40: name = "p25"; break;
                    case 41: name = "p26"; break;
                    case 42: name = "p27"; break;
                    case 43: name = "p28"; break;
                    case 44: name = "p29"; break;
                    case 45: name = "p30"; break;
                    case 46: name = "p31"; break;
                    case 47: name = "p32"; break;
                    case 48: name = "p33"; break;
                    case 49: name = "p34"; break;
                    case 50: name = "p35"; break;
                    case 51: name = "p36"; break;
                    case 52: name = "p37"; break;
                    case 53: name = "p38"; break;
                    case 54: name = "p39"; break;
                    case 55: name = "p40"; break;
                    case 56: name = "fy1"; break;
                    case 57: name = "fy2"; break;
                    case 58: name = "fy3"; break;
                    case 59: name = "fy4"; break;
                    case 60: name = "fg1"; break;
                    case 61: name = "fg2"; break;
                    case 62: name = "fg3"; break;
                    case 63: name = "fg4"; break;
                    case 64: name = "fr1"; break;
                    case 65: name = "fr2"; break;
                    case 66: name = "fr3"; break;
                    case 67: name = "fr4"; break;
                    case 68: name = "fb1"; break;
                    case 69: name = "fb2"; break;
                    case 70: name = "fb3"; break;
                    case 71: name = "fb4"; break;
                    default:
                        break;
                }
                pictureBox.Name = name;
            }
            return name;
        }

        private void pCube_Paint(object sender, PaintEventArgs e) {
            if (sender is PictureBox) { 
                
            }
        }
        private void PictureBox_Paint(object sender, PaintEventArgs e)
        {
            if (sender is CustomPictureBox pictureBox)
            {
                using (Font myFont = new Font("Arial", 14))
                {
                    // Draw the position number in the center of the PictureBox
                    string positionText = pictureBox.Position.ToString();
                    SizeF textSize = e.Graphics.MeasureString(positionText, myFont);
                    PointF locationToDraw = new PointF(
                        (pictureBox.Width / 2) - (textSize.Width / 2),
                        (pictureBox.Height / 2) - (textSize.Height / 2)
                    );
                    positionText = UpdateName(sender);
                    e.Graphics.DrawString(positionText, myFont, Brushes.Black, locationToDraw);
                }
            }
        }

        private void PictureBox_Click(object sender, EventArgs e)
        {
            if (sender is CustomPictureBox pictureBox)
            {
                int startPosition = pictureBox.Position;
                int endPosition = (startPosition + cubeNumber) % 71;

                currentIndex = startPosition;
                targetIndex = endPosition;
                step = 1; // Start with the first step

                animationTimer.Start();
            }
        }

        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            if (currentIndex != targetIndex)
            {
                // Determine the next position
                int nextIndex = (currentIndex + step) % 71;

                // Find the current and next PictureBox controls
                CustomPictureBox currentPictureBox = FindPictureBoxByPosition(currentIndex);
                CustomPictureBox nextPictureBox = FindPictureBoxByPosition(nextIndex);

                // You can add any visual effect here, for example, changing the border color
                if (currentPictureBox != null)
                {
                    currentPictureBox.BorderStyle = BorderStyle.FixedSingle;
                }
                if (nextPictureBox != null)
                {
                    nextPictureBox.BorderStyle = BorderStyle.Fixed3D;
                }

                // Move to the next position
                currentIndex = nextIndex;
            }
            else
            {
                // Stop the animation when the target is reached
                animationTimer.Stop();
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

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            cubeNumber = new Random().Next(1, 7); 
            bCube.Text = cubeNumber.ToString();
        }
    }
}
