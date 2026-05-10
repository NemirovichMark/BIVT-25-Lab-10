{
  "version": 3,
  "targets": {
    "net8.0": {
      "Newtonsoft.Json/13.0.5-beta1": {
        "type": "package",
        "compile": {
          "lib/net6.0/Newtonsoft.Json.dll": {
            "related": ".xml"
          }
        },
        "runtime": {
          "lib/net6.0/Newtonsoft.Json.dll": {
            "related": ".xml"
          }
        }
      }
    }
  },
  "libraries": {
    "Newtonsoft.Json/13.0.5-beta1": {
      "sha512": "cGxikAY78u8CPfIT7mkICvI2ZNL7oDsC+DzgVWxtU5DmNhF2qDcU3YheDitdO9TKIra6Y5rHRD2T7oEexbAbLA==",
      "type": "package",
      "path": "newtonsoft.json/13.0.5-beta1",
      "files": [
        ".nupkg.metadata",
        ".signature.p7s",
        "LICENSE.md",
        "README.md",
        "lib/net20/Newtonsoft.Json.dll",
        "lib/net20/Newtonsoft.Json.xml",
        "lib/net35/Newtonsoft.Json.dll",
        "lib/net35/Newtonsoft.Json.xml",
        "lib/net40/Newtonsoft.Json.dll",
        "lib/net40/Newtonsoft.Json.xml",
        "lib/net45/Newtonsoft.Json.dll",
        "lib/net45/Newtonsoft.Json.xml",
        "lib/net6.0/Newtonsoft.Json.dll",
        "lib/net6.0/Newtonsoft.Json.xml",
        "lib/netstandard1.0/Newtonsoft.Json.dll",
        "lib/netstandard1.0/Newtonsoft.Json.xml",
        "lib/netstandard1.3/Newtonsoft.Json.dll",
        "lib/netstandard1.3/Newtonsoft.Json.xml",
        "lib/netstandard2.0/Newtonsoft.Json.dll",
        "lib/netstandard2.0/Newtonsoft.Json.xml",
        "newtonsoft.json.13.0.5-beta1.nupkg.sha512",
        "newtonsoft.json.nuspec",
        "packageIcon.png"
      ]
    }
  },
  "projectFileDependencyGroups": {
    "net8.0": [
      "Newtonsoft.Json >= 13.0.5-beta1"
    ]
  },
  "packageFolders": {
    "/Users/kirillkadykov/.nuget/packages/": {}
  },
  "project": {
    "version": "1.0.0",
    "restore": {
      "projectUniqueName": "/Users/kirillkadykov/RiderProjects/BIVT-25-Lab-9/Lab9/Lab9.csproj",
      "projectName": "Lab9",
      "projectPath": "/Users/kirillkadykov/RiderProjects/BIVT-25-Lab-9/Lab9/Lab9.csproj",
      "packagesPath": "/Users/kirillkadykov/.nuget/packages/",
      "outputPath": "/Users/kirillkadykov/RiderProjects/BIVT-25-Lab-9/Lab9/obj/",
      "projectStyle": "PackageReference",
      "configFilePaths": [
        "/Users/kirillkadykov/.nuget/NuGet/NuGet.Config"
      ],
      "originalTargetFrameworks": [
        "net8.0"
      ],
      "sources": {
        "https://api.nuget.org/v3/index.json": {}
      },
      "frameworks": {
        "net8.0": {
          "targetAlias": "net8.0",
          "projectReferences": {}
        }
      },
      "warningProperties": {
        "warnAsError": [
          "NU1605"
        ]
      },
      "restoreAuditProperties": {
        "enableAudit": "true",
        "auditLevel": "low",
        "auditMode": "direct"
      },
      "SdkAnalysisLevel": "9.0.300"
    },
    "frameworks": {
      "net8.0": {
        "targetAlias": "net8.0",
        "dependencies": {
          "Newtonsoft.Json": {
            "target": "Package",
            "version": "[13.0.5-beta1, )"
          }
        },
        "imports": [
          "net461",
          "net462",
          "net47",
          "net471",
          "net472",
          "net48",
          "net481"
        ],
        "assetTargetFallback": true,
        "warn": true,
        "frameworkReferences": {
          "Microsoft.NETCore.App": {
            "privateAssets": "all"
          }
        },
        "runtimeIdentifierGraphPath": "/usr/local/share/dotnet/sdk/9.0.305/PortableRuntimeIdentifierGraph.json"
      }
    }
  }
}