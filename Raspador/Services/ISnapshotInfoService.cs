using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Raspador.Models;

namespace Raspador.Services
{
        public interface ISnapshotInfoService
        {
            Task<SnapshotInfo[]> GetSnapshotAsync(IdentityUser user);

            Task<bool> AddSnapshotAsync(SnapshotInfo snapshotInfo, IdentityUser user);
        }
}