﻿(*
    FSharpLint, a linter for F#.
    Copyright (C) 2014 Matthew Mcveigh

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*)

module TestRaiseWithTooManyArgumentsRules

open NUnit.Framework
open FSharpLint.Rules.RaiseWithTooManyArguments
open FSharpLint.Framework.Configuration
open FSharpLint.Framework.LoadVisitors

let config = 
    Map.ofList 
        [ 
            (AnalyserName, 
                { 
                    Rules = Map.ofList 
                        [
                            ("FailwithWithSingleArgument", 
                                { 
                                    Settings = Map.ofList 
                                        [ ("Enabled", Enabled(true)) ] 
                                }) 
                            ("RaiseWithSingleArgument", 
                                { 
                                    Settings = Map.ofList 
                                        [ ("Enabled", Enabled(true)) ] 
                                }) 
                            ("NullArgWithSingleArgument", 
                                { 
                                    Settings = Map.ofList 
                                        [ ("Enabled", Enabled(true)) ] 
                                }) 
                            ("InvalidOpWithSingleArgument", 
                                { 
                                    Settings = Map.ofList 
                                        [ ("Enabled", Enabled(true)) ] 
                                }) 
                            ("InvalidArgWithArgumentsMatchingFormatString", 
                                { 
                                    Settings = Map.ofList 
                                        [ ("Enabled", Enabled(true)) ] 
                                }) 
                            ("FailwithfWithArgumentsMatchingFormatString", 
                                { 
                                    Settings = Map.ofList 
                                        [ ("Enabled", Enabled(true)) ] 
                                }) 
                        ]
                    Settings = Map.ofList 
                        [
                            ("Enabled", Enabled(true))
                        ]
                }) 
        ]

[<TestFixture>]
type TestRaiseWithTooManyArgumentsRules() =
    inherit TestRuleBase.TestRuleBase(Ast(visitor), config)

    [<Test>]
    member this.FailwithWithCorrectNumberOfArguments() = 
        this.Parse """
module Program

failwith "" """

        Assert.IsFalse(this.ErrorExistsAt(4, 0))

    [<Test>]
    member this.FailwithWithExtraArgument() = 
        this.Parse """
module Program

failwith "" "" """

        Assert.IsTrue(this.ErrorExistsAt(4, 0))

    [<Test>]
    member this.FailwithWithMultipleExtraArguments() = 
        this.Parse """
module Program

failwith "" "" "" "" """

        Assert.IsTrue(this.ErrorExistsAt(4, 0))

    [<Test>]
    member this.FailwithWithExtraArgumentWithRightPipe() = 
        this.Parse """
module Program

"" |> failwith "" """

        Assert.IsTrue(this.ErrorExistsAt(4, 0))

    [<Test>]
    member this.RaiseWithCorrectArguments() = 
        this.Parse """
module Program

raise (System.ArgumentException("Divisor cannot be zero!")) """

        Assert.IsFalse(this.ErrorExistsAt(4, 0))

    [<Test>]
    member this.RaiseWithExtraArgument() = 
        this.Parse """
module Program

raise (System.ArgumentException("Divisor cannot be zero!")) "" """

        Assert.IsTrue(this.ErrorExistsAt(4, 0))

    [<Test>]
    member this.FailwithfWithCorrectNumberOfArguments() = 
        this.Parse """
module Program

failwithf "%d %s" 4 "dog" """

        Assert.IsFalse(this.ErrorExistsAt(4, 0))

    [<Test>]
    member this.FailwithfWithExtraArgument() = 
        this.Parse """
module Program

failwithf "%d %s" 4 "dog" 5 """

        Assert.IsTrue(this.ErrorExistsAt(4, 0))

    [<Test>]
    member this.FailwithfWithEscapedFormatAndWithExtraArgument() = 
        this.Parse """
module Program

failwithf "%d %% %s" 4 "dog" 5 """

        Assert.IsTrue(this.ErrorExistsAt(4, 0))

    [<Test>]
    member this.NullArgWithCorrectNumberOfArguments() = 
        this.Parse """
module Program

nullArg "" """

        Assert.IsFalse(this.ErrorExistsAt(4, 0))

    [<Test>]
    member this.NullArgWithExtraArgument() = 
        this.Parse """
module Program

nullArg "" "" """

        Assert.IsTrue(this.ErrorExistsAt(4, 0))

    [<Test>]
    member this.InvalidOpWithCorrectNumberOfArguments() = 
        this.Parse """
module Program

invalidOp "" """

        Assert.IsFalse(this.ErrorExistsAt(4, 0))

    [<Test>]
    member this.InvalidOpWithExtraArgument() = 
        this.Parse """
module Program

invalidOp "" "" """

        Assert.IsTrue(this.ErrorExistsAt(4, 0))

    [<Test>]
    member this.InvalidArgWithCorrectNumberOfArguments() = 
        this.Parse """
module Program

invalidArg "%d %s" 4 "dog" """

        Assert.IsFalse(this.ErrorExistsAt(4, 0))

    [<Test>]
    member this.InvalidArgWithExtraArgument() = 
        this.Parse """
module Program

invalidArg "%d %s" 4 "dog" 5 """

        Assert.IsTrue(this.ErrorExistsAt(4, 0))