open System
open DiffSharp.Symbolic.Float64


[<ReflectedDefinition>]
let d = diff <@ fun x -> sin (3. * sqrt x) @>
Console.WriteLine("{0}",d)

let ds = <@ fun x -> (x ** 3. + x) @>
Console.WriteLine("expr: {0}",ds.GetType())
let dss = diff ds
Console.WriteLine("diff: {0}",dss.GetType())
let dsss = dss 2.
Console.WriteLine("ans: {0}",dsss.GetType())
Console.WriteLine("ans: {0}",dsss)

let m = diff <@ fun x -> (x ** 3. + x) @>
let z = m 2.
Console.WriteLine("ans: {0}",z)


let d2 = d 2.
Console.WriteLine("{0}",d2)


