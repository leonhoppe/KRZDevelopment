﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;

namespace TwitterKlon.Logic
{
    public static class ControllerBaseExtension
    {
        public static ActionResult FromLogicResult(this ControllerBase controller, ILogicResult result)
        {
            switch (result.State)
            {
                case LogicResultState.Ok:
                    return controller.Ok();

                case LogicResultState.BadRequest:
                    return controller.StatusCode((int)HttpStatusCode.BadRequest, result.Message);

                case LogicResultState.Forbidden:
                    return controller.StatusCode((int)HttpStatusCode.Forbidden, result.Message);

                case LogicResultState.NotFound:
                    return controller.StatusCode((int)HttpStatusCode.NotFound, result.Message);

                case LogicResultState.Conflict:
                    return controller.StatusCode((int)HttpStatusCode.Conflict, result.Message);

                default:
                    throw new Exception("An unhandled result has occurred as a result of a service call.");
            }
        }

        public static ActionResult FromLogicResult<T>(this ControllerBase controller, ILogicResult<T> result)
        {
            switch (result.State)
            {
                case LogicResultState.Ok:
                    return controller.Ok(result.Data);

                case LogicResultState.BadRequest:
                    return controller.StatusCode((int)HttpStatusCode.BadRequest, result.Message);

                case LogicResultState.Forbidden:
                    return controller.StatusCode((int)HttpStatusCode.Forbidden, result.Message);

                case LogicResultState.NotFound:
                    return controller.StatusCode((int)HttpStatusCode.NotFound, result.Message);

                case LogicResultState.Conflict:
                    return controller.StatusCode((int)HttpStatusCode.Conflict, result.Message);

                default:
                    throw new Exception("An unhandled result has occurred as a result of a service call.");
            }
        }
    }
}
