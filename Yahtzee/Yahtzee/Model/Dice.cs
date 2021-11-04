/*
 *  Author : Nathan Füllemann
 *  Created : 17.09.2021
 *  Game Yahtzee
 */

using System;

namespace Yahtzee.Model
{
    class Dice
    {
        public Xamarin.Forms.ImageButton _id;
        public byte _value;
        public string _img;
        public bool _active;

        public byte Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public string SourceImg
        {
            get { return _img; }
            set { _img = value; }
        }

        public Dice(Xamarin.Forms.ImageButton id)
        {
            this._id = id;
            this._img = "dice_Chance2.png";
            this._id.Source = this._img;
            this._active = true;
        }

        public void GenerateNew()
        {
            if (this._active)
            {
                this._value = GenerateRandom();
                this._img = Source(this._value);
                this._id.Source = this._img;
            }
        }

        public byte GenerateRandom()
        {
            this._value = (byte)new Random().Next(1, 6);
            return this._value;
        }

        public string Source(byte value)
        {
            switch (value)
            {
                case 1:
                    return "dice_one.png";
                case 2:
                    return "dice_two.png";
                case 3:
                    return "dice_three.png";
                case 4:
                    return "dice_four.png";
                case 5:
                    return "dice_five.png";
                case 6:
                    return "dice_six.png";
                default:
                    return "dice_Chance2.png";
            }
        }

        public string SourceLocked(byte value)
        {
            switch (value)
            {
                case 1:
                    return "dice_one_active.png";
                case 2:
                    return "dice_two_active.png";
                case 3:
                    return "dice_three_active.png";
                case 4:
                    return "dice_four_active.png";
                case 5:
                    return "dice_five_active.png";
                case 6:
                    return "dice_six_active.png";
                default:
                    return "dice_Chance2.png";
            }
        }

        public void LockImage()
        {
            if (_active)
            {
                this._img = Source(this._value);
            }
            else
            {
                this._img = SourceLocked(this._value);
            }
            this._id.Source = this._img;
        }

        public void StartImg()
        {
            this._img = "dice_Chance2.png";
        }
    }
}
