namespace Flux

open System.Text.RegularExpressions

[<Struct>]
type NEString = private | NEString of string

type CannotCreateNEString = CannotCreateNEString

exception CannotCreateNEStringException

module NEString =

    let tryCreate value =
        if String.length value > 0 then Ok(NEString value) else Error CannotCreateNEString

    let maybeCreate value =
        if String.length value > 0 then Some(NEString value) else None

    let create value =
        if String.length value > 0 then NEString value else raise (CannotCreateNEStringException)

    let stringValue (NEString value) = value

[<Struct>]
type Email = private | Email of value: string

type InvalidEmailFormat = InvalidEmailFormat of string

exception InvalidEmailFormatException of InvalidEmailFormat

module Email =

    let private regex =
        Regex
            (@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?",
             RegexOptions.Compiled ||| RegexOptions.IgnoreCase)

    let tryCreate email =
        let result = regex.Match email
        if result.Success then Ok(Email email) else Error(InvalidEmailFormat email)

    let maybeCreate email =
        let result = regex.Match email
        if result.Success then Some(Email email) else None

    let create email =
        let result = regex.Match email
        if result.Success
        then Email email
        else raise (InvalidEmailFormatException(InvalidEmailFormat email))

    let stringValue (Email email) = email

module Text =
    open System.Text

    let bytesToStringUTF8 (bytes: byte []) = Encoding.UTF8.GetString bytes

    let stringToBytesUTF8 (str: string) = Encoding.UTF8.GetBytes str

module Result =
    let safe f e x =
        try
            x
            |> f
            |> Ok
        with ex ->
            ex
            |> e
            |> Error

    let iterEither f e x =
        match x with
        | Ok v -> f v
        | Error er -> e er
        x

namespace Flux.LanguageUtils.Linq.Expressions

open System.Linq.Expressions
open System

type 'T Pred = Expression<Func<'T, bool>>

type Mapper<'T, 'R> = Expression<Func<'T, 'R>>

type Selector<'T, 'R> = Mapper<'T, 'R>

type Expr private () =
    static member Of(e: Expression<Func<'T1, 'TResult>>) = e
    static member Of(e: Expression<Func<'T1, 'T2, 'TResult>>) = e
    static member Of(e: Expression<Func<'T1, 'T2, 'T3, 'TResult>>) = e
    static member Of(e: Expression<Func<'T1, 'T2, 'T3, 'T4, 'TResult>>) = e
