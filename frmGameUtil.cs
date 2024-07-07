using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fall
{
    public partial class frmGame
    {
        public void DisableAllBorders(string unless) {
            if (borderYellow.Name == unless) borderGreen.BorderStyle = borderRed.BorderStyle = borderBlack.BorderStyle = BorderStyle.None;
            if (borderGreen.Name == unless) borderYellow.BorderStyle = borderRed.BorderStyle = borderBlack.BorderStyle = BorderStyle.None;
            if (borderRed.Name == unless) borderBlack.BorderStyle = borderYellow.BorderStyle = borderGreen.BorderStyle = BorderStyle.None;
            if (borderBlack.Name == unless) borderYellow.BorderStyle = borderGreen.BorderStyle = borderRed.BorderStyle = BorderStyle.None;
        }
        public void DisplayFigureByPostion() {            
            if (game.lstOfActivePositions.Count > 0) {
                foreach (Position x in game.lstOfActivePositions) {
                    switch (x.NumInGame.ToString())
                    {
                        case "1": { lbl1.Text = "Y1"; } break;
                        case "11": { lbl11.Text = "Y11"; } break;
                        case "21": { lbl21.Text = "Y21"; } break;
                        case "31": { lbl31.Text = "Y31"; } break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}
