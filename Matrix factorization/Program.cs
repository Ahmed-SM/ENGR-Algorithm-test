using System;
using System.Collections;
using System.Collections.Generic;
using Accord.Math;

namespace Matrix_factorization
{
    class Program
    {
        
        private static int maxLevel = 0;
        private static int maxHelper = 1;
        private const double TEN_TENTHS     = 1.0;
        private const double NINE_TENTHS    = 0.9;
        private const double EIGHT_TENTHS   = 0.8;
        private const double SEVEN_TENTHS   = 0.7;
        private const double SIX_TENTHS     = 0.6;
        private const double FIVE_TENTHS    = 0.5;
        private const double FOUR_TENTHS    = 0.4;
        private const double THREE_TENTHS   = 0.3;
        private const double TWO_TENTHS     = 0.2;
        private const double ONE_TENTHS     = 0.1;
        private static int size             = 20;
        private static int increment        = 1;
        private static double lessThan50    = 1;
        private static Random rnd           = new Random();

        private static ValueTuple<Item, double> mostSimilarItem;
        private static ValueTuple<Item, double> prevItem;
        private static Dictionary<double, Dictionary<string, Item>> SimilarityTable 
                                            = new Dictionary<double, Dictionary<string, Item>>();

        static void Main(string[] args)
        {
          
            SimilarityTable.Add(TEN_TENTHS,     new Dictionary<string, Item>());
            SimilarityTable.Add(NINE_TENTHS,    new Dictionary<string, Item>());
            SimilarityTable.Add(EIGHT_TENTHS,   new Dictionary<string, Item>());
            SimilarityTable.Add(SEVEN_TENTHS,   new Dictionary<string, Item>());
            SimilarityTable.Add(SIX_TENTHS,     new Dictionary<string, Item>());
            SimilarityTable.Add(FIVE_TENTHS,    new Dictionary<string, Item>());
            SimilarityTable.Add(FOUR_TENTHS,    new Dictionary<string, Item>());
            SimilarityTable.Add(THREE_TENTHS,   new Dictionary<string, Item>());
            SimilarityTable.Add(TWO_TENTHS,     new Dictionary<string, Item>());
            SimilarityTable.Add(ONE_TENTHS,     new Dictionary<string, Item>());
            SimilarityTable.Add(0.0,            new Dictionary<string, Item>());

            Item item = new Item();
            MakeItem(ref item);
            SimilarityTable[TEN_TENTHS].Add(item.Name, item);
            Init(item);
            Console.WriteLine(maxLevel);
            Console.WriteLine("Least Value "+lessThan50);

            Console.ReadKey();
        }

        private static void Init(Item item)
        {
            for (int i = size; i > 0; i--)
            {
                mostSimilarItem = new ValueTuple<Item, double>(null, 0);
                prevItem = new ValueTuple<Item, double>(null, 0);
                Item newItem = new Item();
                maxHelper = 1;
                MakeItem(ref newItem);
                Console.WriteLine(" ---- New item is has been created : " + newItem.Name);
                var testResult = Similarity(item, newItem);
                mostSimilarItem = (item, testResult);
                AddNodes(item, newItem, testResult);
                Console.WriteLine(" Finished with item : " + newItem.Name);
                Console.WriteLine(" Parent is : " + mostSimilarItem.Item1.Name);
                Console.WriteLine();
                if (maxLevel < maxHelper)
                {
                    maxLevel = maxHelper;
                    maxHelper = 1;
                }
                
            }
        }

        private static void MakeItem(ref Item newItem)
        {
            newItem.Name = "Movie" + increment;
            newItem.Feature.Preferences.Add("Drama", rnd.Next(1, 100));
            newItem.Feature.Preferences.Add("Action", rnd.Next(1, 100));
            newItem.Feature.Preferences.Add("Comedy", rnd.Next(1, 100));
            increment++;
        }

