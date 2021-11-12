/*
 *  Author : Nathan Füllemann
 *  Created : 17.09.2021
 *  Game Yahtzee
 */

using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Yahtzee.Model;

namespace Yahtzee
{
    public partial class MainPage : ContentPage
    {
        //List de tous les dés
        List<Dice> dices;
        //List de toutes les cases
        List<Case> cases;

        //Variables
        byte _move = 0;
        byte _maxMove = 3;
        byte _turn = 1;
        byte _maxTurn = 15;

        public MainPage()
        {
            InitializeComponent();
            //Create Dice
            dices = new List<Dice>
            {
                new Dice(dice1)
                ,new Dice(dice2)
                ,new Dice(dice3)
                ,new Dice(dice4)
                ,new Dice(dice5)
            };

            //Create Case
            cases = new List<Case>
            {
                new Case(one, ScoreTempOne, ScoreOne),
                new Case(two, ScoreTempTwo, ScoreTwo),
                new Case(three, ScoreTempThree, ScoreThree),
                new Case(four, ScoreTempFour, ScoreFour),
                new Case(five, ScoreTempFive, ScoreFive),
                new Case(six, ScoreTempSix, ScoreSix),
                new Case(paire, ScoreTempPair, ScorePair),
                new Case(brelan, ScoreTempBrelan, ScoreBrelan),
                new Case(xpaire, ScoreTempxPaire, ScorexPaire),
                new Case(carre, ScoreTempCarre, ScoreCarre),
                new Case(full, ScoreTempFull, ScoreFull),
                new Case(litleSuite, ScoreTempLitleSuite, ScorelitleSuite),
                new Case(bigSuite, ScoreTempbigSuite, ScorebigSuite),
                new Case(chance, ScoreTempChance, ScoreChance),
                new Case(yhatzee, ScoreTempYhatzee, ScoreYhatzee)
            };
        }

        /// <summary>
        /// Génération des dés
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GenerateDice(object sender, EventArgs e)
        {
            //Si on est pas au dernier mouvement
            if(_move != _maxMove)
            {
                //On passe un tour
                _move++;

                //Affichage
                move.Text = _move + "/" + _maxMove;

                //Affichage des nouveaux dés
                foreach (Dice dice in dices)
                {
                    dice.GenerateNew();
                }
            }
        }

        /// <summary>
        /// Vérouillage des dés
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LockDice(object sender, EventArgs e)
        {
            //Récupération de l'image bouton
            ImageButton img = (ImageButton)sender;

            //On l active ou pas
            dices[int.Parse(img.ClassId) - 1]._active = dices[Int32.Parse(img.ClassId) - 1]._active != true;
            dices[int.Parse(img.ClassId) - 1].LockImage();

            //On passe dans toute les cases pour afficher les scores temporaires
            foreach (Case c in cases)
            {
                //On passe seulement dans les case pas encore attribuée
                if (!c._isSet)
                {
                    c._tempScore.Text = "0";

                    //Si c est un chiffre
                    if (int.Parse(c._id.ClassId) <= 6)
                    {
                        foreach (Dice dice in dices)
                        {
                            //Si le dés est actif
                            if (!dice._active)
                            {
                                //Si la valeur est bien la meme que celle de la case
                                if (dice.Value.ToString() == c._id.ClassId)
                                {
                                    //On affiche
                                    c._tempScore.Text = (int.Parse(c._tempScore.Text) + dice.Value).ToString();
                                }
                            }
                        }
                    }
                    //Si c est un event autre
                    else
                    {
                        //Liste des valeurs de tous les dés
                        List<int> numero = new List<int>();

                        foreach (Dice dice in dices)
                        {
                            //si le dé est actif
                            if (!dice._active)
                            {
                                //on ajoute le numéro
                                numero.Add(dice.Value);
                            }
                        }
                        //On affiche le score temporaire
                        c._tempScore.Text = CheckScore(c, numero).ToString();
                    }

                    //On affiche les valeurs temporaires
                    if (c._tempScore.Text != "0")
                    {
                        c._tempScore.IsVisible = true;
                    }
                }
                //Si c est un 2eme yahtzee
                else if (c == cases[14]) { 
                    // Liste des valeurs de tous les dés
                        List<int> numero = new List<int>();

                    foreach (Dice dice in dices)
                    {
                        //si le dé est actif
                        if (!dice._active)
                        {
                            //on ajoute le numéro
                            numero.Add(dice.Value);
                        }
                    }

                    c._tempScore.Text = CheckScore(c, numero).ToString();
                }
            }
        }

