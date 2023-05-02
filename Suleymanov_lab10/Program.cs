//one();
//two();
//three();
using System.Data;
using System.Globalization;

four();
void one()
{
    string path = "C:\\Users\\Envy\\source\\repos\\Suleymanov_lab10\\basedir";
    string path1 = path + "\\dir0";
    int filesCount = Directory.GetFiles(path1).Length;
    int dirsCount = Directory.GetDirectories(path1).Length;
    Console.WriteLine($"a) Количество каталогов: {dirsCount}\n" +
        $"Количество файлов: {filesCount}\n" +
        $"Общее количество: {filesCount + dirsCount}");
    string[] dirsNames = Directory.GetDirectories(path1).Select(d => new DirectoryInfo(d).Name).ToArray();
    Console.WriteLine($"b) Имена каталогов: {string.Join(", ", dirsNames)}");
    int textFilesCount = Directory.GetFiles(path1).Count(f => Path.GetExtension(f).Equals(".txt"));
    Console.WriteLine($"c) Количество файлов: {filesCount}\n" +
        $"Количество текстовых среди них: {textFilesCount}");
    string[] dirs = Directory.GetFileSystemEntries(path);
    Console.Write("d) Имена всех пустых директорий: ");
    foreach (string catalog in dirs)
    {
        int subDirsCount = Directory.GetFileSystemEntries(catalog).Length;
        if (subDirsCount == 0) Console.WriteLine(new DirectoryInfo(catalog).Name);
        else
        {
            string[] subDirs = Directory.GetDirectories(catalog);
            foreach (string subCatalog in subDirs)
            {
                int subCatalogCount = Directory.GetFileSystemEntries(subCatalog).Length;
                if (subCatalogCount == 0)
                {
                    Console.WriteLine(new DirectoryInfo(subCatalog).Name);
                }
            }
        }
    }
    string path2 = path1 + "\\елки.txt";
    string fullName = new DirectoryInfo(path2).FullName;
    Console.WriteLine($"e) Полный путь к 'елки.txt': {fullName}");
    string maxDirName = "C:\\Users\\Envy\\source\\repos\\Suleymanov_lab10\\basedir\\dir0";
    Console.WriteLine("f) Имена всех вложенных файлов и директорий: ");
    foreach (string catalog in dirs)
    {
        Console.WriteLine(new DirectoryInfo(catalog).Name);
        string[] catalogsDirs = Directory.GetDirectories(catalog);
        string[] catalogsFiles = Directory.GetFiles(catalog);
        maxDirName = (Directory.GetFiles(maxDirName).Length > catalogsFiles.Length) ? maxDirName : catalog;
        foreach (string subCatalogs in catalogsDirs)
        {
            Console.WriteLine($"  >{new DirectoryInfo(subCatalogs).Name}");
            string[] subCatalogsDirs = Directory.GetDirectories(subCatalogs);
            string[] subCatalogsFiles = Directory.GetFiles(subCatalogs);
            maxDirName = (Directory.GetFiles(maxDirName).Length > subCatalogsFiles.Length) ? maxDirName : subCatalogs;
            foreach (string dd in subCatalogsDirs)
            {
                Console.WriteLine($"    >{new DirectoryInfo(dd).Name}");
                string[] DdDirs = Directory.GetDirectories(dd);
                string[] DdFiles = Directory.GetFiles(dd);
                foreach (string subDd in DdDirs)
                {
                    Console.WriteLine($"       >{new DirectoryInfo(subDd).Name}");
                    string[] subDdDirs = Directory.GetDirectories(subDd);
                    string[] subDdFiles = Directory.GetFiles(subDd);
                }
                foreach (string subF in DdFiles) Console.WriteLine($"       >{new DirectoryInfo(subF).Name}");
            }
            foreach (string ff in subCatalogsFiles) Console.WriteLine($"    >{new DirectoryInfo(ff).Name}");
        }
        foreach (string subCatalogsFiles in catalogsFiles) Console.WriteLine(new DirectoryInfo(subCatalogsFiles).Name);
    }
    Console.WriteLine($"g) Имя директории с максимальым количеством файлов: {new DirectoryInfo(maxDirName).Name}");
    Console.WriteLine($"h) {DeepDir(new DirectoryInfo(path))}");
    DriveInfo driveTem = new DriveInfo(Path.GetPathRoot(Environment.CurrentDirectory));
    long freeSpace = driveTem.AvailableFreeSpace;
    Console.WriteLine($"i) {(freeSpace / (1024 * 1024))} MB");
    DriveInfo[] drives = DriveInfo.GetDrives();
    int count = 0;
    Console.Write("j) ");
    foreach (DriveInfo drive in drives)
    {
        count++;
        Console.WriteLine(drive.Name);
    }
    Console.WriteLine("Количество дисков: " + count);
    Console.WriteLine($"");
}
static string DeepDir(DirectoryInfo dir, string path = "")
{
    string deepestPath = path;
    int deepestLevel = path.Split(Path.DirectorySeparatorChar).Length;

    foreach (var subDir in dir.GetDirectories())
    {
        string subPath = DeepDir(subDir, Path.Combine(path, subDir.Name));
        int subLevel = subPath.Split(Path.DirectorySeparatorChar).Length;
        if (subLevel > deepestLevel)
        {
            deepestPath = subPath;
            deepestLevel = subLevel;
        }
    }
    foreach (var file in dir.GetFiles())
    {
        if (path != "" && path.Split(Path.DirectorySeparatorChar).Length > deepestLevel)
        {
            deepestPath = path + Path.DirectorySeparatorChar + file.Name;
            deepestLevel = path.Split(Path.DirectorySeparatorChar).Length;
        }
    }

    return deepestPath;
}

