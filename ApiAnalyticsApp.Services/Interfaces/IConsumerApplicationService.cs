﻿using ApiAnalyticsApp.DataTransferObjects.Services.ConsumerApplication;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApiAnalyticsApp.Services.Interfaces
{
    public interface IConsumerApplicationService
    {
        Task<KeysDto> CreateConsumerApplication(CreateConsumerApplicationRequestDto request);
    }
}