        /// <summary>
        /// Event de quand un option pour valider ses point est choisie
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OptionChoose (object sender, EventArgs e)
        {
            //Récupération de l image bouton
            ImageButton img = (ImageButton)sender;
            //Récupération de la case sur la quelle on a appuyer
            Case actuCase = cases[int.Parse(img.ClassId) - 1];

            //On la set active
            actuCase._Score.IsVisible = true;
            actuCase._isSet = true;

            //Si c est un chiffre
            if (int.Parse(actuCase._id.ClassId) <= 6)
            {
                foreach (Dice dice in dices)
                {
                    //If dice is active
                    if (!dice._active)
                    {
                        //If dice have same value as actual case 
                        if (dice.Value.ToString() == actuCase._id.ClassId)
                        {
                            //Set Score
                            actuCase._Score.Text = (int.Parse(actuCase._Score.Text) + dice.Value).ToString();
                            actuCase.Score += dice.Value;
                        }
                    }
                   
                }
            }
            //Si c est un event autre
            else
            {
                //List de toutes les valeurs
                List<int> numero = new List<int>();

                foreach (Dice dice in dices)
                {
                    //Si le dé est actif
                    if (!dice._active)
                    {
                        //Ajout de la valeur
                        numero.Add(dice.Value);
                    }
                }
                
                //Récupération
                int score = CheckScore(actuCase, numero);

                //Set if yahtzee
                if (actuCase == cases[14])
                {
                    actuCase._isYahtzee = true;
                }

                //affichage du score
                actuCase._Score.Text = score.ToString();
                actuCase.Score += score;
            }

            //Reset all blocked
            EndTurn();

            _move = 0;
            
           
            //Reset Points
            points.Text = "0";

            foreach (Case c in cases)
            {
                //Get all score
                points.Text = (int.Parse(points.Text) + c.Score).ToString();
                //Reset all tempScore
                c._tempScore.IsVisible = false;
            }

            //Si la partie est finie
            if (_turn == 15)
            {
                Navigation.PushAsync(new ScoreEnd(points.Text));
                Navigation.RemovePage(this);
            }

            //Add turn
            _turn++;
            turn.Text = _turn + "/" + _maxTurn;
        }

        /// <summary>
        /// End Turn
        /// </summary>
        private void  EndTurn()
        {
            foreach (Dice dice in dices)
            {
                dice._active = true;
                dice.StartImg();
            }
        }

