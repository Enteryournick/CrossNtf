using CrossNtf.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CrossNtf.Services
{
    interface ICrossDb
    {
        Task<List<CrossDbItemTypes>> GetCrossDbItem(string UriCrossDbParam);
        Task<string> GetCrossDbLastUpdateTime();
    }
}
