namespace SearchTreeNode
{
	using System;
	using System.Collections.Generic;

	/*!
	 * Node in a search tree. Contains pointers to both
	 * the parent node and the state for the node. Also
	 * includes the action that brought us to this state,
	 * and the path cost to reach the node
	 */
	public class Node<E, A, C> : IEquatable<Node<E,A,C> >
	{
		public E state;
		public Node<E,A,C> parent;
		public A action;
		public C pathCost;
		public int depth;

		public Node(E state, Node<E, A, C> parent=null, A action=default(A), C path_cost=default(C)) 
		{
			this.state = state;
			this.parent = parent;
			this.action = action;
			this.pathCost = path_cost;

			if (parent != null) this.depth = parent.depth + 1;
			else this.depth = 0;
		}

		protected Node() {}

		/*! nodes reachable from this node */
		public List<Node<E, A, C> > Expand(Problem.AbstractProblem<E,A,C> problem)
		{
			List<Node<E, A, C> > nodes = new List<Node<E, A, C> >(); 
			problem.Actions(this.state).ForEach(
					delegate(A action) 
					{
					        nodes.Add(this.ChildNode(problem, action));
					}
					);
			return nodes;

		}

		public Node<E,A,C> ChildNode(Problem.AbstractProblem<E,A,C> problem, A action) 
		{
		        E nextState = problem.Result(this.state, action);	
			Node<E,A,C> cnode = new Node<E,A,C>(nextState, this, action,problem.PathCost(this.pathCost, this.state, action, nextState)); 
			return cnode;

		}

		/*!
		 * return a list of actions that lead from the 
		 * current node to the goal node
		 */
		public List<A> Solution() {
			List<A> actions = new List<A>();
			List<Node<E,A,C> > path = this.Path();

			foreach(Node<E,A,C> node in path) {
				actions.Add(node.action);
			}
			/*path.ForEach(delegate(Node<E,A,C> node)
					{
					actions.Add(node.action);
					});*/
			return actions;
		}

		/*!
		 * Return a path from this node back to the parent
		 */
		public List<Node<E,A,C> > Path()
		{
			Node<E,A,C> node = this;
			List<Node<E,A,C> > path = new List<Node<E,A,C> >();
			while (node != null) {
				path.Add(node);
				node = node.parent;
			}

			return path;
		}

		public override bool Equals(object obj)
		{
			return this.Equals(obj as Node<E,A,C>); 
		}

		public bool Equals(Node<E,A,C> node)
		{
			if (Object.ReferenceEquals(node, null))
			{
				return false;
			}

			if (Object.ReferenceEquals(this, node)) 
			{
				return true;
			}

			if (this.GetType() != node.GetType())
				return false;

			//! TODO: state.Equals(node.state) will check reference equality, not value
			//! equality, which is what will be wanted  for different
			//! node.states. Right now, there is no definition for 'E',
			//! the generic type for state, in the definition
			//! of the Node class
			//! So if the state is an array or some other type, how do we go about testing for
			// equality?


			Type valueType = state.GetType();
			var expectedType = typeof(Array);
			if (valueType.IsArray && expectedType.IsAssignableFrom(valueType.GetElementType())) 
			{

			}
			return (state.Equals(node.state)) && (parent.Equals(node.parent)) && (action.Equals(node.action)) && (depth == node.depth) && (pathCost.Equals(node.pathCost));

		}
	}

	public class NPuzzleNode : Node<int[], int, int>
	{
		public NPuzzleNode(int[] state, Node<int[], int, int> parent=null, int action=default(int), int path_cost=0) : base(state, parent, action, path_cost)
		{

		}
		public bool Equals(Node<int[],int,int> node)
		{
			if (Object.ReferenceEquals(node, null))
			{
				return false;
			}

			if (Object.ReferenceEquals(this, node)) 
			{
				return true;
			}

			if (this.GetType() != node.GetType())
				return false;

			bool stateFlag = true;

			if (state.Length == node.state.Length)
			{
				int L = state.Length;
				for (int i = 0; i < L; i++)
				{
					if(state[i] != node.state[i]) stateFlag = false;
				}
			} else
			{
				stateFlag = false;
			}
		


			return stateFlag && (parent.Equals(node.parent)) && (action.Equals(node.action)) && (depth == node.depth) && (pathCost.Equals(node.pathCost));

		}

	}
}
