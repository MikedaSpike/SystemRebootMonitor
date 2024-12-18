# RebootChecker

**RebootChecker** is a VB.NET console application that verifies if a Windows system has been rebooted within a specified number of minutes, defaulting to 5 minutes if no parameter is provided.

## Requirements

- Windows Operating System
- .NET Framework 4.8 or later

## Usage

1. **Running the Program**:
   - Open Command Prompt.
   - Navigate to the directory containing the `RebootChecker.exe` file.
   - Run the program using the following command:
     ```
     RebootChecker [minutes]
     ```
   - If no parameter is provided, it defaults to 5 minutes.

2. **Command Line Parameters**:
   - `[minutes]` - (Optional) Integer value representing the number of minutes within which to check if the system was rebooted.

3. **Help Information**:
   - To display usage instructions, run the program with `/` or `-`:
     ```
     RebootChecker /?
     ```

## Example

Running the program with a parameter of 10 minutes:
```
RebootChecker 10
```
Output:
```
System rebooted within 10 minutes: True
```

## Contributing

Feel free to fork this repository, submit issues, or create pull requests to contribute to this project.

## License

This project is licensed under the MIT License.



