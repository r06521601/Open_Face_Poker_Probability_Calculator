using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace poker_interface
{
    static class Monte_carlo
    {

        public static string calculator(List<String> hand_cards, List<String> other_cards)
        {
            List<double> prime_number = new List<double> { 41, 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37 };
            List<int> nonuniqueproduct = Data.Get_nonuniqureproduct();
            List<int> nonuniquevalue = Data.Get_nonuniqurevalue();
            List<double> poker = new List<double>() { 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4 };
            List<double> unique_poker = new List<double>() { 41, 41, 41, 41, 2, 2, 2, 2, 3, 3, 3, 3, 5, 5, 5, 5, 7, 7, 7, 7, 11, 11, 11, 11, 13, 13, 13, 13, 17, 17, 17, 17, 19, 19, 19, 19, 23, 23, 23, 23, 29, 29, 29, 29, 31, 31, 31, 31, 37, 37, 37, 37 };
            List<double> flush = new List<double>() { 13, 13, 13, 13 };
            List<string> unique_flush = new List<string>() { "s", "s", "s", "s", "s", "s", "s", "s", "s", "s", "s", "s", "s", "d", "d", "d", "d", "d", "d", "d", "d", "d", "d", "d", "d", "d", "h", "h", "h", "h", "h", "h", "h", "h", "h", "h", "h", "h", "h", "c", "c", "c", "c", "c", "c", "c", "c", "c", "c", "c", "c", "c" };
            string promt = "\n";
            List<double> give_number = new List<double>();
            List<double> know_card = new List<double>();
            List<String> give_flush = new List<string>();
            List<String> know_flush = new List<string>();
            foreach (String card in hand_cards)
            {

                give_flush.Add(card.Substring(2));

                give_number.Add(int.Parse(card.Remove(2)));

            }
            foreach (String card in other_cards)
            {

                know_flush.Add(card.Substring(2));


                know_card.Add(int.Parse(card.Remove(2)));

            }
            know_card = know_card.Concat(give_number).ToList();

            List<double> no_poker = new List<double>();
            for (int i = 0; i < prime_number.Count; i++)
            {
                if (know_card.CountTimes<double>(prime_number[i]) == 4)
                {
                    no_poker.Add(prime_number[i]);
                }
            }
            List<int> probability = new List<int>();
            List<int> probability_rank = new List<int>();
            int flush_probability = 0;
            bool flush_or_no = false;
            List<int> give_flush_list = new List<int> { 0, 0, 0, 0 };
            give_flush_list[0] = Probability.CountTimes<string>(give_flush, "s");

            give_flush_list[1] = Probability.CountTimes<string>(give_flush, "d");

            give_flush_list[2] = Probability.CountTimes<string>(give_flush, "h");
            give_flush_list[3] = Probability.CountTimes<string>(give_flush, "c");
            string[] flush_vs = { "s", "d", "h", "c" };
            if (Probability.CountTimes<int>(give_flush_list, 0) == 3)
            {
                flush_or_no = true;
                List<String> know_flush_sum = new List<string>(know_flush.Concat(give_flush).ToList());
                List<int> know_flush_list = new List<int> { 0, 0, 0, 0 };
                know_flush_list[0] = Probability.CountTimes<string>(know_flush_sum, "s");
                know_flush_list[1] = Probability.CountTimes<string>(know_flush_sum, "d");
                know_flush_list[2] = Probability.CountTimes<string>(know_flush_sum, "h");
                know_flush_list[3] = Probability.CountTimes<string>(know_flush_sum, "c");
                
                for (int i = 0; i < know_flush_list.Count(); i++)
                {
                    for (int j = 0; j < know_flush_list[i]; j++)
                    {
                        unique_flush.Remove(flush_vs[i]);
                    }                   
                }
            }
            
            //monte_carlo_start()
            int round = (52-know_card.Count() - 18) / 2 / 3;
            
            List<double> turn_number = new List<double>();
            for (int i = 0; i < give_number.Count(); i++)
            {
                turn_number.Add(prime_number[int.Parse(((give_number[i]) - 1).ToString())]);
            }

            for (int i = 0; i < know_card.Count(); i++)
            {
                unique_poker.Remove(prime_number[int.Parse(((know_card[i]) - 1).ToString())]);
            }
            double RoyalFlush = 0;
            double StraightFlush = 0;
            double FourOfAKind = 0;
            double FullHouse = 0;
            double Flush = 0;
            double Straight = 0;
            double ThreeOfAKind = 0;
            double TwoPair = 0;
            double OnePair = 0;
            double give_unique_number = 1;
            for (int i = 0; i < turn_number.Count(); i++)
            {
                give_unique_number *= turn_number[i];
            }
            // loop_start()
            int monte_carlo_num = 2000;
            for (int m = 0; m < monte_carlo_num; m++)
            {
                List<double> monte_unique_poker = new List<double>(unique_poker);
                List<string> monte_unique_flush = new List<string>(unique_flush);
                List<double> deck = new List<double>();
                List<string> flush_deck = new List<string>();
                for (int i = 0; i < round; i++)
                {

                    Random rnd = new Random();
                    Random rnd_2 = new Random();
                    foreach (double item in monte_unique_poker.OrderBy(x => rnd.Next(monte_unique_poker.Count)).Take(3))  monte_unique_poker.Remove(item);
                    foreach (double item in monte_unique_poker.OrderBy(x => rnd.Next(monte_unique_poker.Count)).Take(3)) { deck.Add(item); monte_unique_poker.Remove(item); }
                    if (flush_or_no)
                    {
                        foreach (string item in monte_unique_flush.OrderBy(x => rnd.Next(monte_unique_flush.Count)).Take(3)) monte_unique_flush.Remove(item);
                        foreach (string item in monte_unique_flush.OrderBy(x => rnd.Next(monte_unique_flush.Count)).Take(3)) { flush_deck.Add(item); monte_unique_flush.Remove(item); }
                    }
                }
                double unique_number = 1;
                
                for (int i = 0; i < deck.Count(); i++)
                {
                    unique_number *= deck[i];
                }
                
                
                
                for (int i = 0; i < nonuniqueproduct.Count(); i++)
                {
                    
                    if (((double.Parse(nonuniqueproduct[i].ToString())) / give_unique_number % 1) == 0)
                    {
                        if (((unique_number / double.Parse(nonuniqueproduct[i].ToString())) % 1) == 0)
                        {
                            probability.Add(nonuniqueproduct[i]);
                            probability_rank.Add(nonuniquevalue[i]);
                        }
                    }
                }
                int h = 0;
                foreach (int i in give_flush_list)
                {
                    if (i!=0)
                    {
                        
                        if (flush_deck.CountTimes<string>(flush_vs[h]) > 4)
                        {
                            flush_probability += 1;
                        }
                    }
                    h += 1;
                }



            }
            //loop_end()
            for (int i = 0; i < probability.Count(); i++)
            {
                if (probability_rank[i] < 1 + 1)
                {
                    RoyalFlush += 1;
                }
                else if (probability_rank[i] < 10 + 1 && probability_rank[i] > 1)
                {
                    StraightFlush += 1;
                }
                else if (probability_rank[i] < 166 + 1 && probability_rank[i] > 10)
                {
                    FourOfAKind += 1;
                }
                else if (probability_rank[i] < 322 + 1 && probability_rank[i] > 166)
                {
                    FullHouse += 1;
                }
                else if (probability_rank[i] < 1609 + 1 && probability_rank[i] > 1599)
                {
                    Straight += 1;
                }
                else if (probability_rank[i] < 2467 + 1 && probability_rank[i] > 1609)
                {
                    ThreeOfAKind += 1;
                }
                else if (probability_rank[i] < 3325 + 1 && probability_rank[i] > 2467)
                {
                    TwoPair += 1;
                }
                else if (probability_rank[i] < 6185 + 1 && probability_rank[i] > 3325)
                {

                    OnePair += 1;
                }
            }
            
            
            
                //promt += "straight_flush: " + StraightFlush*3  + "%\n";
                if (FourOfAKind / monte_carlo_num * 100<100)
                    promt += "four_of_kind: " + (FourOfAKind / monte_carlo_num * 100).ToString("#0.00") + "%\n";
                else
                    promt += "four_of_kind: " + "#####" + "%\n";
                if (FullHouse / monte_carlo_num * 100 < 100)
                    promt += "house: " + (FullHouse/ monte_carlo_num * 100).ToString("#0.00") + "%\n";
                else
                    promt += "house: " + "#####" + "%\n";
            Flush = flush_probability;
                if (Flush / monte_carlo_num * 100 < 100)
                    promt += "flush: " + (Flush/ monte_carlo_num * 100).ToString("#0.00") + "%\n";
                else
                    promt += "flush: " + "#####" + "%\n";
                if (Straight / monte_carlo_num * 100 < 100)
                    promt += "straight: " + (Straight / monte_carlo_num * 100).ToString("#0.00") + "%\n";
                else
                    promt += "straight: " + "#####" + "%\n";
                if (ThreeOfAKind / monte_carlo_num * 100 < 100)
                    promt += "three_of_kind: " + (ThreeOfAKind / monte_carlo_num * 100).ToString("#0.00") + "%\n";
                else
                    promt += "three_of_kind: " + "#####" + "%\n";
                if (TwoPair / monte_carlo_num * 100 < 100)
                    promt += "two_pairs: " + (TwoPair / monte_carlo_num * 100).ToString("#0.00") + "%\n";
                else
                    promt += "two_pairs: " + "#####" + "%\n";
                if (OnePair / monte_carlo_num * 100 < 100)
                    promt += "one_pair: " + (OnePair / monte_carlo_num * 100).ToString("#0.00") + "%\n";
                else
                    promt += "one_pair: " + "#####" + "%\n";
            

            return promt;
            //Monte_carlo_End()






        }


        public static List<double> getChildren(int num, List<double> children_list)
        {
            bool iszhishu = true;
            int i = 2;
            double square = (Math.Sqrt(num) + 1);
            while (i < square + 1)
            {
                if (num % i == 0)
                {
                    children_list.Add(i);
                    iszhishu = false;
                    getChildren(num / i, children_list);
                    i += 1;
                    break;
                }
                i += 1;
            }
            if (iszhishu == true)
            {
                children_list.Add(num);
            }
            return children_list;

        }
        public static double get_probability(int num, List<double> turn_give_number, List<double> prime_number, List<double> poker)
        {
            double probability_value = 1;
            List<double> children_list = new List<double>();
            children_list = getChildren(num, children_list);
            List<double> need_number = children_list;
            ;
            for (int i = 0; i < turn_give_number.Count(); i++)
            {
                if (children_list.Contains(turn_give_number[i]))
                {
                    need_number.Remove(turn_give_number[i]);
                }
            }
            List<int> need_list = new List<int>();
            for (int i = 0; i < prime_number.Count(); i++)
            {
                need_list.Add(0);
                for (int j = 0; j < need_number.Count(); j++)
                {
                    if (prime_number[i] == need_number[j])
                    {
                        need_list[i] += 1;
                    }
                }
            }

            for (int i = 0; i < need_list.Count(); i++)
            {
                if (need_list[i] <= poker[i] && need_list[i] != 0)
                {
                    for (int j = 0; j < need_list[i]; j++)
                    {

                        probability_value *= (poker[i] - j) / (poker.Sum() - j);
                    }
                }
                else if (need_list[i] == 0)
                {

                }
                else
                {
                    break;
                }
            }
            if (probability_value == 1)
            {
                probability_value = 0;
            }
            return probability_value;
        }
        //public static int CountTimes<T>(this List<T> inputList, T searchItem) { return ((from t in inputList where t.Equals(searchItem) select t).Count()); }
        public static double Probability_flush(List<String> give_flush, List<String> know_flush, List<double> flush)
        {

            double flush_probability_value = 1;
            List<int> give_flush_list = new List<int> { 0, 0, 0, 0 };
            give_flush_list[0] = Probability.CountTimes<string>(give_flush, "s");

            give_flush_list[1] = Probability.CountTimes<string>(give_flush, "d");

            give_flush_list[2] = Probability.CountTimes<string>(give_flush, "h");
            give_flush_list[3] = Probability.CountTimes<string>(give_flush, "c");
            if (Probability.CountTimes<int>(give_flush_list, 0) == 3)
            {
                List<String> know_flush_sum = know_flush.Concat(give_flush).ToList();
                List<int> know_flush_list = new List<int> { 0, 0, 0, 0 };
                know_flush_list[0] = Probability.CountTimes<string>(know_flush_sum, "s");
                know_flush_list[1] = Probability.CountTimes<string>(know_flush_sum, "d");
                know_flush_list[2] = Probability.CountTimes<string>(know_flush_sum, "h");
                know_flush_list[3] = Probability.CountTimes<string>(know_flush_sum, "c");
                for (int i = 0; i < know_flush_list.Count; i++)
                {

                    flush[i] = flush[i] - know_flush_list[i];
                }
                for (int i = 0; i < give_flush_list.Count; i++)
                {
                    if (give_flush_list[i] != 0)
                    {
                        for (int j = 0; j < 5 - give_flush_list[i]; j++)
                        {

                            flush_probability_value *= (flush[i] - j) / (flush.Sum() - j);

                        }
                    }
                }
            }
            if (flush_probability_value == 1)
            {
                flush_probability_value = 0;
            }
            return flush_probability_value;
        }
    }
}

