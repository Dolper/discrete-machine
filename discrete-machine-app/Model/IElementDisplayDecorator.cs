using discrete_machine.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace discrete_machine_app.Model
{
    interface IElementDisplayDecorator : IElement
    {
        int Top { get; set; }
        int Left { get; set; }
        string Template { get; set; }
    }
}
