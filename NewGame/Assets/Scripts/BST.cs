using System.Collections;
using System.Collections.Generic;
using UnityEngine;
template < typename E >
public class BST <E>
{
    protected:
        struct Node// a node of the tree
    {
        Elem elt; // element value 
        Node* par; //parent
        Node* left; //left child
        Node* right; //right child
        Node() : elt(), par(null),left(null),right(null) { }//constructor
    }
   public :
        class Position;
        
    public:
        int size() const ;
        bool empty() const;
        Position root() const;

}
