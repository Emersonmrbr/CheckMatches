# Lottery Results Checker

This repository contains a script to check lottery results. The script compares the drawn numbers with the bets made provided in a JSON file.

## Repository Structure

- `Program.cs`: Main script that compares the results of the bets.
- `Bets.json`: File containing the bets made.

## How to Use

### Prerequisites

- [.NET Core SDK](https://dotnet.microsoft.com/download) installed on your machine.

### Steps

1. Clone this repository:

   ```bash
   git clone https://github.com/Emersonmrbr/CheckMatches.git
   ```

   ```bash
   cd CheckMatches
   ```

2. Make sure the `Bets.json` file is in the correct format:

   ```json
   {
    "Bets": [
     { "Row": 1, "Numbers": [12, 23, 34, 45, 56, 6] },
     { "Row": 2, "Numbers": [8, 19, 30, 41, 52, 3] },
     { "Row": 3, "Numbers": [15, 26, 37, 48, 59, 10] }
    ]
   }
   ```

3. Open the `Program.cs` file and set the `gameResult` variable with the drawn numbers:

   ```csharp
   var gameResult = new List<int>() { 44, 5, 6, 1, 8, 39 };
   ```

4. Run the script:
   ```bash
   dotnet run
   ```

## Usage Example

### Input

- Game result:

  ```csharp
  var gameResult = new List<int>() { 44, 5, 6, 1, 8, 39 };
  ```

- `Bets.json` file:
  ```json
  {
   "Bets": [
    { "Row": 1, "Numbers": [12, 23, 34, 45, 56, 6] },
    { "Row": 2, "Numbers": [8, 19, 30, 41, 52, 3] },
    { "Row": 3, "Numbers": [15, 26, 37, 48, 59, 10] }
   ]
  }
  ```

### Output

The script compares each bet row with the drawn numbers and prints the results:

```bash
  Result in row 5 match this numers list 1, 44
  Result in row 3 dont match any number

  Total de apostas ganhadoras 1
```

## Contribution

If you would like to contribute to this project, feel free to open a pull request or create an issue.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
