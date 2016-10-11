namespace SearchTreeNode
{
	using System;
	using System.Collections;
	using System.Collections.Generic;

	/*!
	 * Node in a search tree. Contains pointers to both
	 * the parent node and the state for the node. Also
	 * includes the action that brought us to this state,
	 * and the path cost to reach the node, as well as the
	 * depth in the search tree
	 *
	 * @class Node<State, Action, Cost>
	 *   - State : this type defines the state for the problem.
	 */
	public class Node<State, Action, Cost> : IEquatable<Node<State, Action, Cost> >
		where State:IEquatable
	{
		public State state;
		public Node<State, Action, Cost> parent;
		public Action action;
		public Cost pathCost;
		public int depth;

		public Node(State state, Node<State, Action, Cost> parent=null,  Action action=default(Action), Cost path_cost=default(Cost)) 
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
		public List<Node<State, Action, Cost> > Expand(Problem.AbstractProblem<State,Action,Cost> problem)
		{
			List<Node<State, Action, Cost> > nodes = new List<Node<State, Action, Cost> >(); 
			problem.Actions(this.state).ForEach(
					delegate(Action action) 
					{
					        nodes.Add(this.ChildNode(problem, action));
					}
					);
			return nodes;
		}

		/*!
		 * Return the node containing the new state obtained
		 * by applying the given action to this node's 
		 * current state, with this node as the parent
		 *
		 * @method ChildNode
		 * @param {Problem.AbstractProblem<State, Action, Cost>} - problem. A standard problem
		 * @param {Action} action initiating a transition fro one state to the next
		 * @return {SearchTreeNode.Node<State, Action, Cost>} node - node produced from the current node by Action action
		 */
		public Node<State,Action,Cost> ChildNode(Problem.AbstractProblem<State,Action,Cost> problem, Action action) 
		{
		        State nextState = problem.Result(this.state, action);	
			Node<State,Action,Cost> cnode = new Node<State,Action,Cost>(nextState, this, action, problem.PathCost(this.pathCost, this.state, action, nextState)); 
			return cnode;
		}

		/*!
		 * return a list of actions that lead from the 
		 * current node to the goal node
		 */
		public List<Action> Solution() {
			List<Action> actions = new List<Action>();
			List<Node<State,Action,Cost> > path = this.Path();

			foreach(Node<State,Action,Cost> node in path) {
				actions.Add(node.action);
			}
			/*path.ForEach(delegate(Node<E,A,Cost> node)
					{
					actions.Add(node.action);
					});*/
			return actions;
		}

		/*!
		 * Return a path from this node back to the parent
		 */
		public List<Node<State,Action,Cost> > Path()
		{
			Node<State,Action,Cost> node = this;
			List<Node<State,Action,Cost> > path = new List<Node<State,Action,Cost> >();
			while (node != null) {
				path.Add(node);
				node = node.parent;
			}

			return path;
		}

		public override bool Equals(object obj)
		{
			return this.Equals(obj as Node<State,Action,Cost>); 
		}

		public bool Equals(Node<State,Action,Cost> node)
		{

			Console.WriteLine("in NOde Equals");
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


			return (state.Equals(node.state));
		}
	}

	/*public class NPuzzleNode<State, Action, Cost> : Node<int[], int, int>
	{
		public NPuzzleNode(int[] state, Node<int[], int, int> parent=null, int action=default(int), int path_cost=0) : base(state, parent, action, path_cost)
		{

		}

		public override bool Equals(object obj)
		{
			return this.Equals(obj as NPuzzleNode<State,Action,Cost>); 
		}

		public bool Equals(NPuzzleNode<State,Action,Cost> node)
		{
			Console.WriteLine("in NPuzzleNode Equals");
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
	*/
}
