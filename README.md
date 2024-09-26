# PshSecureConsole
 SecureString with PowerShell   
 使用 PowerShell 加解密機敏文字，用 C# 程式碼解開。

# PowerShell 加解密機敏文字指令
加密
```powershell
$plainText = 'YourDataBaseConnString'
$secureString = ConvertTo-SecureString -String $plainText -AsPlainText -Force
$encryptedString = ConvertFrom-SecureString -SecureString $secureString
$encryptedString
```
解密
```powershell
$decryptedString = ConvertTo-SecureString -String $encryptedString
$Marshal = [System.Runtime.InteropServices.Marshal]
$Bstr = $Marshal::SecureStringToBSTR($decryptedString)
$decryptedText = $Marshal::PtrToStringAuto($Bstr)
$Marshal::ZeroFreeBSTR($Bstr)
$decryptedText
```
 
# 開發環境
* 平台: NET8
* 骨架: Console App

# 沒圖沒真象
工具加解密測試。   
![工具加解密測試](/_doc/encrypt_decrypt_test.png)

PowerShell加密；工具解密。   
![PowerShell加密；工具解密](/_doc/psh_encrypt_decrypt.png)

工具加密；PowerShell工具解密。   
![工具加密；PowerShell工具解密](/_doc/encrypt_psh_descrypt.png)
 
# 參考文章
* [如何在 PowerShell 對機敏字串進行加解密處理](https://blog.miniasp.com/post/2023/09/18/Encryption-and-Decryption-in-PowerShell)
* [如何在C#中解密通过PowerShell加密的字符串](https://cloud.tencent.com/developer/ask/sof/112156544)
* [How to decrypt a string in C# which is encrypted via PowerShell](https://stackoverflow.com/questions/30859038/how-to-decrypt-a-string-in-c-sharp-which-is-encrypted-via-powershell)
* [Encryption and Decryption in PowerShell](https://medium.com/@nikhilsda/encryption-and-decryption-in-powershell-e7a678c5cd7d)
