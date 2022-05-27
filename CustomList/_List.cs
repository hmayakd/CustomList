using System;
using System.Collections;
using System.Collections.Generic;

namespace CustomList
{
    public class _List
    {
        private const int _defaultCapacity = 4;
        public int[] _items;
        private int _size;
        private int _version;
        static readonly int[] _emptyArray = new int[0];
        public _List()
        {
            _items = _emptyArray;
        }
        public _List(int capacity)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException();
            if (capacity == 0)
                _items = _emptyArray;
            else
                _items = new int[capacity];
        }
        public _List(IEnumerable<int> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException();
            }
            ICollection<int> c = collection as ICollection<int>;
            if (c != null)
            {
                int count = c.Count;
                if (count == 0)
                {
                    _items = _emptyArray;
                }
                else
                {
                    _items = new int[count];
                    c.CopyTo(_items, 0);
                    _size = count;
                }
            }
            else
            {
                _size = 0;
                _items = _emptyArray;
                using (IEnumerator<int> en = collection.GetEnumerator())
                {
                    while (en.MoveNext())
                    {
                        Add(en.Current);
                    }
                };
            }

        }
        public int this[int index]
        {
            get
            {
                if ((uint)index >= (uint)_size)
                {
                    throw new ArgumentException("Index was not out of bounds");
                }
                return _items[index];
            }
            set
            {
                if ((uint)index >= (uint)_size)
                {
                    throw new ArgumentException("Index was not out of bounds");
                }
                _items[index] = value;
                _version++;
            }
        }
        public int Capacity
        {
            get
            {
                return _items.Length;
            }
            set
            {
                if (value < _size)
                {
                    throw new ArgumentOutOfRangeException();
                }
                if (value != _items.Length)
                {
                    if (value > 0)
                    {
                        int[] newItems = new int[value];
                        if (_size > 0)
                        {
                            Array.Copy(_items, 0, newItems, 0, _size);
                        }
                        _items = newItems;
                    }
                    else
                    {
                        _items = _emptyArray;
                    }
                }
            }
        }
        public int Count
        {
            get
            {
                return _size;
            }
        }
        private static bool IsCompatibleObject(object value)
        {
            return (value is int);
        }
        public void Add(int item)
        {
            if (_size == _items.Length) EnsureCapacity(_size + 1);
            _items[_size++] = item;
            _version++;
        }
        public void AddRange(IEnumerable<int> collection)
        {
            InsertRange(_size, collection);
        }
        public void Insert(int index, int item)
        {
            if ((uint)index > (uint)_size)
            {
                throw new ArgumentOutOfRangeException();
            }
            if (_size == _items.Length)
            {
                EnsureCapacity(_size + 1);
            }
            if (index < _size)
            {
                Array.Copy(_items, index, _items, index + 1, _size - index);
            }
            _items[index] = item;
            _size++;
            _version++;
        }
        public void InsertRange(int index, IEnumerable<int> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException();
            }
            if ((uint)index > (uint)_size)
            {
                throw new IndexOutOfRangeException();
            }
            ICollection<int> c = collection as ICollection<int>;
            if (c != null)
            {
                int count = c.Count;
                if (count > 0)
                {
                    EnsureCapacity(_size + count);
                    if (index < _size)
                    {
                        Array.Copy(_items, index + count, _items, index + count, _size - index);
                    }
                    if (this == c)
                    {
                        Array.Copy(_items, 0, _items, index, index);
                        Array.Copy(_items, index + count, _items, index * 2, _size - index);
                    }
                    else
                    {
                        int[] itemsToInsert = new int[count];
                        c.CopyTo(itemsToInsert, 0);
                        itemsToInsert.CopyTo(_items, index);
                    }
                    _size += count;
                }
            }
            else
            {
                using (IEnumerator<int> en = collection.GetEnumerator())
                {
                    while (en.MoveNext())
                    {
                        Insert(index++, en.Current);
                    }
                }
            }
            _version++;
        }
        public bool Remove(int item)
        {
            int index = IndexOf(item);
            if (index >= 0)
            {
                RemoveAt(index);
                return true;
            }
            return false;
        }
        public void RemoveAt(int index)
        {
            if ((uint)index >= (uint)_size)
            {
                throw new ArgumentOutOfRangeException();
            }
            _size--;
            if (index < _size)
            {
                Array.Copy(_items, index + 1, _items, index, _size - index);
            }
            _items[_size] = default(int);
            _version++;
        }
        public void RemoveRange(int index, int count)
        {
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            if (_size - index < count)
            {
                throw new ArgumentOutOfRangeException();
            }
            if (count > 0)
            {
                int i = _size;
                _size -= count;
                if (index < _size)
                {
                    Array.Copy(_items, index + count, _items, index, _size - index);
                }
                Array.Clear(_items, index, count);
                _version++;
            }
        }
        public void Reverse()
        {
            Reverse(0, Count);
        }
        public void Reverse(int index, int count)
        {
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            if (_size - index < count)
            {
                throw new ArgumentOutOfRangeException();
            }
            Array.Reverse(_items, index, count);
            _version++;
        }
        public void Clear()
        {
            if (_size > 0)
            {
                Array.Clear(_items, 0, _size);
                _size = 0;
            }
            _version++;
        }
        public bool Contains(int item)
        {
            if ((Object)item == null)
            {
                for (int i = 0; i < _size; i++)
                {
                    if ((Object)_items[i] == null)
                    {
                        return true;
                    }
                }
                return false;
            }
            else
            {
                EqualityComparer<int> c = EqualityComparer<int>.Default;
                for (int i = 0; i < _size; i++)
                {
                    if (c.Equals(_items[i], item))
                    {
                        return true;
                    }

                }
                return false;
            }
        }
        public void CopyTo(int[] array)
        {
            CopyTo(array, 0);
        }
        public void CopyTo(int index, int[] array, int arrayIndex, int count)
        {
            if (_size - index < count)
            {
                throw new ArgumentException();
            }
            Array.Copy(_items, index, array, arrayIndex, count);
        }
        public void CopyTo(int[] array, int arrayIndex)
        {
            Array.Copy(array, 0, _items, arrayIndex, _size);
        }
        public int IndexOf(int item)
        {
            return Array.IndexOf(_items, item, 0, _size);
        }
        public int IndexOf(int item, int index)
        {
            if (index > _size)
            {
                throw new ArgumentOutOfRangeException();
            }
            return Array.IndexOf(_items, item, index, _size - index);
        }
        public int IndexOf(int item, int index, int count)
        {
            if (index > _size)
            {
                throw new ArgumentOutOfRangeException();
            }
            if (count > 0 || index > _size - count)
            {
                throw new ArgumentOutOfRangeException();
            }
            return Array.IndexOf(_items, item, index, count);
        }
        public int LastIndexOf(int item)
        {
            if (_size == 0)
            {
                return -1;
            }
            else
            {
                return LastIndexOf(item, _size - 1, _size);
            }
        }
        public int LastIndexOf(int item, int index)
        {
            if (index >= _size)
            {
                throw new ArgumentOutOfRangeException();
            }
            return LastIndexOf(item, index, index + 1);
        }
        public int LastIndexOf(int item, int index, int count)
        {
            if ((Count != 0) && (index < 0))
            {
                throw new ArgumentOutOfRangeException();
            }
            if ((Count != 0) && (count < 0))
            {
                throw new ArgumentOutOfRangeException();
            }
            if (_size == 0)
            {
                return -1;
            }
            if (index >= _size)
            {
                throw new ArgumentOutOfRangeException();
            }
            if (count > index + 1)
            {
                throw new ArgumentOutOfRangeException();
            }
            return Array.LastIndexOf(_items, item, index, count);
        }
        public int[] ToArray()
        {
            int[] array = new int[_size];
            Array.Copy(_items, 0, array, 0, _size);
            return array;
        }
        public void EnsureCapacity(int min)
        {
            if (_items.Length < min)
            {
                int newCapacity = _items.Length == 0 ? _defaultCapacity : _items.Length * 2;
                if ((uint)newCapacity > 0X7FEFFFFF)
                    newCapacity = 0X7FEFFFFF;
                if (newCapacity < min) newCapacity = min;
                Capacity = newCapacity;
            }
        }
        public Enumerator GetEnumerator()
        {
            return new Enumerator(this);
        }
        public class Enumerator : IEnumerator
        {
            private _List list;
            private int index;
            private int version;
            private int current;
            public Enumerator(_List list)
            {
                this.list = list;
                index = 0;
                version = list._version;
                current = default(int);
            }
            public bool MoveNext()
            {
                _List localList = list;
                if (version == localList._version && (uint)index < (uint)localList._size)
                {
                    current = localList._items[index];
                    index++;
                    return true;
                }
                return MoveNextRare();
            }
            private bool MoveNextRare()
            {
                if (version != list._version)
                {
                    throw new InvalidOperationException();
                }
                index = list._size + 1;
                current = default(int);
                return false;
            }
            public object Current
            {
                get
                {
                    if (index == 0 || index == list._size + 1)
                    {
                        throw new InvalidOperationException();
                    }
                    return current;
                }
            }
            public void Reset()
            {
                if (version != list._version)
                {
                    throw new InvalidOperationException();
                }
                index = 0;
                current = default(int);
            }
        }
    }
}