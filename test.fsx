#r @"TypeLib\TypeLib\bin\x64\Debug\net48\TypeLib.dll"
#r @"TypeLib\TypeLib\bin\x64\Debug\net48\DiffSharp.dll"
#r @"TypeLib\TypeLib\bin\x64\Debug\net48\\FSharp.Quotations.Evaluator.dll"
#r @"TypeLib\MethodProvider\bin\x64\Debug\net48\\MethodProvider.dll"
#r @"MethodLb.dll"


open System
open DiffSharp.Symbolic.Float64
open InTheName.OfScience.TypeProvider.Provided
open MethodProvider.Say
open MethodLb.compute



let t = ``Time(s)`` 12
let t2 = ``Time(h)`` 3

let t3 = ``Time(s)`` 3600
let t4 = t3.toHour
let t5 = t4.toSec
let t8 = t+t3 


let d = ``Distance(m)``(120)
let d2 = ``Distance(km)`` 40 

let d3 = ``Distance(km)`` 4
let d4 = d3.toM
let d5 = d4.toKM
let d6 = d + d2.toM


let v = ``Velocity(m/s)``(12)

let v2 = ``Velocity(km/h)`` (d,t)

``Velocity(m/s)`` (d,t)
120/12

``Velocity(m/s)`` (d2,t2)
(40*1000)/(3*3600)

``Velocity(m/s)`` (d2,t)
(40*1000)/12

``Velocity(m/s)`` (d,t2)
120/(3*3600)

let v1 = ``Velocity(km/h)`` 10
let v3 = v1.toMpS
let v4 = v3.toKMpH




let vel = Velocity_Equation <@ fun x -> (x ** 3. + x) @>
let acc = Acceleration_Equation <@ fun x -> (x ** 3. + x) @>
vel.GetType()

let eval2Acc vel at =
    let temp = diff vel at
    Accuracy temp

let z = eval2Acc vel 2.

let m = eval_toAcc vel 2.
let zm = evalAcceleration vel 2.


let accccu = diff vel 2.

let b = eval_toAcc vel 2.

let lol = eval_toAcc vel 2.


//the following 4 are still under development and will most likey be ported to the method library
(*
let acc2 = vel.deriv_toAcc()

let acc3 = vel.eval_toAccDeaf(2.)

let acc4 = vel.eval_toAcc1(2.0)

let acc5 = vel.eval_toAcc2(vel, 2.) 
*)



let acc5 = Acceleration_Equation 5.
let acc6 = Acceleration_Equation (diff <@ fun x -> (x ** 3. + x) @>)

let dm = Accuracy 5.
let p = Accuracy 7.
let ll = dm + z 

let n = <@ fun x -> (x ** 3. + x) @>
n.GetType()
let ms = diff <@ fun x -> (x ** 3. + x) @>
ms.GetType()
let mss = diff <@ fun x -> (x ** 3. + x) @> 2.
mss.GetType()