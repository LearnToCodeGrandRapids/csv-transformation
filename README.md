# CSV Transformation

## Background

> In computing, a comma-separated values (CSV) file is a delimited text file that uses a comma to separate values (many implementations of CSV import/export tools allow other separators to be used). It stores tabular data (numbers and text) in plain text. Each line of the file is a data record. Each record consists of one or more fields, separated by commas. The use of the comma as a field separator is the source of the name for this file format.
>
> The CSV file format is not standardized. The basic idea of separating fields with a comma is clear, but that idea gets complicated when the field data may also contain commas or even embedded line-breaks. CSV implementations may not handle such field data, or they may use quotation marks to surround the field. Quotation does not solve everything: some fields may need embedded quotation marks, so a CSV implementation may include escape characters or escape sequences.
>
> -- [Wikipedia, Comma-separated values](https://en.wikipedia.org/wiki/Comma-separated_values)

## Overview

In this exercise you will create a console application that reads in a CSV file, performs a data transformation, and then writes the new data into a new CSV file.

## Requirements

### Command Line Arguments

* **input-file**: the file path to the _input_ csv file
* **output-file**: the file path to the _output_ csv file

### Example Usage (.NET)

```
CsvTransformation.exe --input-file=C:\Data\Input.csv --output-file=C:\Data\Output.csv
```

### Functional Requirements

* Parse the command line arguments passed to the console application
* Read the input file
* Parse each record
* Perform the data transformation
    * Parse the `Name` field into `FirstName` and `LastName`
    * Convert the `AmountDue` field from cents to dollars
    * Convert the date to YYYY-MM-DD format
* Write the resulting records to the output file

### Considerations

* Should you read the input file in all at once, or should you read it in one line at a time?
* Should you keep a running list of all the resulting records after transformation and then write them out to the output file all at once, or write each record out as you perform the transformation?

### Example 

#### Input File

```
AccountNumber,LoanId,Name,AmountDue,DateDue,SSN
100000,001,"Green, Joe",10000,20180715,1234
100001,002,"White, Fox",20000,20180715,4321
```

#### Output File

```
AccountNumber,LoanId,FirstName,LastName,AmountDue,DateDue,SSN
100000,001,Joe,Green,100.00,2018-07-15,1234
100001,002,Fox,White,200.00,2018-07-15,4321
```

## Resources

* **CsvTransformationInput.csv**: a sample input file for this exercise
* **CsvTransformationOutput.csv**: the expected output file based on the sample input file