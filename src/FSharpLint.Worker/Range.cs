﻿/// FSharpLint, a linter for F#.
/// Copyright (C) 2014 Matthew Mcveigh
/// 
/// This program is free software: you can redistribute it and/or modify
/// it under the terms of the GNU General Public License as published by
/// the Free Software Foundation, either version 3 of the License, or
/// (at your option) any later version.
/// 
/// This program is distributed in the hope that it will be useful,
/// but WITHOUT ANY WARRANTY; without even the implied warranty of
/// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
/// GNU General Public License for more details.
/// 
/// You should have received a copy of the GNU General Public License
/// along with this program.  If not, see <http://www.gnu.org/licenses/>.

namespace FSharpLint.Worker
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Specifies an area in a document.
    /// Used to specify where a lint error has occurred.
    /// C# representation of <see cref="Microsoft.FSharp.Compiler.Range.range"/>.
    /// </summary>
    [Serializable]
    public class Range
    {
        public int StartLine { get; set; }

        public int StartColumn { get; set; }

        public int EndLine { get; set; }

        public int EndColumn { get; set; }

        public string FileName { get; set; }
    }
}
