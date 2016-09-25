namespace Search
{
	using System;
	using System.Collections.Generic;

	public class Frontier<Key, Value> where Key:IComparable 
	{
		/*! Properties */
		private SortedList<Key, List<Value> > frontier;
		private IList<Key> keys;

		public Frontier()
		{
			frontier = new SortedList<Key, List<Value> >();
			keys = frontier.Keys;
		}

		/*!
		 * Append an value with a given key to the frontier
		 *
		 * @param {Key} k - key for the value
		 * @param {Value} v - the value 
		 * @returns void
		 */
		public void Append(Key k, Value v)
		{
			//! first check if the frontier Contains
			//! the given key. If so, simply add the
			//! value to the end of the List. 
			//! If not, a new List<Value> needs to 
			//! be created, the value added to it,
			//! and the list added to the frontier
			//! at the given key 
			if (frontier.Contains(k))
			{
				frontier[k].Add(v);
			} else
			{
				List<Value> list = new List<Value>();
				list.Add(v);
				frontier(k, list);
			}
		}

		/*!
		 * Remove an element from the Frontier. This will
		 * remove the first element of the list
		 * with the smallest key value
		 *
		 * @returns {Value} val
		 */
		public Value Pop()
		{
			//! keys sorts the frontier keys in order
			//! so keys[0] is the smallest key
			Value val = frontier[keys[0]][0];
			//! remove the element from the List 
			frontier[keys[0]].RemoveAt(0);
			//! if the List at keys[0] is empty,
			//! remove it from the SortedList
			if (frontier[keys[0]].Count == 0) frontier.Remove(keys[0]);
			return val;
		}


	}
}
