using System;
using System.Collections.Generic;
using System.Text;

namespace Hotel.Application.Interface.Repositorys
{
    public interface IdbSeederRepository
    {
        Task SeederAsync();
    }
}
