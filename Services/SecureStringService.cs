using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace PshSecureConsole.Services;

/// <summary>
/// SecureString 的基底是 ProtectedData 類別。
/// 因為用 Uincode 進行編解碼故不適用中文字。
/// </summary>
internal class SecureStringService
{
  public string Decrypt(string encryptedText)
  {
    // 解開 Protected SecureString
    string decryptedText = DoUpprotectText(encryptedText);
    return decryptedText;
  }

  public string Encrypt(string encryptedText)
  {
    string cypherText = DoProtectText(encryptedText);
    return cypherText;
  }

  /// <summary>
  /// 說明：SecureString 的基底是 ProtectedData 類別。
  /// </summary>
  static string DoUpprotectText(string cypherText)
  {
    // Convert the hex dump to byte array
    int length = cypherText.Length / 2;
    byte[] encryptedData = new byte[length];
    for (int index = 0; index < length; ++index)
    {
      var chunk = cypherText.Substring(2 * index, 2);
      encryptedData[index] = byte.Parse(chunk, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
    }

    // Decrypt the byte array to Unicode byte array
#pragma warning disable CA1416 // 驗證平台相容性
    byte[] data = ProtectedData.Unprotect(encryptedData, null, DataProtectionScope.LocalMachine);
#pragma warning restore CA1416 // 驗證平台相容性

    // Convert Unicode byte array to string
    string unprotectedText = Encoding.Unicode.GetString(data);

    return unprotectedText;
  }

  /// <summary>
  /// 說明：SecureString 的基底是 ProtectedData 類別。
  /// </summary>
  static string DoProtectText(string plainText)
  {
    byte[] plainBlob = Encoding.Unicode.GetBytes(plainText);

#pragma warning disable CA1416 // 驗證平台相容性
    byte[] cypherBlob = ProtectedData.Protect(plainBlob, null, DataProtectionScope.LocalMachine);
#pragma warning restore CA1416 // 驗證平台相容性

    string cypherText = Convert.ToHexString(cypherBlob);
    return cypherText;
  }

  static SecureString StringToSecureString(string plainText)
  {
    SecureString secureString = new SecureString();
    foreach (char c in plainText)
      secureString.AppendChar(c);

    secureString.MakeReadOnly();
    return secureString;
  }

  static string SecureStringToString(SecureString secureString)
  {
    IntPtr bstr = IntPtr.Zero;
    try
    {
      bstr = Marshal.SecureStringToBSTR(secureString);
      return Marshal.PtrToStringAuto(bstr)!;
    }
    finally
    {
      if (bstr != IntPtr.Zero)
      {
        Marshal.ZeroFreeBSTR(bstr);
      }
    }
  }
}
