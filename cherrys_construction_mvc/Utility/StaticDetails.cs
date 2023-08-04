using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Security.Cryptography;

namespace cherrys_construction_mvc.Utility
{
    public class StaticDetails
    {
        //Account Roles
        public const string Role_Admin = "Admin";
        public const string Role_Employee = "Employee";

        // SHA512 Hash Settings
        public const int keySize = 64;
        public const int iterations = 350000;

        // AES Key
        public const string hardPass = "wiejwdaso=-09/83s=lkmdfv;smkfaw'[fk2ikv$^lnn3297;v303fv-lsrngaf;okjnv4tu+3541";

        // SendGrid API Key
        public const string SendGridKey = "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";

        // Photo Sizes
        public const int LargeCoverImage = 3000;
        public const int LargeImage = 1280;
        public const int WideImage = 1280;
        public const int StandardImage = 1200;
        public const int PortraitImage = 500;
        public const int SquareAvatarImage = 500;
        public const int UltrawideImage = 5000;
    }
}
