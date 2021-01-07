﻿module InTheName.OfScience.TypeProvider

open ProviderImplementation.ProvidedTypes
open Microsoft.FSharp.Core.CompilerServices
open System.Reflection

[<TypeProvider>]
type MathProvider (config : TypeProviderConfig) as this =
    inherit TypeProviderForNamespaces (config, addDefaultProbingLocation=true)

    let ns = "InTheName.OfScience.TypeProvider.Provided"
    let asm = Assembly.GetExecutingAssembly()


    let createTypes () =
        let myType = ProvidedTypeDefinition(asm, ns, "MyType", Some typeof<obj>)

        let myProp = ProvidedProperty("MyProperty", typeof<string>,(fun args -> <@@ "Hello world" @@>), isStatic = true)
        myType.AddMember(myProp)

        let ctor = ProvidedConstructor([], invokeCode = fun args -> <@@ "My internal state" :> obj @@>)
        myType.AddMember(ctor)

        let ctor2 = ProvidedConstructor(
                        [ProvidedParameter("InnerState", typeof<string>)],
                        invokeCode = fun args -> <@@ (%%(args.[0]):string) :> obj @@>)
        myType.AddMember(ctor2)

        let innerState = ProvidedProperty("InnerState", typeof<string>,
                            getterCode = fun args -> <@@ (%%(args.[0]) :> obj) :?> string @@>)
        myType.AddMember(innerState)



        let sec = ProvidedTypeDefinition(asm,ns,"Time(s)", Some typeof<int>)
        let ctor = ProvidedConstructor(
                        [ProvidedParameter("t", typeof<int>)], 
                        invokeCode = fun args -> <@@ (%%(args.[0]):int) @@>)
        sec.AddMember(ctor)
        

        let hr = ProvidedTypeDefinition(asm,ns,"Time(h)", Some typeof<int>)
        let ctor = ProvidedConstructor(
                        [ProvidedParameter("t", typeof<int>)], 
                        invokeCode = fun args -> <@@ (%%(args.[0]):int) :> obj @@>)
        hr.AddMember(ctor)


        let prop = ProvidedProperty("toHour", hr , getterCode = fun args -> <@@ ((%%(args.[0]):int)/3600) @@>)
        sec.AddMember(prop)
        let prop = ProvidedProperty("toSec", sec , getterCode = fun args -> <@@ ((%%(args.[0]):int)*3600) @@>)
        hr.AddMember(prop)


        let meter = ProvidedTypeDefinition(asm,ns,"Distance(m)", Some typeof<int>)
        let ctor = ProvidedConstructor(
                        [ProvidedParameter("d", typeof<int>)], 
                        invokeCode = fun args -> <@@ (%%(args.[0]):int) :> obj @@>)
        meter.AddMember(ctor)


        let km = ProvidedTypeDefinition(asm,ns,"Distance(km)", Some typeof<int>)
        let ctor = ProvidedConstructor(
                        [ProvidedParameter("d", typeof<int>)], 
                        invokeCode = fun args -> <@@ (%%(args.[0]):int) :> obj @@>)
        km.AddMember(ctor)


        let prop = ProvidedProperty("toKM", km , getterCode = fun args -> <@@ ((%%(args.[0]):int)/1000) @@>)
        meter.AddMember(prop)
        let prop = ProvidedProperty("toM", meter , getterCode = fun args -> <@@ ((%%(args.[0]):int)*1000) @@>)
        km.AddMember(prop)


        let mps = ProvidedTypeDefinition(asm,ns,"Velocity(m/s)", Some typeof<obj>)
        let ctor = ProvidedConstructor(
                        [ProvidedParameter("d", typeof<int>)], 
                        invokeCode = fun args -> <@@ (%%(args.[0]):int) :> obj @@>)
        mps.AddMember(ctor)
        let ctor2 = ProvidedConstructor(
                        [ProvidedParameter("d", meter); ProvidedParameter("t", sec)], 
                        invokeCode = fun args -> <@@ (%%(args.[0]):int) / (%%(args.[1]):int) :> obj @@> )
        mps.AddMember(ctor2)
        let ctor3 = ProvidedConstructor(
                        [ProvidedParameter("d", km); ProvidedParameter("t", hr)], 
                        invokeCode = fun args -> <@@ ((%%(args.[0]):int)*1000) / ((%%(args.[1]):int)*3600) :> obj @@> )
        mps.AddMember(ctor3)
        let ctor4 = ProvidedConstructor(
                        [ProvidedParameter("d", meter); ProvidedParameter("t", hr)], 
                        invokeCode = fun args -> <@@ ((%%(args.[0]):int)) / ((%%(args.[1]):int)*3600) :> obj @@> )
        mps.AddMember(ctor4)
        let ctor5 = ProvidedConstructor(
                        [ProvidedParameter("d", km); ProvidedParameter("t", sec)], 
                        invokeCode = fun args -> <@@ ((%%(args.[0]):int)*1000) / ((%%(args.[1]):int)) :> obj @@> )
        mps.AddMember(ctor5)


        let kph = ProvidedTypeDefinition(asm,ns,"Velocity(km/h)", Some typeof<obj>)
        let ctor = ProvidedConstructor(
                        [ProvidedParameter("d", typeof<int>)], 
                        invokeCode = fun args -> <@@ (%%(args.[0]):int) :> obj @@>)
        kph.AddMember(ctor)
        let ctor2 = ProvidedConstructor(
                        [ProvidedParameter("d", km); ProvidedParameter("t", hr)], 
                        invokeCode = fun args -> <@@ (%%(args.[0]):int) / (%%(args.[1]):int) :> obj @@> )
        kph.AddMember(ctor2)
        let ctor3 = ProvidedConstructor(
                        [ProvidedParameter("d", meter); ProvidedParameter("t", sec)], 
                        invokeCode = fun args -> <@@ ((%%(args.[0]):int)/1000) / ((%%(args.[1]):int)/3600) :> obj @@> )
        kph.AddMember(ctor3)
        let ctor4 = ProvidedConstructor(
                        [ProvidedParameter("d", meter); ProvidedParameter("t", hr)], 
                        invokeCode = fun args -> <@@ ((%%(args.[0]):int)/1000) / ((%%(args.[1]):int)) :> obj @@> )
        kph.AddMember(ctor4)
        let ctor5 = ProvidedConstructor(
                        [ProvidedParameter("d", km); ProvidedParameter("t", sec)], 
                        invokeCode = fun args -> <@@ ((%%(args.[0]):int)) / ((%%(args.[1]):int)/3600) :> obj @@> )
        kph.AddMember(ctor5)

        
        let prop = ProvidedProperty("toMpS", mps , getterCode = fun args -> <@@ (((%%(args.[0]):obj):?>int)*10/36) :> obj @@>)
        kph.AddMember(prop)
        let prop = ProvidedProperty("toKMpH", kph , getterCode = fun args -> <@@ (((%%(args.[0]):obj):?>int)*36/10) :> obj @@>)
        mps.AddMember(prop)


        [myType;sec;meter;mps;hr;km;kph]

    do
        this.AddNamespace(ns, createTypes())




[<assembly:TypeProviderAssembly>]
do ()