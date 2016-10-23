namespace Search
{
	using System;
	using System.Collections.Generic;

	public class PriorityQueue<Key, Value> where Key:IComparable 
	{
		/*! Properties */
		private SortedList<Key, List<Value> > frontier;
		private IList<Key> keys;
		private Heuristics.Heurfun<Key, Value> heurfun;

		public PriorityQueue(Heuristics.Heurfun<Key, Value> hf)
		{
			frontier = new SortedList<Key, List<Value> >();
			keys = frontier.Keys;
			heurfun = hf;
		}

		public Key Heurfun(Value v)
		{
			return heurfun(v);
		}

		/*!
		 * Provide access to underlying frontier
		 */
		public SortedList<Key, List<Value> > GetPriorityQueue()
		{
			return frontier;
		}

		/*! Get total elements in the frontier. Based
		 * on number of distinct keys in SortedList, and
		 * the number of elements in each List<Value>
		 * at that key
		 *
		 */
		public int Count()
		{
			int count = 0, l = frontier.Count,
			    keyscount = keys.Count;
			for (int i = 0; i < keyscount; i++)
			{
				count += frontier[keys[i]].Count;
			}

			return count;
		}

		/*!
		 * Append an value with a given key to the frontier
		 *
		 * @param {Key} k - key for the value
		 * @param {Value} v - the value 
		 * @returns void
		 */
		public void Append(Value v)
		{
			//! first check if the frontier Contains
			//! the given key. If so, simply add the
			//! value to the end of the List. 
			//! If not, a new List<Value> needs to 
			//! be created, the value added to it,
			//! and the list added to the frontier
			//! at the given key 
			Key k = heurfun(v);
			if (frontier.ContainsKey(k))
			{
				frontier[k].Add(v);
			} else
			{
				List<Value> list = new List<Value>();
				list.Add(v);
				frontier.Add(k, list);
			}
		}

		/*!
		 * Remove an element from the PriorityQueue. This will
		 * remove the first element of the list
		 * with the smallest key value
		 *
		 * @returns {Value} val
		 */
		public Value Pop()
		{
			//! keys sorts the frontier keys in order
			//! so keys[0] is the smallest key
			if (keys.Count > 0) {
				Value val = frontier[keys[0]][0];
				//! remove the element from the List 
				frontier[keys[0]].RemoveAt(0);
				//! if the List at keys[0] is empty,
				//! remove it from the SortedList
				if (frontier[keys[0]].Count == 0) frontier.Remove(keys[0]);
				return val;
			}

			return default(Value);
		}

		/*!
		 * Check if the frontier contains a node, given
		 * the heuristic value for the node
		 *
		 * @param {Key} - value calculated by the heuristic function
		 * @param {Value} - value to be located
		 * @returns {bool}
		 */
		public bool InPriorityQueue(Value v)
		{
			//TODO:  Add heuristic function
			//to PriorityQueue properties. That
			//way, we can calculate the key
			// when a value is passed in
			Key k = heurfun(v);
			if (frontier.ContainsKey(k)) {
				if (frontier[k].Contains(v))
					return true;
			}

			return false;
			/*if (frontier.ContainsKey(k))
			    return frontier[k].Contains(v);
			else return false;*/
		}

		/*!
		 * Replace a value v in the frontier with a
		 * different but equivalent  
		 * of another value v2 only if v2 has a smaller
		 * value as input to heur function than v
		 *
		 * @param {Key} k - value to possibly be replaced
		 * @param {Value} v - value to possibly be replaced
		 * @param {Func<Value, Key>} - function to calculate
		 *   output of value with
		 * @returns {bool} - true if value was replaced, 
		 *   false otherwise
		 */
		public Value GetIncumbent(Value v)
		{
			Key k =  heurfun(v);
			if (frontier.ContainsKey(k)) 
			{
				int index = frontier[k].IndexOf(v);
				return frontier[k][index];
			}
			else return default(Value);
		}

		public bool RemoveIncumbent(Value v)
		{
			Key k =  heurfun(v);
			if (frontier.ContainsKey(k))
			{
				return frontier[k].Remove(v);
			}
			else return false;
		}
	}
}
