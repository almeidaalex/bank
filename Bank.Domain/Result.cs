using System;
using System.Diagnostics.CodeAnalysis;

using MediatR;

namespace Bank.Domain
{
  public class Result
  {
    public bool Success { get; private set; }
    public string Error { get; private set; }

    public bool Failure
    {
      get { return !Success; }
    }

    protected Result(bool success, string error)
    {
      Success = success;
      Error = error;
    }

    public static Result Fail(string message) =>
       new(false, message);


    public static Result<T> Fail<T>(string message)
    {
      return new Result<T>(default, false, message);
    }

    public static Result<T> Fail<T>()
    {
      return new Result<T>(default, false, string.Empty);
    }

    public static Result Ok()
    {
      return new Result(true, String.Empty);
    }

    public static Result<T> Ok<T>(T value)
    {
      return new Result<T>(value, true, String.Empty);
    }

    public static Result Combine(params Result[] results)
    {
      foreach (Result result in results)
      {
        if (result.Failure)
          return result;
      }

      return Ok();
    }

    public static Result<T> From<T>(T value, Result result)
    {
      return new Result<T>(value, result.Success, result.Error);
    }
  }


  public class Result<T> : Result
  {
    private readonly T _value;
    public T Value => _value;


    protected internal Result([AllowNull] T value, bool success, string error)
        : base(success, error)
    {
      _value = value;
    }
  }


}