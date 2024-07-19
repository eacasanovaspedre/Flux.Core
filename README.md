[![Build and Publish](https://github.com/eacasanovaspedre/Flux.Core/actions/workflows/action-nuget.yml/badge.svg?branch=master)](https://github.com/eacasanovaspedre/Flux.Core/actions/workflows/action-nuget.yml)

# What's is Flux.Core

Flux.Core is a set of types and functions that I find useful to have in my F# projects.

# Types

## NonEmptyString (nestring)

Most strings in my domain types can't be empty. This type helps with that, so I don't have to be constantly validating strings.

## EmailAddress

As the name suggests, EmailAddress represents an email address. It includes validation of the string value, so that if the object EmailAddress is built it complies with the rules for a well formed email address. I use a very complex regular expression that I'm not sure it's perfect, but it seems to work. It's definitely better that doing it manually.

## Other utilities

Some functions and types that I've have used or needed in the past are included.