        /// <summary>
        /// Events des case autre des chiffres
        /// </summary>
        /// <param name="actuCase"></param>
        /// <param name="numero"></param>
        /// <returns></returns>
        private int CheckScore(Case actuCase,List<int> numero)
        {
            //Sort
            largestToSmallest lts = new largestToSmallest();

            int result = 0;

            //Switch sur tout les events
            switch (int.Parse(actuCase._id.ClassId))
            {
                //Paire
                case 7:
                    if (numero.Count() >= 2)
                    {
                        result =  IfMultipleValues(AssembleValues(numero.ToArray()), 2);
                    }
                        break;
                //Brelan
                case 8:
                    if(numero.Count() >= 3)
                    {
                        numero = AddDefaultValue(numero);

                        if (numero[0] == numero[1] && numero[1] == numero[2] || numero[0] == numero[1] && numero[1] == numero[3] ||
                            numero[0] == numero[1] && numero[1] == numero[4] || numero[0] == numero[2] && numero[2] == numero[3] ||
                            numero[0] == numero[2] && numero[2] == numero[4] || numero[0] == numero[3] && numero[3] == numero[4] ||
                            numero[1] == numero[2] && numero[2] == numero[3] || numero[1] == numero[2] && numero[2] == numero[4] ||
                            numero[2] == numero[3] && numero[3] == numero[4] || numero[1] == numero[3] && numero[3] == numero[4])
                        {
                            result = total(numero);
                        }                  
                    }
                    break;
                //Double paire
                case 9:
                    if (numero.Count() >= 4)
                    {
                        result = IfMultipleValues(AssembleValues(numero.ToArray()), 2, 2);

                    }
                    break;
                //Carré
                case 10:
                    if(numero.Count() >= 4)
                    {
                        numero = AddDefaultValue(numero);

                        if (numero[1] == numero[2] && numero[2] == numero[3] && numero[3] == numero[4] ||
                            numero[0] == numero[1] && numero[1] == numero[2] && numero[2] == numero[3] ||
                            numero[0] == numero[1] && numero[1] == numero[3] && numero[3] == numero[4] ||
                            numero[0] == numero[2] && numero[2] == numero[3] && numero[3] == numero[4] ||
                            numero[0] == numero[1] && numero[1] == numero[2] && numero[2] == numero[4])
                        {
                            result = total(numero);
                        }
                    }
                    
                    break;
                //Full
                case 11:
                    if(numero.Count() == 5)
                    {
                        if (numero[0] == numero[1] && numero[1] == numero[2] && numero[3] == numero[4] ||
                        numero[0] == numero[1] && numero[1] == numero[3] && numero[2] == numero[4] ||
                        numero[0] == numero[1] && numero[1] == numero[4] && numero[2] == numero[3] ||
                        numero[0] == numero[2] && numero[2] == numero[3] && numero[1] == numero[4] ||
                        numero[0] == numero[2] && numero[2] == numero[4] && numero[1] == numero[3] ||
                        numero[0] == numero[3] && numero[3] == numero[4] && numero[1] == numero[2] ||
                        numero[1] == numero[2] && numero[2] == numero[3] && numero[0] == numero[4] ||
                        numero[1] == numero[2] && numero[2] == numero[4] && numero[0] == numero[3] ||
                        numero[2] == numero[3] && numero[3] == numero[4] && numero[0] == numero[1] ||
                        numero[1] == numero[3] && numero[3] == numero[4] && numero[0] == numero[2])
                        {
                            result = 25;
                        }
                    }
                    break;
                //Petite suite
                case 12:
                    if(numero.Count() >= 4)
                    {
                        numero = AddDefaultValue(numero);
                        numero.Sort(lts);

                        if (
                            numero[0] == numero[1] && (numero[4] - numero[3]) == 1 && (numero[3] - numero[2]) == 1 && (numero[2] - numero[1]) == 1 ||
                            numero[1] == numero[2] && (numero[4] - numero[3]) == 1 && (numero[3] - numero[1]) == 1 && (numero[1] - numero[0]) == 1 ||
                            numero[2] == numero[3] && (numero[4] - numero[2]) == 1 && (numero[2] - numero[1]) == 1 && (numero[1] - numero[0]) == 1 ||
                            numero[3] == numero[4] && (numero[3] - numero[2]) == 1 && (numero[2] - numero[1]) == 1 && (numero[1] - numero[0]) == 1 ||
                            (numero[4] - numero[3]) == 1 && (numero[3] - numero[2]) == 1 && (numero[2] - numero[1]) == 1 && (numero[1] - numero[0]) == 1 ||
                            numero[0] != numero[1] && (numero[4] - numero[3]) == 1 && (numero[3] - numero[2]) == 1 && (numero[2] - numero[1]) == 1 ||
                            numero[4] != numero[3] && (numero[3] - numero[2]) == 1 && (numero[2] - numero[1]) == 1 && (numero[1] - numero[0]) == 1
                            )
                        {
                            result = 30;
                        }
                    }
                    break;
                //Grande Suite
                case 13:
                    if(numero.Count() == 5)
                    {
                        numero.Sort(lts);
                        if ((numero[4] - numero[3]) == 1 && (numero[3] - numero[2]) == 1 && (numero[2] - numero[1]) == 1 && (numero[1] - numero[0]) == 1)
                        {
                            result = 40;
                        }
                    }
                    break;
                //Chance
                case 14:
                    foreach (int value in numero)
                    {
                        result += value;
                    }
                    break;
                //Yhatzee
                case 15:
                    if (numero.Count() == 5)
                    {
                        if (numero[0] == numero[1] && numero[1] == numero[2] && numero[2] == numero[3] && numero[3] == numero[4])
                        {
                            if (cases[14]._isYahtzee)
                            {
                                result = 100;
                            }
                            else
                            {
                                result = 50;
                            }
                        }
                    }
                       
                    break;
            }
            //Check if possible and return
            if(result != -1)
            {
                return result;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Sort
        /// </summary>
        class largestToSmallest : IComparer<int>
        {
            public int Compare(int x, int y)
            {
                if (x == 0 || y == 0)
                {
                    return 0;
                }

                return x.CompareTo(y);
            }
        }

        /// <summary>
        /// Calcule du total de tous les valeurs
        /// </summary>
        /// <param name="numero"></param>
        /// <returns></returns>
        private int total(List<int> numero)
        {
            int result = 0;
            foreach (int value in numero)
            {
                result += value;
            }

            return result;
        }

        /// <summary>
        /// Ajout de valeur fictive a la liste de chiffre
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        private List<int> AddDefaultValue(List<int> number)
        {
            for (int i = number.Count(); i < 5; i++)
            {
                number.Add(0);
            }
            

            return number;
        }

        /// <summary>
        /// On assemble les valeurs
        /// </summary>
        /// <param name="numbers">Liste des valeurs</param>
        /// <returns>On retourne la liste des nombre assemblés</returns>
        public static Dictionary<byte, int> AssembleValues(int[] numbers)
        {
            //Dictionnaire de valeurs
            Dictionary<byte, int> values = new Dictionary<byte, int>();

            //On boucle les nombres
            foreach (byte number in numbers)
            {
                //Si on trouve la clé
                if (values.ContainsKey(number))
                {
                    //On incrémente
                    values[number]++;
                }
                else
                {
                    //On ajoute un element
                    values.Add(number, 1);
                }
            }

            //On retourne les valeurs
            return values;
        }

        /// Check si il y a plusieurs valeurs dans une liste
        /// </summary>
        /// <param name="values">Liste de valeurs</param>
        /// <param name="total">Total des valeurs</param>
        /// <returns>Retourne si il y a assez de valeur ou pas</returns>
        public static int IfMultipleValues(Dictionary<byte, int> values, params int[] total)
        {
            //On crée la somme
            int sum = 0;

            //On boucle les valeurs
            foreach (int number in total)
            {
                //On récupére les clés par rapport au nombre de valeur qu'on veut
                IEnumerable<byte> keys = values.Where(x => x.Value == number).Select(x => x.Key);

                //Si on a trouvé des valeurs
                if (keys.Count() > 0)
                {
                    //On récupére la plus grande klé
                    byte key = keys.Max();

                    //On ajoute le somme des valeurs
                    sum += values[key] * key;

                    //On retire la clé des valeurs
                    values.Remove(key);
                }
                else
                {
                    //On retourne -1 (Not Found)
                    return -1;
                }
            }

            //On retourne la somme des valeurs
            return sum;
        }
    }
}