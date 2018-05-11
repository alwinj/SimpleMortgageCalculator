using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;

namespace MortgageCalculator.Api.App_Start
{
    public class AutoMapperConfig
    {
        public static void Initialize()
        {
            Mapper.Initialize((config) =>
            {
                config.CreateMap<MortgageData.Mortgage, MortgageCalculator.Dto.Mortgage>().ReverseMap();
            });
        }
    }
}