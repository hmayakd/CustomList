using System;
using System.Collections;

namespace CustomList
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region int type _List testing
            _List<int> _list = new _List<int>();
            _list.Add(2);
            _list.Add(22);
            _list.Add(222);

            int a = _list[0];
            _list[0] = 12;
            Console.WriteLine(_list.Capacity);
            Console.WriteLine(_list.Count);

            _List<int> _list1 = new _List<int>(7);
            _list1.Add(3);
            _list1[0] = 13;

            int[] arr = new int[10];
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = i;
            }
            int[] arr1 = new int[5];
            for (int i = 0; i < arr1.Length; i++)
            {
                arr1[i] = i + 2;
            }
            int[] arr2 = new int[10];
            int[] arr3 = new int[11];
            int[] arr4 = new int[17];
            _List<int> _list2 = new _List<int>(arr);
            _List<int> _list3 = new _List<int>();
            _list3.AddRange(arr);

            _List<int> _list4 = new _List<int>();
            _list4.AddRange(arr);
            _list4.Insert(3, 4);

            _List<int> _list5 = new _List<int>();
            _list5.AddRange(arr);
            _list5.InsertRange(2, arr1);

            _list.Remove(12);
            _list.RemoveAt(1);
            _list2.RemoveRange(1, 2);

            _list3.Reverse();
            _list4.Reverse(1, 3);

            _list5.Clear();

            bool c = _list4.Contains(3);

            _list4.CopyTo(arr3);
            _list3.CopyTo(arr4, 6);
            _list2.CopyTo(2, arr, 3, 4);

            _list4[7] = 3;
            _list4[9] = 3;
            int b = _list3.IndexOf(3);
            b = _list4.IndexOf(3, 5);
            b = _list4.IndexOf(3, 5, 4);

            b = _list4.LastIndexOf(3);
            b = _list4.LastIndexOf(3, 4);
            b = _list4.LastIndexOf(3, 9, 3);


            arr2 = _list2.ToArray();

            foreach (var item in _list2)
            {
                Console.WriteLine(item);
            }
            #endregion

            #region string type _List testing
            _List<string> _list10 = new _List<string>();
            _list10.Add("a2");
            _list10.Add("a22");
            _list10.Add("a222");

            string a10 = _list10[0];
            _list10[0] = "a12";
            Console.WriteLine(_list10.Capacity);
            Console.WriteLine(_list10.Count);

            _List<string> _list11 = new _List<string>(7);
            _list11.Add("a3");
            _list11[0] = "a13";

            string[] arr10 = new string[10];
            for (int i = 0; i < arr10.Length; i++)
            {
                arr10[i] = $"a{i}";
            }
            string[] arr11 = new string[5];
            for (int i = 0; i < arr11.Length; i++)
            {
                arr11[i] = $"a{i + 2}";
            }
            string[] arr12 = new string[10];
            string[] arr13 = new string[11];
            string[] arr14 = new string[17];
            _List<string> _list12 = new _List<string>(arr10);
            _List<string> _list13 = new _List<string>();
            _list13.AddRange(arr10);

            _List<string> _list14 = new _List<string>();
            _list14.AddRange(arr10);
            _list14.Insert(3, "a4");

            _List<string> _list15 = new _List<string>();
            _list15.AddRange(arr10);
            _list15.InsertRange(3, arr11);

            _list10.Remove("a12");
            _list10.RemoveAt(1);
            _list12.RemoveRange(1, 2);

            _list13.Reverse();
            _list14.Reverse(1, 3);

            _list15.Clear();

            bool c10 = _list14.Contains("a3");

            _list14.CopyTo(arr13);
            _list13.CopyTo(arr14, 6);
            _list12.CopyTo(2, arr10, 3, 4);

            _list14[7] = "a3";
            _list14[9] = "a3";
            int b10 = _list13.IndexOf("a3");
            b10 = _list14.IndexOf("a3", 5);
            b10 = _list14.IndexOf("a3", 5, 4);

            b10 = _list14.LastIndexOf("a3");
            b10 = _list14.LastIndexOf("a3", 4);
            b10 = _list14.LastIndexOf("a", 9, 3);


            arr12 = _list12.ToArray();

            foreach (var item in _list12)
            {
                Console.WriteLine(item);
            }
            #endregion
        }
    }
}