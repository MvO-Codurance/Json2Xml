# Convert JSON To XML
Simple tool to efficiently convert a JSON file to an XML file.
Supports the conversion of very large files by streaming the input and output, thus keeping the memory usage small and consistent.

## Notes
This tool expects the JSON file to contain an array at root level that then contains the objects to be converted.
The input parameters allow the caller to specify the names of the generated root and item elements. 
As such the converter is quite generic.

## Example Execution
```
ConvertJson2Xml.exe --input "C:\Temp\input.json" --output "C:\Temp\output.xml" --rootElementName "People" --elementName "Person"
```

## Options
```
  -i, --input              Required. Full path to the input JSON file.

  -o, --output             Required. Full path to the output XML file.

  -r, --rootElementName    Required. Name of the root XML element.

  -e, --elementName        Required. Name of the item XML element.

  --help                   Display this help screen.

  --version                Display version information.
```