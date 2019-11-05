using Microsoft.AspNetCore.Mvc;
using Site.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;


namespace Site.Services
{
    public interface IRegisterContactService
    {
        Task Register(Contact json);
    }
}
