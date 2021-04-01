using MediatR;
using SentimentAnalyser.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SentimentAnalyser.Application.Common.Interfaces
{
    public interface IRequestWrapper<T> : IRequest<ServiceResult<T>>
    {

    }

    public interface IRequestHandlerWrapper<TIn, TOut> : IRequestHandler<TIn, ServiceResult<TOut>> where TIn : IRequestWrapper<TOut>
    {

    }
}
