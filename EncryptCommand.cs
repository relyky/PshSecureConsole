using Cocona;
using PshSecureConsole.Services;

namespace PshSecureConsole;

internal class EncryptCommand(SecureStringService _sstrSvc)
{
  [Command("encrypt", Description = "加密文字成 SecureString。因為用 Uincode 進行編解碼故不適用中文字。")]
  public Task Encrypt()
  {
    // 從標準輸入(std::cin)中讀取明文。
    string plainText = string.Empty;
    using (Stream inputStream = Console.OpenStandardInput())
    using (StreamReader reader = new StreamReader(inputStream))
    {
      plainText = reader.ReadToEnd();
    }

    // 加密明文。
    string cypherText = _sstrSvc.Encrypt(plainText);

    // 將加密後的 SecureString 輸出到標準輸出(std::cout)。
    Console.WriteLine(cypherText);

    return Task.CompletedTask;
  }
}
