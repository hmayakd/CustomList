﻿using System;
using System.Collections;

namespace CustomList
{
    internal class Program
    {
        static void Main(string[] args)
        {
            _List _list = new _List();
            _list.Add(2);
            int a = _list[0];
            _list[0] = 12;
            Console.WriteLine(_list.Capacity);
            Console.WriteLine(_list.Count);

            _List _list1 = new _List(7);
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
            _List _list2 = new _List(arr);
            _List _list3 = new _List();
            _list3.AddRange(arr);

            _List _list4 = new _List();
            _list4.AddRange(arr);
            _list4.Insert(3, 4);

            _List _list5 = new _List();
            _list5.AddRange(arr);
            _list5.InsertRange(2, arr1);

            _list.Remove(2);
            _list1.RemoveAt(0);
            _list2.RemoveRange(1, 2);

            _list3.Reverse();
            _list4.Reverse(1, 3);

            _list5.Clear();

            bool c = _list5.Contains(3);

            _list.CopyTo(arr);
            _list1.CopyTo(arr, 3);
            _list2.CopyTo(2, arr, 3, 4);

            int b = _list3.IndexOf(3);
            b = _list4.IndexOf(3, 2);
            b = _list5.IndexOf(3, 2, 3);

            b = _list3.LastIndexOf(3);
            b = _list4.LastIndexOf(3, 2);
            b = _list5.LastIndexOf(3, 2, 3);

            int[] arr2 = new int[10];
            arr2 = _list.ToArray();

            foreach (var item in _list2)
            {
                Console.WriteLine(item);
            }
        }
    }
}