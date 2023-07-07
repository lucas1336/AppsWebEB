using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EB.API.Input;
using EB.Domain.Interface;
using EB.Infrastructure.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EB.API.Controllers
{
    [Route("api/snapshot")]
    [ApiController]
    public class SnapshotController : ControllerBase
    {
        private ISnapshotDomain _snapshotDomain;
        
        public SnapshotController(ISnapshotDomain snapshotDomain)
        {
            _snapshotDomain = snapshotDomain;
        }

        // GET: api/Snapshot/5
        [HttpGet("{id}", Name = "Get Snapshots By Product Id")]
        public List<Snapshot> Get(int id)
        {
            return _snapshotDomain.GetSnapshotsByProductId(id);
        }

        // POST: api/Snapshot/5
        [HttpPost ("{id}", Name = "Create Snapshot")]
        public Snapshot Post([FromBody] SnapshotInput value, int id)
        {
            return _snapshotDomain.Create(value, id);
        }
    }
}
