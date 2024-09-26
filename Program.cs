/// Description: 使用 Cocona 套件來實作命令列應用程式

using Cocona;
using Microsoft.Extensions.DependencyInjection;
using PshSecureConsole;
using PshSecureConsole.Services;

var builder = CoconaApp.CreateBuilder();

builder.Services.AddScoped<SecureStringService>();

var app = builder.Build(); //===================================================

//// 主要指令。※只能有一個。
//app.AddCommand((
//  [Option("decript", Description = "解密字串。")] string encryptedText,
//  SecureStringService sstrSvc) =>
//{
//  string decryptedText =  sstrSvc.Decrypt(encryptedText);
//  Console.WriteLine(decryptedText);
//});

app.AddCommands<EncryptCommand>();

app.AddCommands<DecryptCommand>();

app.Run();
