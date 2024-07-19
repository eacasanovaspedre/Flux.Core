[<AutoOpen>]
module Flux.DotNetSupport

open System

let fromFunc0 (f: Func<_>) = fun () -> f.Invoke ()

let fromFunc1 (f: Func<_, _>) = f.Invoke

let fromFunc2 (f: Func<_, _, _>) = fun a b -> f.Invoke (a, b)

let fromFunc3 (f: Func<_, _, _, _>) = fun a b c -> f.Invoke (a, b, c)

let fromFunc4 (f: Func<_, _, _, _, _>) = fun a b c d -> f.Invoke (a, b, c, d)

let fromFunc5 (f: Func<_, _, _, _, _, _>) = fun a b c d e -> f.Invoke (a, b, c, d, e)

let fromAction (f: Action) = f.Invoke

let fromAction1 (f: Action<_>) = f.Invoke

let fromAction2 (f: Action<_, _>) = fun a b -> f.Invoke (a, b)

let fromAction3 (f: Action<_, _, _>) = fun a b c -> f.Invoke (a, b, c)

let fromAction4 (f: Action<_, _, _, _>) = fun a b c d -> f.Invoke (a, b, c, d)

let fromAction5 (f: Action<_, _, _, _, _>) = fun a b c d e -> f.Invoke (a, b, c, d, e)

let (|Func|) f = fromFunc0 f

let (|Func1|) f = fromFunc1 f

let (|Func2|) f = fromFunc2 f

let (|Func3|) f = fromFunc3 f

let (|Func4|) f = fromFunc4 f

let (|Func5|) f = fromFunc5 f

let (|Action|) f = fromAction f

let (|Action1|) f = fromAction1 f

let (|Action2|) f = fromAction2 f

let (|Action3|) f = fromAction3 f

let (|Action4|) f = fromAction4 f

let (|Action5|) f = fromAction5 f