        private static void AddNodes(Item item, Item newItem, double similarityValue)
        {
            foreach (var vItem in item.ConnectedItem.Values)
            {
                mostSimilarItem = (item, similarityValue);
                if (vItem == null) { continue; }
                var similarityResult = Similarity(vItem, newItem);
                if (similarityResult > prevItem.Item2 || prevItem.Item1 == mostSimilarItem.Item1)
                {
                    prevItem = (vItem, similarityResult);
                }
                if (similarityResult < FIVE_TENTHS && lessThan50> similarityResult) {
                    lessThan50 = similarityResult;// for debuging remove later
                }
                if ( similarityResult > mostSimilarItem.Item2)
                {
                    Console.WriteLine(vItem.Name + " Yes is more similar than "
                        + mostSimilarItem.Item1.Name
                        + " and the next test will happen with the child of "
                        + vItem.Name + " if there is ");
                    AddNodes(vItem, newItem, similarityResult);
                    return;
                }
                else
                {
                    Console.WriteLine(mostSimilarItem.Item1.Name +" the parent is most similar item to " +newItem.Name);
                }
            }
            EnsureEmptyNode(mostSimilarItem.Item1, newItem);
        }

        private static void EnsureEmptyNode(Item item, Item newItem)
        {
            var calculateIndex = Math.Floor(mostSimilarItem.Item2 * Math.Pow(10, 1)) / Math.Pow(10, 1);

            if (mostSimilarItem.Item1.ConnectedItem[calculateIndex] == null)
            {
                maxHelper++;
                SimilarityTable[calculateIndex].Add(newItem.Name, newItem);
                mostSimilarItem.Item1.ConnectedItem[calculateIndex] = newItem;
            }
            else
            {
                maxHelper+=2;
                SimilarityTable[calculateIndex].Add(newItem.Name, newItem);
                //mostSimilarItem.Item1.ConnectedItem[calculateIndex]
                    //.ConnectedItem[Math.Floor(prevItem.Item2 * Math.Pow(10, 1)) / Math.Pow(10, 1)] = newItem;
                newItem.ConnectedItem[Math.Floor(prevItem.Item2* Math.Pow(10, 1)) / Math.Pow(10, 1)] = mostSimilarItem.Item1.ConnectedItem[calculateIndex];
                mostSimilarItem.Item1.ConnectedItem[calculateIndex] = newItem;
            }
        }

        private static double Similarity(Item item, Item newItem)
        {
            var prefFirstValues = (new List<double>(item.Feature.Preferences.Values)).ToArray();
            var prefSecValues = (new List<double>(newItem.Feature.Preferences.Values)).ToArray();

            prefFirstValues = prefFirstValues.Normalize();
            prefSecValues = prefSecValues.Normalize();

            for (int i = 0; i < prefFirstValues.Length; i++)
            {  
                Console.WriteLine(item.Name+" : " + prefFirstValues[i] + ", " + newItem.Name + " : " + prefSecValues[i]);

            }

            Console.WriteLine(1 - Distance.Cosine(prefFirstValues, prefSecValues));
            return 1 - Distance.Cosine(prefFirstValues, prefSecValues);
        }
    }
}






























/*
 *  if (item.ConnectedItem.Count == 0)
            {
                item.ConnectedItem.Add(newItem.Name, newItem);
            }

    */


/**

//Main matrix 
double[,] itemMatrix = 
{
 //CategorieWeight1     //CategorieWeight2 
 { 2,                   1},//Item1
 { 2,                   3},//Item2
 { 3,                   2},//Item3
 //{ 1,                 2},//Item4
 
};

double[,] CategoriesMatrix =
{
 //CategorieWeight 
 { 1, 0 },//Path1
 { 1, 1 },//Path2
 { 1, 1 },//Path3
 { 0, 1 },//Path3
};

double[,] ResultMatrix = itemMatrix.DotWithTransposed(CategoriesMatrix);
int countCol = 0;
foreach (var t in ResultMatrix)
{
    if(countCol == 4)
    {
        Console.Write("\n");
        countCol = 0;
    }
    Console.Write(t + " ");
    countCol++;
}
Console.ReadKey();
*/
