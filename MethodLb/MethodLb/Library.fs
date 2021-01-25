namespace MethodLb

open DiffSharp.Symbolic.Float64
open InTheName.OfScience.TypeProvider.Provided

module compute =  
    let evalAcceleration vel at = 
        let z = diff vel at 
        let zz = ``Velocity(km/h)`` (int z)
        zz
