using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{

    public class MachineDTO
    {
        public string? CODE { get; set; }
        public string? MODEL { get; set; }
        public string? MACHINE_TYPE { get; set; }
        public string? MACHINE_GROUP { get; set; }
        public string? LOCATION { get; set; }

        public Machine ToEntity()
        {
            return new Machine
            {
                CODE = this.CODE,
                MODEL = this.MODEL,
                MACHINE_TYPE = this.MACHINE_TYPE,
                MACHINE_GROUP = this.MACHINE_GROUP,
                LOCATION = this.LOCATION
            };
        }
    }

    [Table("MACHINELIST2")]
    public class Machine
    {
        [Key] // eğer primary key olacaksa
        [Range(1, int.MaxValue, ErrorMessage = "ID 0'dan büyük olmalıdır")]
        public int ID { get; set; }
        public string? CODE { get; set; }
        public string? MODEL { get; set; }
        public string? MACHINE_TYPE { get; set; }
        public string? MACHINE_GROUP { get; set; }
        public string? LOCATION { get; set; }

        public MachineDTO ToDto()
        {
            return new MachineDTO
            {
                CODE = this.CODE,
                MODEL = this.MODEL,
                MACHINE_TYPE = this.MACHINE_TYPE,
                MACHINE_GROUP = this.MACHINE_GROUP,
                LOCATION = this.LOCATION
            };
        }

    }
}
