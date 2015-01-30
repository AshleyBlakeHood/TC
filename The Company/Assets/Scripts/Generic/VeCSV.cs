using System;
using System.Collections.Generic;

class VeCSV
{
    public enum HeaderDirection { Horizontal, Vertical }

    private HeaderDirection headerDirection;
    string[,] data;

    public int headerCount { get; private set; }
    public int subValueCount { get; private set; }

    public VeCSV(string csvData, HeaderDirection headerDirection)
    {
        this.headerDirection = headerDirection;

        string[] csvLines = csvData.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

        //Sanitise the lines in the CSV file.
        for (int l = 0; l < csvLines.Length; l++)
        {
            //Remove all trailing commas.
            csvLines[l] = csvLines[l].TrimEnd(',');

            //Add comma at the end so the final value is counted when parsing.
            csvLines[l] += ',';
        }

        switch(headerDirection)
        {
            case HeaderDirection.Horizontal:
                int maxColumns = GetHorizontalColumnCount(csvLines[0]);
                headerCount = maxColumns;
                subValueCount = csvLines.Length;

                //Set data size based on found values in first row.
                data = new string[csvLines.Length, maxColumns]; //Row, Column.

                SetHorizontalData(csvLines);
                break;
            case HeaderDirection.Vertical:
                int maxRows = GetHorizontalColumnCount(csvLines[0]);
                headerCount = csvLines.Length;
                subValueCount = maxRows;

                //Set data size based on found values in first row.
                data = new string[maxRows, csvLines.Length]; //Row, Column.

                SetVerticalData(csvLines);
                break;
        }
    }

    public string Get(string header, int subValue)
    {
		try
		{
		string v = data [subValue, GetHeaderIntValueFromString (header)];
		}
		catch{
			string ss = "s";
				}
		return data[subValue, GetHeaderIntValueFromString(header)];
    }

    public string Get(int header, int subValue)
    {
		return data[subValue, header];
    }

    public int GetHeaderIntValueFromString(string header)
    {
        switch (headerDirection)
        {
            case HeaderDirection.Horizontal:
                for (int i = 0; i < headerCount; i++)
                {
                    if (data[0, i].ToUpper() == header.ToUpper())
                        return i;
                }

                return -1;
            case HeaderDirection.Vertical:
                for (int i = 0; i < headerCount; i++)
                {
                    if (data[0, i].ToUpper() == header.ToUpper())
                        return i;
                }

                return -1;
        }

        return -1;
    }

    private void SetHorizontalData(string[] csvLines)
    {
        bool flag = false;

        //Loop through all lines.
        for (int l = 0; l < csvLines.Length; l++)
        {
            int currentColumn = 0;
            int startIndex = 0;

            //Loop through all characters.
            for (int c = 0; c < csvLines[l].Length; c++)
            {
                //If character is a comma and the flag isn't active set the data.
                if (csvLines[l][c] == ',' && !flag)
                {
                    string addition = "";
                    string substring = csvLines[l].Substring(startIndex, c - startIndex);

                    for (int a = 0; a < substring.Length; a++)
                    {
                        if (substring[a] == '"')
                        {
                            if (a < substring.Length - 1)
                            {
                                if (substring[a + 1] == '"')
                                {
                                    addition += '"';
                                    a++;
                                }
                            }
                        }
                        else
                            addition += substring[a];
                    }

                    data[l, currentColumn] = addition;
                    startIndex = c + 1;
                    currentColumn++;
                }
                else if (csvLines[l][c] == '"')
                    flag = !flag;
            }
        }
    }

    private int GetHorizontalColumnCount(string horizontalLine)
    {
        int maxColumns = 0;
        bool flag = false;

        //Get the columns based on the first item.
        for (int c = 0; c < horizontalLine.Length; c++)
        {
            if (horizontalLine[c] == ',' && !flag)
            {
                maxColumns++;
            }
            else if (horizontalLine[c] == '"')
                flag = !flag;
        }

        return maxColumns;
    }

    private void SetVerticalData(string[] csvLines)
    {
        bool flag = false;

        //Loop through all lines.
        for (int l = 0; l < csvLines.Length; l++)
        {
            int currentColumn = 0;
            int startIndex = 0;

            //Loop through all characters.
            for (int c = 0; c < csvLines[l].Length; c++)
            {
                //If character is a comma and the flag isn't active set the data.
                if (csvLines[l][c] == ',' && !flag)
                {
                    string addition = "";
                    string substring = csvLines[l].Substring(startIndex, c - startIndex);

                    for (int a = 0; a < substring.Length; a++)
                    {
                        if (substring[a] == '"')
                        {
                            if (a < substring.Length - 1)
                            {
                                if (substring[a + 1] == '"')
                                {
                                    addition += '"';
                                    a++;
                                }
                            }
                        }
                        else
                            addition += substring[a];
                    }

                    data[l, currentColumn] = addition;
                    startIndex = c + 1;
                    currentColumn++;
                }
                else if (csvLines[l][c] == '"')
                    flag = !flag;
            }
        }
    }
}
