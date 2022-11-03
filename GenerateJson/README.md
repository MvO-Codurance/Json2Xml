# Generate JSON
Simple tool to efficiently generate (and optionally zip) a JSON file containing an arbitrary number of serialised items.
Supports the generation of very large files by streaming the output, thus keeping the memory usage small and consistent.

## Example Execution
```
GenerateJson.exe --output "C:\Temp\output.json" --number 1000000
```

## Options
```
  -o, --output    Required. Full path to the output file.

  -n, --number    Required. Number of items to generate.

  -i, --indent    (Default: false) Indent the generated JSON.

  -z, --zip       (Default: false) Zip the generated JSON file.

  --help          Display this help screen.

  --version       Display version information.

```