void two()
{
//a)
    string path = "C:\\Users\\Envy\\source\\repos\\Suleymanov_lab10\\basedir";
    string pictureDir = Path.Combine(path, "Picture");
    string textsDir = Path.Combine(path, "Texts");
    string historyDir = Path.Combine(textsDir, "History");
    string horrorStock = Path.Combine(textsDir, "Horror");
    string horrorDir = Path.Combine(horrorStock, "First");
    Directory.CreateDirectory(pictureDir);
    Directory.CreateDirectory(historyDir);
    Directory.CreateDirectory(horrorDir);
//b)
    for (int i = 1; i <= 6; i++)
    {
        string fileName = Path.Combine(pictureDir, i + ".txt");
        File.Create(fileName).Close();
    }
//b)
    string oldPathFile = Path.Combine(pictureDir, "5.txt");
    string newPathFile = Path.Combine(pictureDir, "5000.txt");
    File.Move(oldPathFile, newPathFile, true);
    File.Move(newPathFile, Path.Combine(historyDir, "5000.txt"), true);
//c)
    File.Delete(Path.Combine(pictureDir, "6.txt"));
    string[] filesPicture = Directory.GetFiles(pictureDir);
    foreach (var file in filesPicture) Console.WriteLine(new FileInfo(file).Name);
    Console.Write("Какой файл удалить? Например: '6': " );
    string fileNum = Console.ReadLine();
    File.Delete(Path.Combine(pictureDir, fileNum + ".txt"));
    Directory.Delete(horrorStock, true);
    Directory.Delete(pictureDir, true);
}

void three()
{
    string path = "C:\\Users\\Envy\\source\\repos\\Suleymanov_lab10\\basedir";
    string dataPath = Path.Combine(path, "data");
    //a)
    string dataOnePath = Path.Combine(dataPath, "dataset_1.txt");
    string[] fileOneText = File.ReadAllText(dataOnePath).Split();
    int a = int.Parse(fileOneText[0]);
    int b = int.Parse(fileOneText[1]);
    Console.WriteLine($"a)\na + b = {a + b}\n" +
        $"a * b = {a * b}\n" +
        $"a + b^2 = {a + b * b}");
    //b)
    string dataTwoPath = Path.Combine(dataPath, "dataset_2.txt");
    int count = 0;
    string[] fileTwoText = File.ReadAllLines(dataTwoPath);
    foreach (string num in fileTwoText) if (int.Parse(num) % 2 == 0) count++;
    Console.WriteLine($"b)\nКоличество четных среди них = {count}");
    //c)
    Console.WriteLine("c)");
    string dataThreePath = Path.Combine(dataPath, "dataset_3.txt");
    string dataResPath = Path.Combine(dataPath, "res_3.txt");
    File.Create(dataResPath).Close();
    string[] fileThreeText = File.ReadAllText(dataThreePath).Split();
    foreach (string text in fileThreeText)
    {
        if (int.Parse(text) < 9999)
        {
            Console.WriteLine(text);
            File.AppendAllText(dataResPath, text + "\n");
        }
    }
    //d)
    string dataFourPath = Path.Combine(dataPath, "dataset_4.txt");
    string[] fileFourPath = File.ReadAllText(dataFourPath).Split();
    string maxNum = fileFourPath[0];
    foreach(string text in fileFourPath)
    {
        maxNum = (int.Parse(maxNum) > int.Parse(text)) ? maxNum : text;
    }
    Console.WriteLine($"d) {maxNum}");
    File.AppendAllText(dataResPath, maxNum);
    //e)
    Console.WriteLine("e)");
    string dataFivePath = Path.Combine(dataPath, "dataset_5.txt");
    string[] fileFiveText = File.ReadAllLines(dataFivePath);
    foreach (string text in fileFiveText) Console.WriteLine(text.ToUpper());
}

