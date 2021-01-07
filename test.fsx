#r @"E:/Uni-Weimar Subjects/Math.Sc type provider/Workspace/Blank test/Fresh/TypeLib/TypeLib/bin/Debug/netstandard2.0/TypeLib.dll"


open InTheName.OfScience.TypeProvider.Provided

MyType.MyProperty

let thing = MyType()
let thingInnerState = thing.InnerState

let thing2 = MyType("Some other text")
let thing2InnerState = thing2.InnerState


let a = int 12


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

