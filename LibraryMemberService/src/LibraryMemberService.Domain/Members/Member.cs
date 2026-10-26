using System;
using System.Linq;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace LibraryMemberService.Members;

public class Member : BasicAggregateRoot<Guid>
{
    public string FullName { get; private set; } = string.Empty;
    public string NationalCode { get; private set; } = string.Empty;
    public string PhoneNumber { get; private set; } = string.Empty;
    public DateTime MembershipDate { get; private set; }

    protected Member()
    {
    }

    public Member(
        Guid id,
        string fullName,
        string nationalCode,
        string phoneNumber)
        : base(id)
    {
        SetDetails(fullName, nationalCode, phoneNumber);
        MembershipDate = DateTime.UtcNow;
    }

    public void SetDetails(
        string fullName,
        string nationalCode,
        string phoneNumber)
    {
        ValidateNationalCode(nationalCode);
        ValidatePhoneNumber(phoneNumber);

        FullName = fullName;
        NationalCode = nationalCode;
        PhoneNumber = phoneNumber;
    }

    private static void ValidateNationalCode(string nationalCode)
    {
        if (string.IsNullOrWhiteSpace(nationalCode))
        {
            throw new BusinessException(
                "کد ملی نمی‌تواند خالی باشد.",
                nameof(nationalCode));
        }

        if (nationalCode.Length != 10)
        {
            throw new BusinessException(
                "کد ملی باید دقیقا 10 رقم باشد.",
                nameof(nationalCode));
        }

        if (!nationalCode.All(char.IsDigit))
        {
            throw new BusinessException(
                "کد ملی باید فقط شامل اعداد باشد.",
                nameof(nationalCode));
        }
    }

    private static void ValidatePhoneNumber(string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
        {
            throw new BusinessException(
                "شماره تلفن نمی‌تواند خالی باشد.",
                nameof(phoneNumber));
        }

        if (phoneNumber.Length != 11)
        {
            throw new BusinessException(
                "شماره تلفن باید دقیقا 11 رقم باشد.",
                nameof(phoneNumber));
        }

        if (!phoneNumber.All(char.IsDigit))
        {
            throw new BusinessException(
                "شماره تلفن باید فقط شامل اعداد باشد.",
                nameof(phoneNumber));
        }
    }
}