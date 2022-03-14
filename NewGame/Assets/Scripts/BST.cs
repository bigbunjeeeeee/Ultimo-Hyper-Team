using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BST<E> where E : System.IComparable<E>
{
    public BST_NodeClass Root { get; set; }

    public bool Add () 
    {
        BST_NodeClass<E> before = null, after = this.Root;
        while (after!= null)
        {
            before = after;
            if(condition < after.Data)//Is new node in the left tree?
            {
                after = after.LeftNode;
                
            }
            else if (condition > after.Data)//Is the new node in the right tree?
            {
                after = after.RightNode;
            }
            else
            {
                //Exist same value
                return false;
            }
            BST_NodeClass<E> newNode = new BST_NodeClass<E>();
            newNode.Data = condition;
            if (this.Root == null)//Tree is empty
                this.Root = newNode;
            else
            {
                if (condition < before.Data)
                    before.LeftNode = newNode;
                else
                    before.RightNode = newNode;
            }
            return true;
        }
    }
        
};
