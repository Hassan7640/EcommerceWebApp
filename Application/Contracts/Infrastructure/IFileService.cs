using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Infrastructure
{
    public interface IFileService
    {
        public Task<bool> UploadFile(IFormFile file)
    }
}
