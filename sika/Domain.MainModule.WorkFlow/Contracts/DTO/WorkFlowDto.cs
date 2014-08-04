using System.Collections.Generic;

namespace Domain.MainModule.WorkFlow.Contracts.DTO
{
    public class WorkFlowDto
    {
        public string TextControl { get; set; }

        public string NextStatus { get; set; }

        public string IdNextStatus { get; set; }

        public string CurrenteResponsible { get; set; }

        public string IdRuta { get; set; }

        public string CurrentStatus { get; set; }
    }
}