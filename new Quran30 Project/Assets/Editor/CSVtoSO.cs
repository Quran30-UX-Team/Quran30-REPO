using UnityEngine;
using UnityEditor;
using System.IO;

public class CSVtoSO
{
    // Adjusted path to be relative to the project's root folder
    private static string questionCSVPath = "/Editor/CSVs/questionCSV.csv";

    [MenuItem("Utilities/Generate Question SO")]
    public static void CreateQuestionSO()
    {
        // Combine application data path with CSV path
        string fullPath = Application.dataPath + questionCSVPath;

        // Check if the CSV file exists
        if (!File.Exists(fullPath))
        {
            Debug.LogError("CSV file not found at path: " + fullPath);
            return;
        }

        // Read all lines from the CSV file
        string[] allLines = File.ReadAllLines(fullPath);

        // Iterate through each line in the CSV
        for (int lineIndex = 0; lineIndex < allLines.Length; lineIndex++)
        {
            string line = allLines[lineIndex];

            // Split the line into elements using comma as separator
            string[] splitData = line.Split(',');

            // Ensure the line contains at least 6 elements (question code, question, and 4 answers)
            if (splitData.Length >= 6)
            {
                // Create a new instance of questionData scriptable object
                questionData questionData = ScriptableObject.CreateInstance<questionData>();

                // Assign values from the split data to questionData fields
                questionData.questionCode = splitData[0];
                questionData.question = splitData[1];

                // Initialize answers array
                questionData.answers = new string[4];

                // Populate answers array
                for (int i = 0; i < 4; i++)
                {
                    questionData.answers[i] = splitData[i + 2];
                }

                // Specify the asset path for the questionData and create the asset
                string assetPath = $"Assets/Resources/GeneratedQuestion/{questionData.questionCode}.asset";
                AssetDatabase.CreateAsset(questionData, assetPath);

                // Refresh asset database to ensure the asset is visible in Unity Editor
                AssetDatabase.Refresh();
            }
            else
            {
                // Log an error for invalid CSV format
                Debug.LogError($"Invalid CSV format at line {lineIndex + 1}. Each line must contain at least 5 elements (question code, question, and 4 answers).");
            }
        }
    }
}
