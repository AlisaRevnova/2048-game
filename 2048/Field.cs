using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.Schema;

namespace _2048
{
    [Serializable]
    public class Field
    {
        public int[,] FieldOfButtons;
        public int UserCount;

        Random rand = new Random();

        public Field()
        {
            FieldOfButtons = new int[4, 4];
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    FieldOfButtons[i, j] = 0;
                }
            }
            int x = rand.Next(0, 4);
            Thread.Sleep(50);
            int y = rand.Next(0, 4);
            FieldOfButtons[x, y] = 2;
            while (true)
            {
                x = rand.Next(0, 4);
                Thread.Sleep(50);
                y = rand.Next(0, 4);
                if (FieldOfButtons[x, y] == 0)
                {
                    FieldOfButtons[x, y] = 2;
                    break;
                }
            }
            UserCount = 0;
        }
        public void Reset()
        {
            FieldOfButtons = null;
            rand = null;
        }
        void UserCountRaise(int num)
        {
            UserCount += num;
        }
        void SetNewButton()
        {
            while (true)
            {
                int x = rand.Next(0, 4);
                Thread.Sleep(50);
                int y = rand.Next(0, 4);
                if (FieldOfButtons[x, y] == 0)
                {
                    int a = rand.Next(0, 100);
                    if (a >= 0 && a <= 70)
                    {
                        FieldOfButtons[x, y] = 2;
                    }
                    else
                    {
                        FieldOfButtons[x, y] = 4;
                    }
                    break;
                }
            }
        }
        public void ShowField()
        {
            Console.WriteLine("Score: " + UserCount + "\n");
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (FieldOfButtons[i, j] != 0)
                    {
                        Console.Write(FieldOfButtons[i, j]);
                        Console.Write(" |  ");
                    }
                    else
                    {
                        Console.Write("  |  ");
                    }
                }
                Console.WriteLine("\n");
                Console.WriteLine("____________________");
            }
        }
        bool EndOfGame(bool what)
        {
            List<int>lst = FieldOfButtons.Cast<int>().ToList();
            if (what == true && lst.All(x=>x!=0))
            {
                return what;
            }
            return false;
        }
        public bool Congratulations()
        {
            List<int> lst = FieldOfButtons.Cast<int>().ToList();
            if (lst.Any(x => x == 2048))
            {
                return true;
            }
            return false;
        }
        public bool Up() // идем сверху вниз
        {
            bool end = true;
            for (int j = 0; j < 4; j++)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int k = i + 1; k < 4; k++)
                    {
                        if (FieldOfButtons[k, j] != 0)
                        {
                            if (FieldOfButtons[i, j] == 0)
                            {
                                FieldOfButtons[i, j] = FieldOfButtons[k, j];
                                FieldOfButtons[k, j] = 0;
                                end = false;
                            }
                            else
                            {
                                if (FieldOfButtons[i, j] == FieldOfButtons[k, j])
                                {
                                    FieldOfButtons[i, j] += FieldOfButtons[k, j];
                                    UserCountRaise(FieldOfButtons[i, j]);
                                    FieldOfButtons[k, j] = 0;
                                    end = false;
                                }
                                break;
                            }
                        }
                    }
                }
            }
            SetNewButton();
            return EndOfGame(end);
        }
        public bool Down() // идем снизу вверх
        {
            bool end = true;
            for (int j = 3; j >= 0; j--)
            {
                for (int i = 3; i >= 0; i--)
                {
                    for (int k = i - 1; k >= 0; k--)
                    {
                        if (FieldOfButtons[k, j] != 0)
                        {
                            if (FieldOfButtons[i, j] == 0)
                            {
                                FieldOfButtons[i, j] = FieldOfButtons[k, j];
                                FieldOfButtons[k, j] = 0;
                                end = false;
                            }
                            else
                            {
                                if (FieldOfButtons[i, j] == FieldOfButtons[k, j])
                                {
                                    FieldOfButtons[i, j] += FieldOfButtons[k, j];
                                    UserCountRaise(FieldOfButtons[i, j]);
                                    FieldOfButtons[k, j] = 0;
                                    end = false;
                                }
                                break;
                            }
                        }
                    }
                }
            }
            SetNewButton();
            return EndOfGame(end);
        }
        public bool Left() // двигаемся слева
        {
            bool end = true;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    for (int k = j + 1; k < 4; k++)
                    {
                        if (FieldOfButtons[i, k] != 0)
                        {
                            if (FieldOfButtons[i, j] == 0)
                            {
                                FieldOfButtons[i, j] = FieldOfButtons[i, k];
                                FieldOfButtons[i, k] = 0;
                                end = false;
                            }
                            else
                            {
                                if (FieldOfButtons[i, j] == FieldOfButtons[i, k])
                                {
                                    FieldOfButtons[i, j] += FieldOfButtons[i, k];
                                    UserCountRaise(FieldOfButtons[i, j]);
                                    FieldOfButtons[i, k] = 0;
                                    end = false;
                                }
                                break;
                            }
                        }
                    }
                }
            }
            SetNewButton();
            return EndOfGame(end);
        }
        public bool Right() // двигаемся справа 
        {
            bool end = true;
            for (int i = 3; i >= 0; i--)
            {
                for (int j = 3; j >= 0; j--)
                {
                    for (int k = j - 1; k >= 0; k--)
                    {
                        if (FieldOfButtons[i, k] != 0)
                        {
                            if (FieldOfButtons[i, j] == 0)
                            {
                                FieldOfButtons[i, j] = FieldOfButtons[i, k];
                                FieldOfButtons[i, k] = 0;
                                end = false;
                            }
                            else
                            {
                                if (FieldOfButtons[i, j] == FieldOfButtons[i, k])
                                {
                                    FieldOfButtons[i, j] += FieldOfButtons[i, k];
                                    UserCountRaise(FieldOfButtons[i, j]);
                                    FieldOfButtons[i, k] = 0;
                                    end = false;
                                }
                                break;
                            }
                        }
                    }
                }
            }
            SetNewButton();
            return EndOfGame(end);
        }
    }
}
