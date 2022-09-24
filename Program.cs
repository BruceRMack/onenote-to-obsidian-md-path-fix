namespace onenote_to_obsidian_md_path_fix
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = args[0];
            string[] files = Directory.GetFiles(path, "*.md", SearchOption.AllDirectories);

            foreach (string file in files)
            {

                string fileContent = File.ReadAllText(file);
                String newContent = fixMediaPaths(file, fileContent);
                //replace width in inches
                //eg{width="7.25in" height="5.28125in"}
                newContent = removeImageWidths(newContent);
                
                if (!newContent.Equals(fileContent))
                {
                    File.WriteAllText(file, newContent);
                }

            }
        }

        private static string fixMediaPaths(string file, string fileContent)
        {
            String old_path = file.Substring(0, file.LastIndexOf('\\')) + "/media/";
            //lower first character
            old_path = old_path[0].ToString().ToLower() + old_path.Substring(1);
            old_path = Char.ToLower(old_path[0]) + old_path.Substring(1);
            return fileContent.Replace(old_path, "media/");
        }

        private static string removeImageWidths(string newContent)
        {
            String newStr = newContent.ToString();
            while (newStr.Contains("){width"))
            {
                int startPosition = newStr.IndexOf("){width")+1;
                int endPosition = newStr.IndexOf("\"}")+2;
                String beforeStart = newStr.Substring(1, startPosition-1);
                String afterEnd = newStr.Substring(endPosition);
                newStr = beforeStart + afterEnd;
            }
            return newStr;
        }
    }
}