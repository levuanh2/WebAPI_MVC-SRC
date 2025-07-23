namespace TuNhua.Model
{
    public class VaiTroVM
    {
        public string TenVaiTro { get; set; }
    }

    public class VaiTroDetailVM : VaiTroVM
    {
        public Guid RoleId { get; set; }
    }
}
