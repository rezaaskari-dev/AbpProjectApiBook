using System.ComponentModel.DataAnnotations;

namespace LibraryMemberService.Members;

public class CreateUpdateMemberDto
{
    [Required]
    [StringLength(100)]
    public string FullName { get; set; } = string.Empty;

    [Required]
    //[StringLength(10, MinimumLength = 10)]
    public string NationalCode { get; set; } = string.Empty;

    [Required]
    [StringLength(20)]
    public string PhoneNumber { get; set; } = string.Empty;
}