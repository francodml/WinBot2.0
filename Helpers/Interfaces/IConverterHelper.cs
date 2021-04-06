﻿using System.Threading.Tasks;
using WBot2.Commands;

namespace WBot2.Helpers.Interfaces
{
    public interface IConverterHelper
    {
        Task<object> ConvertParameterAsync<T>(CommandContext ctx, string argument, int argindex);
    }
}