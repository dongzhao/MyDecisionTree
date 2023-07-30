using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecisionTree.MVC.Core.Interfaces
{
    public interface IEntity<ID>
    {
        ID Id { get; set; }
    }
}
