﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using OctoCTypes;
using System;
using System.Collections;
using System.Collections.Generic;

namespace OctoCTests
{
	[TestClass]
	public class ArrayListTests
	{
		[TestMethod]
		public void EmptyAL()
		{
			var arrayList = new ArrayList<int>();
			Assert.IsNotNull(arrayList, "ArrayList was not instantiated empty");
			Assert.AreEqual(0, arrayList.Count, "Empty ArrayList should have no elements.");
		}

		[TestMethod]
		public void OneElementAL()
		{
			var arrayList = new ArrayList<int>(1);
			Assert.IsNotNull(arrayList, "ArrayList was not instantiated with an element");
			Assert.AreEqual(1, arrayList.Count, "ArrayList should have one element.");
			Assert.AreEqual(1, arrayList[0]);
		}

		[TestMethod]
		public void ArrayAL()
		{
			var arrayList = new ArrayList<int> { 0, 2, 3 };
			Assert.AreEqual(0, arrayList[0]);
			Assert.AreEqual(2, arrayList[1]);
			Assert.AreEqual(3, arrayList[2]);
			Assert.AreEqual(3, arrayList.Count);
		}

		[TestMethod]
		public void AddAL()
		{
			var arrayList = new ArrayList<int>();
			Assert.AreEqual(0, arrayList.Count, "Empty ArrayList should be empty");
			arrayList.Add(0);
			Assert.AreEqual(1, arrayList.Count, "ArrayList should have one added element");
			arrayList.Add(1);
			Assert.AreEqual(2, arrayList.Count, "ArrayList should have two added elements");
			arrayList.Add(2);
			Assert.AreEqual(3, arrayList.Count, "ArrayList should have three added elements");
		}

