using Microsoft.Xna.Framework;
using Panacea.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panacea.Engine_Code.Interfaces
{
    public interface IPathFinder
    {
        List<Vector2> Path { get; set; }
        // do nothing for now
        
    }
}
