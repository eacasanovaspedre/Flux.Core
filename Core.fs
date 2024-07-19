namespace Flux

open System.Text.RegularExpressions

[<Struct>]
type NonEmptyString = private NonEmptyString of string

type nestring = NonEmptyString

module NonEmptyString =

    [<RequireQualifiedAccess>]
    type CouldNotCreateNonEmptyString = | StringWasEmpty

    exception CouldNotCreateNonEmptyStringException of CouldNotCreateNonEmptyString

    let tryCreate value =
        if String.length value = 0 then
            Ok (NonEmptyString value)
        else
            Error CouldNotCreateNonEmptyString.StringWasEmpty

    let maybeCreate value =
        if String.length value > 0 then
            Some (NonEmptyString value)
        else
            None

    let create value =
        if String.length value > 0 then
            NonEmptyString value
        else
            raise (CouldNotCreateNonEmptyStringException CouldNotCreateNonEmptyString.StringWasEmpty)

    let toString (NonEmptyString value) = value

    module ActivePattern =

        let (|NonEmptyString|) (NonEmptyString value) = value

[<Struct>]
type EmailAddress = private EmailAddress of string

module Email =

    [<RequireQualifiedAccess>]
    type CouldNotCreateEmailAddress =
        | ValueWasEmpty
        | InvalidFormat of string

    exception CouldNotCreateEmailAddressException of CouldNotCreateEmailAddress

    let private regex =
        Regex (
            @"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?",
            RegexOptions.Compiled ||| RegexOptions.IgnoreCase
        )

    let tryCreate value =
        if String.length value = 0 then
            Error (CouldNotCreateEmailAddress.ValueWasEmpty)
        else
            let result = regex.Match value

            if result.Success then
                Ok (EmailAddress value)
            else
                Error (CouldNotCreateEmailAddress.InvalidFormat value)

    let maybeCreate value =
        if String.length value = 0 then
            None
        else
            let result = regex.Match value
            if result.Success then Some (EmailAddress value) else None

    let create value =
        if String.length value = 0 then
            raise (CouldNotCreateEmailAddressException CouldNotCreateEmailAddress.ValueWasEmpty)
        else
            let result = regex.Match value

            if result.Success then
                EmailAddress value
            else
                raise (CouldNotCreateEmailAddressException (CouldNotCreateEmailAddress.InvalidFormat value))

    let toString (EmailAddress value) = value

    module ActivePattern =

        let (|EmailAddress|) (EmailAddress value) = value

module Text =
    open System.Text

    let bytesToStringUTF8 (bytes: byte[]) = Encoding.UTF8.GetString bytes

    let stringToBytesUTF8 (str: string) = Encoding.UTF8.GetBytes str

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
