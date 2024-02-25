### Arch Linux
- dotnet-sdk-7.0-bin
- dotnet-runtime-7.0-bin

## VSCode Environment.

#### Extensions
C# (ext id - name) needed config per ext listed below
- ms-dotnettools.csharp - C#
- ms-dotnettools.csdevkit - C# Dev Kit
- adrianwilczynski.namespace - C# Namespace Autocomplete
- ms-dotnettools.vscodeintellicode-csharp - IntelliCode for C# Dev Kit
- ms-dotnettools.vscode-dotnet-runtime - .NET Install Tool

Godot
- geequlim.godot-tools - Godot-Tools
    - set godot executable abs path
    - set gdscript lsp server to 127.0.0.1
    - set gdscript lsp server port to 6008

Utility
- Gruntfuggly.todo-tree - Todo Tree

## In Editor settings of Godot-Mono.
- Under `Text Editor` for `External` set
    - use external editor to on
- Under `Network` set
    - remote host to 127.0.0.1
    - remote port to 6008
- Under `Dotnet` for `Editor` options set
    - external editor to vscode