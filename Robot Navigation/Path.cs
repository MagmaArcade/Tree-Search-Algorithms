﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robot_Navigation
{
    class Path
    {
        Grid _location;

        public Path(Grid location) { _location = location; }
        public Grid Location { get { return _location; } set { _location = value; } }
    }
}
