#r @"TypeLib\TypeLib\bin\Debug\net48\DiffSharp.dll"
#r @"TypeLib\TypeLib\bin\Debug\net48\\FSharp.Quotations.Evaluator.dll"


open System
open DiffSharp.Symbolic.Float64

//practise script for using the differntiation library 

[<ReflectedDefinition>]
let d = diff <@ fun x -> sin (3. * sqrt x) @>
Console.WriteLine("{0}",d)

let d2 = d 2.
Console.WriteLine("{0}",d2)


let ds = <@ fun x -> (x ** 3. + x) @>
Console.WriteLine("expr: {0}",ds.GetType())

let dss = diff ds
Console.WriteLine("diff: {0}",dss.GetType())

let dsss = dss 2.
Console.WriteLine("ans: {0}",dsss.GetType())
Console.WriteLine("ans: {0}",dsss)


let ms = diff <@ fun x -> (x ** 3. + x) @> 2.





type Expr =  
    | Con of int
    | Var of string
    | Add of Expr * Expr
    | Sub of Expr * Expr
    | Mult of Expr * Expr
    | Div of Expr * Expr
    | Power of Expr * Expr
    | Neg of Expr

type Expr with  
    static member (+) (x, y) = Add (x, y)
    static member (-) (x, y)  = Sub (x, y)
    static member (*) (x, y)  = Mult (x, y)
    static member (/) (x, y)  = Div (x, y)
    static member Pow (x, y) = Power (x, y)
    static member (~-) x = Neg x
    static member (~+) x = x

type Expr with  
    static member (+) (x, y) = Add (x, Con y)
    static member (+) (x, y) = Add (Con x, y)
    static member (-) (x, y) = Sub (x, Con y)
    static member (-) (x, y) = Sub (Con x, y)
    static member (*) (x, y) = Mult (x, Con y)
    static member (*) (x, y) = Mult (Con x, y)
    static member (/) (x, y) = Div (x, Con y)
    static member (/) (x, y) = Div (Con x, y)
    static member Pow (x, y) = Power (x, Con y)
    static member Pow (x, y) = Power (Con x, y)

let rec deriv var expr =  
    let d = deriv var
    match expr with
    | Var var -> Con 1                             // Identity Rule
    | Con x -> Con 0                               // Constant Rule
    | Mult (Con x, y) | Mult (y, Con x) -> Con x   // Constant Factor Rule
    | Add (x, y) -> d x + d y                      // Sum Rule
    | Sub (x, y) -> d x - d y                      // Difference Rule
    | Mult (x, y) -> d x * y + x * d y             // Product Rule
    | Div (x, y) -> (d x * y - x * d y) / y ** 2   // Quotient Rule
    | Power (var, Con x) -> x * var ** (x - 1)     // Elementary Power Rule
    | _ -> failwith "Sorry, don't know how to differentiate that!"


let x = Var "x"  
let y = Var "y"  
let z = Var "z" 
deriv x (x ** 3 + x)


let rec print expr =  
    match expr with     
    | Add (x, y) -> sprintf "(%s + %s)" (print x) (print y)
    | Sub (x, y) -> sprintf "(%s - %s)" (print x) (print y)
    | Mult (x, y) -> sprintf "(%s * %s)" (print x) (print y)
    | Div (x, y) -> sprintf "(%s / %s)" (print x) (print y)
    | Power (x, y) -> sprintf "(%s ** %s)" (print x) (print y)
    | Neg x -> sprintf "-(%s)" (print x)
    | Var x -> x
    | Con x -> string x

print <| (-x + 2) * (x ** y) ** z / -2
