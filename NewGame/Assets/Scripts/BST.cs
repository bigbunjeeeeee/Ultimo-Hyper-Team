using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BST <E>
{
    
        public struct Node// a node of the tree
        {
        Elem elt; // element value 
        Node* par { get; set= null; }; //parent
        Node* left; //left child
        Node* right; //right child
        Node() :  elt(), par(null),left(null),right(null) { };//constructor
        }
   
        //public class Position;
        
    
        //public int size() const ;
        //public bool empty() const;
        //public Position root() const;
        
};
