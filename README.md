# PshSecureConsole
 SecureString with PowerShell   
 使用 PowerShell 加解密機敏文字，用 C# 程式碼解開。   

# 引言
使用 PowerShell 在本機進行加解密。過程中使用 SecureString 結構存儲存與交換；其基底是 ProtectedData 類別。SecureString 是用本機的秘密金鑰加解密，故換主機後會無效。   
注意事項：加解密的編解碼是用 Unicode (UTF-16) 故中文字無效。另外 ProtectedData 類別只能在 windows 環境執行。

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
```bash
echo abc;XYZ;12345.6789 | PshSecureConsole encrypt | PshSecureConsole decrypt
abc;XYZ;12345.6789
```
![工具加解密測試](/_doc/encrypt_decrypt_test.png)

PowerShell加密；工具解密。  
```powershell
# 用 Powershell 加密
$plainText = 'Your DataBase ConnString; 12345.6789'
$secureString = ConvertTo-SecureString -String $plainText -AsPlainText -Force
$encryptedString = ConvertFrom-SecureString -SecureString $secureString
$encryptedString

# 用本工具解密
$decryptedText = echo $encryptedString | ./PshSecureConsole decrypt
$decryptedText
```
![PowerShell加密；工具解密](/_doc/psh_encrypt_decrypt.png)

工具加密；PowerShell工具解密。   
```powershell
# 用本工具加密
$plainText = 'ABC;def; 1234.56789'
$encryptedString = echo $plainText | ./PshSecureConsole encrypt

# 用 Powershell 解開
$decryptedString = ConvertTo-SecureString -String $encryptedString
$Marshal = [System.Runtime.InteropServices.Marshal]
$Bstr = $Marshal::SecureStringToBSTR($decryptedString)
$decryptedText = $Marshal::PtrToStringAuto($Bstr)
$Marshal::ZeroFreeBSTR($Bstr)
$decryptedText
```
![工具加密；PowerShell工具解密](/_doc/encrypt_psh_descrypt.png)
 
# 參考文章
* [如何在 PowerShell 對機敏字串進行加解密處理](https://blog.miniasp.com/post/2023/09/18/Encryption-and-Decryption-in-PowerShell)
* [如何在C#中解密通过PowerShell加密的字符串](https://cloud.tencent.com/developer/ask/sof/112156544)
* [How to decrypt a string in C# which is encrypted via PowerShell](https://stackoverflow.com/questions/30859038/how-to-decrypt-a-string-in-c-sharp-which-is-encrypted-via-powershell)
* [Encryption and Decryption in PowerShell](https://medium.com/@nikhilsda/encryption-and-decryption-in-powershell-e7a678c5cd7d)
