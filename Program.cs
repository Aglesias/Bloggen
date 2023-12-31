﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggen
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<string[]> inlägg = new List<string[]>();
            bool sorteradeInlägg = false;
            bool isRunning = true;
            while (isRunning) 
            {
                
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\tVälkommen till Bloggen!\n");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("\tVälj vad du vill göra ur menyn:");
                Console.WriteLine("\t1: Skriv ett nytt inlägg i bloggen.");
                Console.WriteLine("\t2: Visa alla inlägg i bloggen.");
                Console.WriteLine("\t3: Sortera bloggen efter titel i bokstavsordning.");
                Console.WriteLine("\t4: Sortera bloggen i datumordning.");
                Console.WriteLine("\t5: Sök i bloggen.");
                Console.WriteLine("\t6: Radera inlägg.");
                Console.WriteLine("\t7: Redigera inlägg.");
                Console.WriteLine("\t8: Avsluta programmet.");
                Int32.TryParse(Console.ReadLine(), out int menyVal);
                
                
                switch (menyVal)
                {
                    case 1:
                        // skapar en ny strängvektor som gör det möjligt att spara inläggets titel och sedan inläggets text. Sparar även datum och tid och gör om det till en sträng.
                        string[] detSenaste = new string[3];
                        Console.WriteLine("\tSkriv en titel på ditt inlägg:");
                        detSenaste[0] = Console.ReadLine();
                        Console.WriteLine("\tSkriv ditt inlägg:");
                        detSenaste[1] = Console.ReadLine();
                        detSenaste[2] = DateTime.Now.ToString();
                        inlägg.Add(detSenaste);
                        break;

                    case 2:
                        if (inlägg.Count > 0)
                        {
                            AllaInlägg(inlägg); // Kallar metoden AllaInlägg som skriver ut alla inlägg i bloggen
                        }
                        else
                        {
                            Console.WriteLine("\n\tDet saknas inlägg i bloggen. Skriv några inlägg först.");
                        }
                        MenyAvslut();
                        break;
                    case 3:
                        if (inlägg.Count > 0)
                        {
                            SortIBokstavsordning(inlägg);
                            Console.WriteLine("\n\tBloggen är nu sorterad efter titel på inlägg i bokstavsordning.\n");
                        }
                        else
                        {
                            Console.WriteLine("\n\tDet saknas inlägg i bloggen. Skriv ett inlägg för att kunna sortera."); // Användaren har inte genererat något värde till listan.
                        }

                        MenyAvslut();
                        Console.Clear();
                        break;
                    case 4:
                        if (inlägg.Count > 0)
                        {
                            SortIDatumordning(inlägg);
                            Console.WriteLine("\n\tBloggen är nu sorterad i tidsordning med det första inlägget överst.\n");
                        }
                        else
                        {
                            Console.WriteLine("\n\tDet saknas inlägg i bloggen. Skriv ett inlägg för att kunna sortera."); // Användaren har inte genererat något värde till listan.
                        }

                        MenyAvslut();
                        Console.Clear();
                        break;
                        break;
                    case 5:
                        if (inlägg.Count > 0)
                        {
                            SortIBokstavsordning(inlägg); // kallar metoden SortIBokstavsordning som sorterar allt i bokstavsordning så att det är möjligt att söka på inlägg via binärsökning.
                            sorteradeInlägg = true; // Listan är sorterad.
                            if (sorteradeInlägg)
                            {
                                Console.WriteLine("\n\tVilket inläggs titel vill du söka efter?");
                                string key = Console.ReadLine(); ; // Tar emot användarens sökning
                                key = key.ToUpper(); // Ändrar sökningen till versaler för att användaren ska slippa bry sig om små och stora bokstäver.
                                if (key.Length <= 0) key = "a"; // Ser till att sökningen alltid är något, i det här fallet "a"                                                             
                                int first = 0; // skapar en int som är första värdet av listan
                                int last = inlägg.Count - 1; // skapar en int som är värdet av listans längd.


                                while (first <= last)
                                {

                                    int mellan = (first + last) / 2; // skapar ett medelvärde som är första värdet plus sista värdet delat med två.
                                    string stor = inlägg[mellan][0]; // skapar en sträng av titeln på inlägget så sökningen kan bli smidigare
                                    stor = stor.ToUpper();
                                    int cmpVal = key.CompareTo(stor);// jämnför titeln som användaren vill söka efter med titeln på inlägget som står på plats "mellan" i listan


                                    if (cmpVal > 0) // OM titeln som eftersöks är efter första bokstaven i "mellan" i alfabetet
                                    {
                                        first = (mellan + 1); // ändra värdet på first till mellan + 1
                                    }
                                    else if (cmpVal < 0)//  OM titeln som eftersöks är före första bokstaven i "mellan" i alfabetet
                                    {
                                        last = (mellan - 1); // ändra värdet på "last" till mellan -1
                                    }
                                    else // om den varken är mer eller mindre vilket innebär att första bokstaven i "key" är lika med första bokstaven i ordet som nu är på plats "mellan" i alfabetet.
                                    {
                                        Console.WriteLine("Titeln du sökt är inlägg " + (mellan + 1) + " och är skrivet: " + inlägg[mellan][2]);
                                        SpecifiktInlägg(inlägg, mellan);
                                        break;
                                    }



                                }
                                if (first > last)
                                {
                                    Console.WriteLine("Det du sökte fanns ej i listan.");
                                }
                            }
                            SortIDatumordning(inlägg); // sorterar listan i datuordning så att allt är där det var från början igen.
                           
                            


                        }
                        else
                        {
                            Console.WriteLine("\n\tDet saknas inlägg i bloggen. Skriv ett inlägg för att kunna söka."); // Användaren har inte genererat något värde till listan.
                        }
                        MenyAvslut(); // Kallar på vår standardiserade menyavslutning.
                        break;
                        

                    case 6:
                        inlägg.Clear();
                        break;
                    case 7:
                        break;
                    case 8:
                        isRunning = false;
                        break;

                }
                
                
            }
            
        }
        static void SpecifiktInlägg(List<string[]> spec, int specInt)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\t" + spec[specInt][0]);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("\t-------------------\n");
            Console.WriteLine("\t" + spec[specInt][1] + "\n");
            Console.WriteLine("\t-------------------");
            Console.WriteLine("\t----Skrivet:" + spec[specInt][2] + "-----");
        }
        static void AllaInlägg(List<string[]> inl)
        {
            for (int i = 0; i < inl.Count; i++)
            {               
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\t" + inl[i][0]);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("\t-------------------\n");
                Console.WriteLine("\t" + inl[i][1] + "\n");
                Console.WriteLine("\t-------------------");
                Console.WriteLine("\t----Skrivet:" + inl[i][2] + "-----");
            }
        }
       static void SortIBokstavsordning(List<string[]> bokstavs)
        {
            if (bokstavs.Count > 0)
            {



                int max = bokstavs.Count - 1;
                for (int i = 0; i < max; i++)
                {

                    int nrLeft = max - i;

                    for (int j = 0; j < nrLeft; j++)
                    {
                        int cmpVal = bokstavs[j][0].CompareTo(bokstavs[j + 1][0]); // Skapar ett värde som antingen är -1, 0, eller 1 genom att jämnföra ord i ordLista med varandra
                        if (cmpVal > 0) // om värdet är mindre än 0 så sorteras listan genom att man skapar en temporär sträng med som är samma som j och ändrar den till j + 1 och därmed byter plats på de båda.
                        {
                            string temp = bokstavs[j][0];
                            bokstavs[j][0] = bokstavs[j + 1][0];
                            bokstavs[j + 1][0] = temp;

                            string temp1 = bokstavs[j][1];
                            bokstavs[j][1] = bokstavs[j + 1][1];
                            bokstavs[j + 1][1] = temp1;

                            string temp2 = bokstavs[j][2];
                            bokstavs[j][2] = bokstavs[j + 1][2];
                            bokstavs[j + 1][2] = temp2;
                        }

                    }

                }
               


            }
          
        }
        static void SortIDatumordning(List<string[]> datum)
        {
            if (datum.Count > 0)
            {
                

                int max = datum.Count - 1;
                for (int i = 0; i < max; i++)
                {

                    int nrLeft = max - i;

                    for (int j = 0; j < nrLeft; j++)
                    {
                        int cmpVal = datum[j][2].CompareTo(datum[j + 1][2]); // Skapar ett värde som antingen är -1, 0, eller 1 genom att jämnföra ord i ordLista med varandra
                        if (cmpVal > 0) // om värdet är mindre än 0 så sorteras listan genom att man skapar en temporär sträng med som är samma som j och ändrar den till j + 1 och därmed byter plats på de båda.
                        {
                            string temp = datum[j][2];
                            datum[j][2] = datum[j + 1][2];
                            datum[j + 1][2] = temp;

                            string temp1 = datum[j][0];
                            datum[j][0] = datum[j + 1][0];
                            datum[j + 1][0] = temp1;

                            string temp2 = datum[j][1];
                            datum[j][1] = datum[j + 1][1];
                            datum[j + 1][1] = temp2;
                        }

                    }

                }
                


            }
            else
            {
                Console.WriteLine("\n\tDet saknas inlägg i bokstavslistan. Generera lite bokstäver först.");
            }

        }
        static void MenyAvslut()
        {

            Console.WriteLine("Tryck ENTER för att återgå till menyn!");
            Console.ReadLine();

        }
    }
}
