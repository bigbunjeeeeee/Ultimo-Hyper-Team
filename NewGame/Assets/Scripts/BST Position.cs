using System.Collections;
using System.Collections.Generic;
using UnityEngine;
template <typename E>
public class BSTPosition <E>
{
    private:
        
    public :
        E& operator*();
        BSTPosition left() const;
        BSTPosition right() const;
        BSTPosition parent() const;
        bool isRoot() const;
        bool isExternal() const;
    
}
