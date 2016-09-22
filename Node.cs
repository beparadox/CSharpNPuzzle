using System;
using System.Collections.Generic;
namespace SearchTreeNode
{
	/*!
	 * Node in a search tree. Contains pointers to both
	 * the parent node and the state for the node. Also
	 * includes the action that brought us to this state,
	 * and the path cost to reach the node
	 */
	public class Node<E, A, C>
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

		/*! nodes reachable from this node */
		public List<Node<E, A, C> > Expand(Problem.AbstractProblem<E,A,C> problem)
		{
			List<Node<E, A, C> > nodes = new List<Node<E, A, C> >(); 
			problem.Actions(this.state).ForEach(delegate(A action) 
					{
					        nodes.Add(this.ChildNode(problem, action));

					});
			return nodes;

		}

		public Node<E,A,C> ChildNode(Problem.AbstractProblem<E,A,C> problem, A action) 
		{
		        E nextState = problem.Result(this.state, action);	
			Node<E,A,C> cnode = new Node<E,A,C>(nextState, this, action,problem.PathCost(this.pathCost, this.state, action, nextState)); 
			return cnode;

		}

		/*!
		 * return a list of actions that led from the root node to this current node
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
			Node<E,A,C> node = this.parent;
			List<Node<E,A,C> > path = new List<Node<E,A,C> >();
			path.Add(this);
			while (node != null) {
				path.Add(node);
				node = node.parent;
			}

			return path;
		}
	}
}
