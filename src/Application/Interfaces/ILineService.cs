using Application.Models.Request;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ILineService
    {
        IEnumerable<LineGetRequest> GetAllLines();
        LineGetRequest GetLineById(int id);
        Line CreateLine(LineCreateRequest lineCreateRequest);
        void UpdateLine(int id, LineUpdateRequest lineUpdateRequest);
        void DeleteLine(int id);
    }
}
