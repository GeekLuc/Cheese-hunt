using System.Collections.Generic;
/*
 * Rougefort Luca
 * HEAJ JV B2
 * Dev Mobile
 */
namespace Script.BehaviorTree{
    public enum NodeState{
        RUNNING,
        SUCCESS,
        FAILURE
    }

    public class Node{
        protected NodeState state;

        protected Node parent;
        protected List<Node> children = new List<Node>();

        private Dictionary<string, object> _dataContext = new Dictionary<string, object>();

        public Node(){
            parent = null;
        }

        public Node(List<Node> children){
            foreach (Node child in children)
                _attach(child);
        }

        private void _attach(Node node){
            node.parent = this;
            children.Add(node);
        }

        public virtual NodeState Evaluate() => NodeState.FAILURE;
        
        public virtual void Reset() {}

        public void SetData(string key, object value){
            Node prev = parent;
            Node curr = parent;
            while(curr != null){
				prev = curr;
				curr = curr.parent;
			}	
            prev._dataContext[key] = value;
        }

        public object GetData(string key){
            object value = null;
            if(_dataContext.TryGetValue(key, out value))
                return value;

            Node node = parent;
            while(node != null){
                value = node.GetData(key);
                if(value != null)
                    return value;
                node = node.parent;
            }

            return null;
        }



        public bool ClearData(string key){
            if(_dataContext.ContainsKey(key)){
                _dataContext.Remove(key);
                return true;
            }

            Node node = parent;
            while(node != null){
                bool cleared = node.ClearData(key);
                if(cleared)
                    return true;
                node = node.parent;
            }

            return false;
        }
    }
}