using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fall
{
    public static class GameState
    {
        public const string Start = "Start";
        public const string Play = "Play";
        public const string End = "End";
    }
    internal class Game
    {
        public string State { get; set; }
        public bool Bingo { get; set; }
        public Player ActivePlayer { get; set; }

        public int CurrentPosition { get; set; }
        public int StartPosition { get; set; }
        public int EndPosition { get; set; }

        public string whichPlayer;

        public List<KeyValuePair<string, string>> lstNotationPosition = new List<KeyValuePair<string, string>>();
        
        public int RandomNumber { get; set; }
        public List<Player> lstPlayers = new List<Player>();

        public List<Position> lstOfActivePositions = new List<Position>();

        public int GenerateRandomNumber() {
            Random rand = new Random();
            int tempNumber = rand.Next(1, 7);
            Bingo = false;
            if (tempNumber == 6) Bingo = true;
            return tempNumber;
        }

        // set Active player


        // insert player for new game after getting Bingo

        // repeat with same Player if Bingo is setted
        public Player RunCubeForActivePlayer(Player activePlayer) {
            activePlayer.LastNumber = GenerateRandomNumber();
            activePlayer.IsBingo = Bingo;
            InsertFigureInGame(activePlayer);
            /*
            while (Bingo) {
                activePlayer.LastNumber = GenerateRandomNumber();
                MoveFigureToNewPosition(activePlayer);
                // Movement
                // with active Player move active Figure on Position
                // set active Player and set active Figure change and save Position property if already some Figure is there
                // save Position property - figureOnPosition
                // Movement - if non active figure in player -- do nothing
            }
            */
            Figure figure = new Figure();
            // save figure position
            figure = MoveInGameFigure(activePlayer);
            if(figure.CurrentPosition != null) 
                activePlayer.ActiveFigures.Add(figure);
            

            return activePlayer;
        }

        public Figure InsertFigureInGame(Player player) {
            // choose active figure and move it 
            // if other figure is behind them move those figure -- read oponent figure from position - which is near
            Figure figure = new Figure();
            Position position = new Position(new Position[0]);

            // if (player.ActiveFigures.Count == 0) {
                if (player.IsBingo) {
                    // put First figure to the board (position)
                    if (player.Name == "zu1") { position.NumInGame = 1; figure.CurrentPosition = position; }
                    else if (player.Name == "ze1") { position.NumInGame = 11; figure.CurrentPosition = position; }
                    else if (player.Name == "c1") { position.NumInGame = 21; figure.CurrentPosition = position; }
                    else if (player.Name == "cr1") { position.NumInGame = 31; figure.CurrentPosition = position; }
                    figure.Name = player.Name + "1"; player.ActiveFigures.Add(figure);
                    lstOfActivePositions.Add(position);
                }
            // }

            return figure; // player.figurefigure;
        }
        public Figure MoveInGameFigure(Player player) {
            Figure figure = new Figure();

            try
            {
                // if case of One figure InGame - move those to new location
                if (player.ActiveFigures.Count > 0)
                {
                    int currentPostion = player.ActiveFigures[0].CurrentPosition.NumInGame;
                    int lastNumber = player.LastNumber;
                    int newPosition = currentPostion + lastNumber;
                    string newPositionText = "lbl" + newPosition.ToString();
                    figure.CurrentPosition = new Position(new Position[0]) { Name = newPositionText };
                    figure.CurrentPosition.Name = newPositionText;
                }
            }
            catch { }

            return figure;
        }
    }
}
