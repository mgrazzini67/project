

## The task

The task is to extend the library to implement another parser that can parse files with orders that contain the following data:
* Order ID (positive integer)
* Date and time when the order was created (UTC date in [ISO format](https://en.wikipedia.org/wiki/ISO_8601))
* Item ID (positive integer)
* Customer ID (positive integer)
* Amount of items in the order
* Price of each item (in German format, i.e. with comma decimal separator)
* Customer comment (string, not required)

Files with orders have slightly different format:
* columns in each line are space-separated instead of comma;
* customer comment may also contain spaces, in which case the value is surrounded with double quotes.

Files are expected to not exceed 1000 lines.

Please use `orders.txt` in `Parsers.Test` project as an example of such file. 

When working on the task, please feel free to implement it as you see better fit (including refactoring of the existing code if needed). Although a third party library for parsing the CSV file could be used, we would rather like to see how you would solve this part yourself.

Sample files can be found in the project that contains a Console application which shows how to use both parsers.
