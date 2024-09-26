using Cocona;
using PshSecureConsole.Services;

namespace PshSecureConsole;

internal class DecryptCommand(SecureStringService _sstrSvc)
{
  [Command("decrypt", Description = "解密 SecureString 成文字。因為用 Uincode 進行編解碼故不適用中文字。")]
  public async Task Decrypt()
  {
    // 從標準輸入(std::cin)中讀取加密後的 SecureString。
    string cypherText = string.Empty;
    using (Stream inputStream = Console.OpenStandardInput())
    using (StreamReader reader = new StreamReader(inputStream))
    {
      cypherText = await reader.ReadToEndAsync();
    }

    // Remove all new-lines
    cypherText = cypherText.Replace(Environment.NewLine, "");

    // 解密 SecureString。
    string plainText = _sstrSvc.Decrypt(cypherText);

    // 將解密後的文字輸出到標準輸出(std::cout)。
    Console.WriteLine(plainText);
  }
}
