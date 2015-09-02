namespace FCore.Tests

open FCore
open Xunit
open FsCheck
open FsCheck.Xunit
open System

module BoolMatrixConstruction =

    [<Property>]
    let ``Constructs empty matrix`` () =
        let v = BoolMatrix.Empty
        v.LongRowCount = 0L && v.LongColCount = 0L && v.ToArray2D() = Array2D.zeroCreate<bool> 0 0

    [<Property>]
    let ``Throws arg exception if construction with row64 or col64 < 0`` (r : int64) (c : int64) (x:bool) =
        (r < 0L || c < 0L) ==> Prop.throws<ArgumentException, _> (lazy(new BoolMatrix(r, c, x)))

    [<Property>]
    let ``Throws arg exception if construction with row or col < 0`` (r : int) (c: int) (x : bool) =
        (r < 0 || c < 0) ==> Prop.throws<ArgumentException, _> (lazy(new BoolMatrix(r, c, x)))

    [<Property>]
    let ``Constructs BoolMatrix from non negative int64s and bool value`` (r : int64) (c : int64) (x:bool) =
        (r >= 0L && c >= 0L) ==> lazy(let v = new BoolMatrix(r, c, x) in v.LongRowCount = r && v.LongColCount = c && v.ToArray2D() = Array2D.create (int(r)) (int(c)) x)

    [<Property>]
    let ``Constructs BoolMatrix from non negative ints and bool value`` (r : int) (c : int) (x:bool) =
        (r >= 0 && c >= 0) ==> lazy(let v = new BoolMatrix(r, c, x) in v.RowCount = r && v.ColCount = c && v.ToArray2D() = Array2D.create r c x)

    [<Property>]
    let ``Constructs BoolMatrix from bool value`` (x : bool) =
        let v = new BoolMatrix(x) in v.RowCount = 1 && v.ColCount = 1 && v.ToArray2D() = ([[x]]|>array2D)

    [<Property>]
    let ``Constructs BoolMatrix from bool array2d`` (data : bool[,]) = 
        let v = new BoolMatrix(data) in v.RowCount = (data|>Array2D.length1) && v.ColCount = (data|>Array2D.length2) && v.ToArray2D() = data

    [<Property>]
    let ``Constructs BoolMatrix from ints and initializer function`` (r : int) (c : int) (init : int -> int -> bool) = 
        (r >= 0 && c >= 0) ==> lazy (let v = new BoolMatrix(r, c, init) in v.RowCount = r && v.ColCount = c && v.ToArray2D() = Array2D.init r c init)
