﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagement.Data.Contracts
{
    public interface IUnitOfWork: IDisposable
    {
        Task CommitAsync();
        void Commit();
    }
}
