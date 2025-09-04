﻿using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Google.Rpc;
using Grpc.Core;
using GoogleStatus = Google.Rpc.Status;
using GrpcStatus = Grpc.Core.Status;

namespace Discount.Extentions
{
    public static class GrpcErrorHelper
    {
        // Crwating a detailed exception for validation errors
        // enabling the client to receive a structured validation errors
        public static RpcException CreateValidationException(Dictionary<string,string> fieldErrors)
        {
            var fieldViolations = new List<BadRequest.Types.FieldViolation>();
            foreach (var error in fieldErrors)
            {
                fieldViolations.Add(new BadRequest.Types.FieldViolation
                {
                    Field = error.Key,
                    Description = error.Value
                });

               
            }

            // Now Add Bad Request
            var badRequest = new BadRequest();
            badRequest.FieldViolations.Add(fieldViolations);

            var status = new GoogleStatus
            {
                Code = (int)StatusCode.InvalidArgument,
                Message = "Validation Failed",
                Details = { Any.Pack(badRequest) }
            };

            var trailers = new Metadata
                {
                    {"grpc-status-details-bin" , status.ToByteArray() }
                };

            return new RpcException(new GrpcStatus(StatusCode.InvalidArgument, "Validation errors"), trailers);
        }
    }
}
