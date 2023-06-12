using System.ComponentModel.DataAnnotations.Schema;


namespace StudentApp.Entity
{
	[Table("roles")]
	public class Role : BaseEntity
	{
		public int Id { get; set; }


		public string Name { get; set; }

		public ICollection<User> users { get; set; }
	}
}