		[TestMethod]
		public void ValueExceptionsAL()
		{
			var arrayList = new ArrayList<int>(0);
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => arrayList[-1]);
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => arrayList[1]);
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => arrayList[-1] = -1);
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => arrayList[1] = 0);
		}

		[TestMethod]
		public void ValueAL()
		{
			var arrayList = new ArrayList<int>(0);
			Assert.AreEqual(0, arrayList[0]);
			arrayList[0] = 1;
			Assert.AreEqual(1, arrayList[0]);
		}

		[TestMethod]
		public void ClearAL()
		{
			var arrayList = new ArrayList<int>(0);
			Assert.AreEqual(1, arrayList.Count);
			Assert.AreEqual(0, arrayList[0]);
			arrayList.Clear();
			Assert.AreEqual(0, arrayList.Count);
		}

		[TestMethod]
		public void ClearExceptionsAL()
		{
			var arrayList = new ArrayList<int>(0);
			arrayList.Clear();
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => arrayList[arrayList.Count]);
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => arrayList[-1]);
		}

		[TestMethod]
		public void ContainsAL()
		{
			var arrayList = new ArrayList<int> { 0, 1, 1, 2, 3, 5, 8, 13, 21, 34, 55 };
			for (var i = 0; i < arrayList.Count; i++)
			{
				Assert.IsTrue(arrayList.Contains(arrayList[i]));
			}
			Assert.IsFalse(arrayList.Contains(4));

		}

		[TestMethod]
		public void Contains16kAL()
		{
			var multiplier = 128;
			var arrayList = new ArrayList<int>();
			for (var i = 0; i < multiplier * multiplier; i++) arrayList.Add(i);
			Assert.AreEqual(multiplier * multiplier, arrayList.Count);
			for (var i = 0; i < multiplier; i++) Assert.IsTrue(arrayList.Contains(arrayList[arrayList.Count - 1 - i]));
			Assert.IsFalse(arrayList.Contains(multiplier * multiplier));
		}

		[TestMethod]
		public void CopyToAL()
		{
			var arrayList = new ArrayList<int>();
			for (var i = 0; i < 10; i++) arrayList.Add(i);
			
			var targetArray = new int[11];
			int start = 1;
			arrayList.CopyTo(targetArray, start);
			for (var i = 0; i < arrayList.Count; i++) Assert.AreEqual(arrayList[i], targetArray[start + i]);
		}

		[TestMethod]
		public void CopyToExceptionsAL()
		{
			var arrayList = new ArrayList<int>();
			for (var i = 0; i < 10; i++) arrayList.Add(i);
			int[] targetArray = null;
			Assert.ThrowsException<ArgumentNullException>(() => arrayList.CopyTo(targetArray, 0));
			targetArray = new int[11];
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => arrayList.CopyTo(targetArray, -1));
			Assert.ThrowsException<ArgumentException>(() => arrayList.CopyTo(targetArray, 11));
			Assert.ThrowsException<ArgumentException>(() => arrayList.CopyTo(targetArray, 2));
		}

		[TestMethod]
		public void IndexOfAL()
		{
			var arrayList = new ArrayList<int>();
			for (var i = 0; i < 10; i++) arrayList.Add(2 * i);

			for (var i = 0; i < 10; i++) Assert.AreEqual(i, arrayList.IndexOf(2 * i));
		}

		[TestMethod]
		public void InsertAL()
		{
			var arrayList = new ArrayList<int>();
			for (var i = 0; i < 10; i++) arrayList.Add(2 * i);
			for (var i = 0; i < 10; i++) arrayList.Insert(2 * i, 2 * i - 1);

			for (var i = 0; i < 20; i++) Assert.AreEqual(i - 1, arrayList[i]);
		}

		[TestMethod]
		public void InsertExceptionsAL()
		{
			var arrayList = new ArrayList<int>(0);
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => arrayList.Insert(arrayList.Count, 0));
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => arrayList.Insert(-1, 0));
		}

		[TestMethod]
		public void RemoveAtReverseAL()
		{
			var arrayList = new ArrayList<int>();
			for (var i = 0; i < 10; i++) arrayList.Add(i);
			var count = arrayList.Count;
			Assert.AreEqual(10, count);
			for (var i = 0; i < 10; i++) Assert.AreEqual(i, arrayList[i]);

			for (var i = 10; i > 0; i--)
			{
				arrayList.RemoveAt(i - 1);
				count--;
				Assert.AreEqual(count, arrayList.Count);

				for (var j = 0; j < arrayList.Count; j++) Assert.AreEqual(j, arrayList[j]);
			}
		}

		[TestMethod]
		public void RemoveAtBetweenAL()
		{
			var arrayList = new ArrayList<int>();
			for (var i = 0; i < 10; i++) arrayList.Add(i);
			var count = arrayList.Count;
			Assert.AreEqual(10, count);
			for (var i = 0; i < 10; i++) Assert.AreEqual(i, arrayList[i]);

			for (var i = 4; i >= 0; i--)
			{
				arrayList.RemoveAt(2 * i + 1);
				count--;
				Assert.AreEqual(count, arrayList.Count);
			}
			for (var j = 0; j < 5; j++) Assert.AreEqual(2 * j, arrayList[j]);
		}

		[TestMethod]
		public void RemoveAtExceptionsAL()
		{
			var arrayList = new ArrayList<int>();
			for (var i = 0; i < 10; i++) arrayList.Add(i);
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => arrayList.RemoveAt(-1));
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => arrayList.RemoveAt(10));
		}

		[TestMethod]
		public void RemoveAL()
		{
			var arrayList = new ArrayList<int>();
			for (var i = 0; i < 10; i++) arrayList.Add(i);
			var count = arrayList.Count;
			Assert.AreEqual(10, count);
			for (var i = 0; i < 10; i++) Assert.AreEqual(i, arrayList[i]);

			Assert.IsTrue(arrayList.Remove(3));
			var offset = 0;
			for (var i = 0; i < arrayList.Count; i++)
			{
				if (i != 3) Assert.AreEqual(i + offset, arrayList[i]);
				else offset++;
			}
			Assert.AreEqual(false, arrayList.Contains(3));
			Assert.IsFalse(arrayList.Remove(3));
		}

		[TestMethod]
		public void IEnumeratorTAL()
		{
			var arrayList = new ArrayList<int>();
			for (var i = 0; i < 5; i++) arrayList.Add(i);
			IEnumerator<int> intEnum = arrayList.GetEnumerator();
			int count = 0;

			Assert.AreEqual(default(int), intEnum.Current);
			while (intEnum.MoveNext())
				Assert.AreEqual(count++, intEnum.Current);
			intEnum.Reset();
			Assert.AreEqual(default(int), intEnum.Current);
			intEnum.Dispose();
		}

		[TestMethod]
		public void IEnumeratorAL()
		{
			var arrayList = new ArrayList<int>();
			for (var i = 0; i < 5; i++) arrayList.Add(i);
			IEnumerator intEnum = ((IEnumerable)arrayList).GetEnumerator();
			int count = 0;

			Assert.AreEqual(default(int), intEnum.Current);
			while (intEnum.MoveNext())
				Assert.AreEqual(count++, intEnum.Current);
			intEnum.Reset();
			Assert.AreEqual(default(int), intEnum.Current);
		}

		[TestMethod]
		public void IsNotReadOnlyAL()
		{
			var arrayList = new ArrayList<int>();
			Assert.IsFalse(arrayList.IsReadOnly);
		}
	}
}
