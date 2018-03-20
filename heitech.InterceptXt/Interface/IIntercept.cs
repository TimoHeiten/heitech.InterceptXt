using System;
using System.Collections.Generic;
using System.Text;

namespace heitech.InterceptXt.Interface
{
    public interface IIntercept
    {
        void Invoke(IInterceptionContext context);
    }
}
