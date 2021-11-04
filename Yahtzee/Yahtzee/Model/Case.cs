/*
 *  Author : Nathan Füllemann
 *  Created : 17.09.2021
 *  Game Yahtzee
 */
namespace Yahtzee.Model
{
    /// <summary>
    /// Class Case
    /// </summary>
    public class Case
    {
        public Xamarin.Forms.ImageButton _id;
        public Xamarin.Forms.Label _tempScore;
        public Xamarin.Forms.Label _Score;
        public bool _isSet;
        public int _score = 0;

        public int Score
        {
            get { return _score; }
            set { _score = value; }
        }

        /// <summary>
        /// Constructor Custom
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tempScore"></param>
        /// <param name="Score"></param>
        public Case(Xamarin.Forms.ImageButton id, Xamarin.Forms.Label tempScore, Xamarin.Forms.Label Score)
        {
            this._id = id;
            this._tempScore = tempScore;
            this._Score = Score;
            this._isSet = false;
        }
    }
}
