namespace onenote_to_obsidian_md_path_fix
{
    internal class Program
    {
        static void Main(string[] args)
        {
            String removedcurlies = removeImageWidths(File.ReadAllText(@"C:\Temp\notes\MMSA\Finance\Loading-Allocations.md"));
            return;
            string path = @"C:\temp\notes\MMSA";
            string[] files = Directory.GetFiles(path, "*.md", SearchOption.AllDirectories);

            foreach (string file in files)
            {
                String old_path = file.Substring(0, file.LastIndexOf('\\')) + "/media/";
                old_path = old_path.Replace("C:", "c:");
                old_path = Char.ToLower(old_path[0]) + old_path.Substring(1);
                //Console.WriteLine(old_path);

                    string fileContent = File.ReadAllText(file);
                if (fileContent.Contains(old_path))
                    Console.WriteLine("url match found in " + file);
                string newContent = fileContent.Replace(old_path, "media/");
                //Console.WriteLine(file.Substring(0, file.LastIndexOf('\\'))+"/media/");
                //replace width in inches
                //eg{width="7.25in" height="5.28125in"}
                //if (!newContent.Contains({width"))
                newContent = removeImageWidths(newContent);
                if (!newContent.Equals(fileContent))
                {
                    Console.WriteLine("path found " + file);
                    File.WriteAllText(file, newContent);
                }
                /*
                if (file.Substring(0, file.LastIndexOf('\\')).Length > 0)
                {
                    Console.WriteLine("path found " + file + ":" + file.Substring(0, file.LastIndexOf('\\') + 6));
                    return;
                }
                */
//                    if (newContent.Contains("<p>"))
//                    Console.WriteLine("Page is html "+file);
                //Console.WriteLine(newContent);
                //return;
            }
        }

        private static string removeImageWidths(string newContent)
        {
            String newStr = newContent.ToString();
            while (newStr.Contains("){width"))
            {
                int startPosition = newStr.IndexOf("){width")+1;
                int endPosition = newStr.IndexOf("\"}")+2;
                Console.WriteLine("Chop out from "+startPosition+" to "+endPosition+":"+newStr.Substring(startPosition,endPosition-startPosition));  
                String beforeStart = newStr.Substring(1, startPosition-1);
                String afterEnd = newStr.Substring(endPosition);
                newStr = beforeStart + afterEnd;
            }
            return newStr;
        }
    }
}