void four()
{
    const int n = 3;
    Person[] persons = new Person[n];
    persons[0] = new Person("Ivanov", 165, 70, "01.01.2000");
    persons[1] = new Person("Kuznetsov", 180, 100, "30.04.2002");
    persons[2] = new Person("Korsakova", 162, 50, "25.01.2005");
    string path = "C:\\Users\\Envy\\source\\repos\\Suleymanov_lab10\\basedir";
    string dataPath = Path.Combine(path, "data");
    string personsPath = Path.Combine(dataPath, "persons.txt");
    File.Create(personsPath).Close();
    foreach (Person pers in persons)
    {
        string info = pers.Information();
        File.AppendAllText(personsPath, info + "\n");
    }
    Person person1 = new Person();
    Person person2 = new Person();
    person1.Input();
    person2.Input();
    File.AppendAllText(personsPath, person1.Information() + "\n");
    File.AppendAllText(personsPath, person2.Information() + "\n");
    string[] personsInfo = File.ReadAllLines(personsPath);
    //a)
    Console.WriteLine("a)");
    foreach (string info in personsInfo)
    {
        string[] inf = info.Split();
        DateTime bh = DateTime.Parse(inf[3]);
        int age = DateTime.Now.Year - bh.Year;
        if (DateTime.Now.DayOfYear < bh.DayOfYear) age--;
        Console.Write($"{inf[0]} {age}\n");
    }
    //b)
    Console.WriteLine("b)");
    double avgLenght = 0;
    double avgWeight = 0;
    foreach(string info in personsInfo)
    {
        string[] inf = info.Split();
        avgLenght += double.Parse(inf[1]);
        avgWeight += double.Parse(inf[2]);
    }
    avgLenght /= 5;
    avgWeight /= 5;
    Console.WriteLine(avgLenght + "\n" + avgWeight);
    File.AppendAllText(personsPath, avgLenght + " " + avgWeight);
    //c)
    Console.WriteLine("c)");
    string fatsPath = Path.Combine(dataPath, "fats.txt");
    File.Create(fatsPath).Close();
    foreach (string info in personsInfo)
    {
        string[] inf = info.Split();
        if (int.Parse(inf[2]) > int.Parse(inf[1]) - 90)
        {
            string res = $"Фамилия: {inf[0]}\n" +
                $"Рост: {inf[1]}\n" +
                $"Вес: {inf[2]}\n" +
                $"Дата рождения: {inf[3]}\n";
            Console.WriteLine(res);
            File.AppendAllText(fatsPath, res + "\n");
        }
    }
}

class Person
{
    public string lastname { get; set; }
    public int length { get; set; }
    public double weight { get; set; }
    private DateTime birthday;
    public string Birthday
    {
        set
        {
            char[] seps = { '\\', '.', '/', '-' };
            string[] date = value.Split(seps);
            birthday = new DateTime(int.Parse(date[2]), int.Parse(date[1]), int.Parse(date[0]));
        }
        get { return $"{birthday.ToShortDateString()}"; }
    }
    public Person()
    {
        lastname = "Suleyamnov";
        length = 179;
        weight = 65.5;
        Birthday = "30.06.2004";
    }
    public Person(string lastname, int length, double weight, string date)
    {
        this.lastname = lastname;
        this.length = length;
        this.weight = weight;
        Birthday = date;
    }
    public Person(Person other)
    {
        this.lastname = other.lastname;
        this.weight = other.weight;
        this.length = other.length;
        this.Birthday = other.Birthday;
    }
    public void Input()
    {
        Console.Write("Введите фамилию: ");
        string l = Console.ReadLine();
        while (l == "")
        {
            Console.Write("Еще разок: ");
            l = Console.ReadLine();
        }
        lastname = l;
        Console.Write("Введите рост: ");
        string len = Console.ReadLine();
        while(len == "" || int.TryParse(len, out int leng) == false)
        {
            Console.Write("Еще разок: ");
            len = Console.ReadLine();
        }
        length = int.Parse(len);
        Console.Write("Введите вес: ");
        string w = Console.ReadLine();
        while (w == "" || double.TryParse(w, out double weig) == false)
        {
            Console.Write("Еще разок: ");
            w = Console.ReadLine();
        }
        weight = double.Parse(w);
        Console.Write("Введите дату в формате dd/mm/yyyy: ");
        string b = Console.ReadLine();
        while (b == "" || DateTime.TryParse(b, out DateTime res) == false)
        {
            Console.Write("Еще разок: ");
            b = Console.ReadLine();
        }
        Birthday = b;
    }

    public string Information()
    {
        string info = $"{lastname} {length} {weight} {Birthday}";
        return info;
    }
}