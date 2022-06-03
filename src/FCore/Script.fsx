#r @".\bin\release\FCore.dll"
open FCore
open FCore.ExplicitConversion
open FCore.Math
open FCore.LinearAlgebra
open System
open System.IO
open System.Runtime.InteropServices
open System.Collections.Generic
open FCore.Random

open Overloading
open BasicStats

let is64 = Environment.Is64BitProcess

let m = new Matrix([[1.;2.;3.]
                    [1.;2.;3.]
                   ]
                  )
